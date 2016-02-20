using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Anthill.TypedFactories;
using Selkie.Aco.Ants;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails;
using Selkie.Common;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Aco.Anthill
{
    // todo split into multiple classes
    // todo remove ReSharper disable...
    // todo SpecFlow
    [ProjectComponent(Lifestyle.Transient)]
    // ReSharper disable once ClassTooBig
    public sealed class Colony : IColony
    {
        internal const int DefaultTurnsBeforeSelection = 100;
        internal const double Epsilon = 0.01;
        private readonly IChromosomeFactory m_ChromosomeFactory;
        private readonly IDistanceGraph m_Graph;
        private readonly IColonyLogger m_Logger;
        private readonly INaturalSelection m_NaturalSelection;
        private readonly IOptimizer m_Optimizer;
        private readonly IQueen m_Queen;
        private readonly IDateTime m_SystemTime;
        private readonly IPheromonesTracker m_Tracker;
        private readonly ITrailBuilderFactory m_TrailBuilderFactory;
        public bool IsRequestedToStop { get; private set; }

        [NotNull]
        internal ITrailBuilder ColonyBestTrailBuilder { get; private set; }

        public ITrailHistory TrailHistory
        {
            get
            {
                return m_NaturalSelection.TrailHistory;
            }
        }

        public DateTime StartTime { get; private set; }

        public DateTime FinishTime { get; private set; }

        public int TurnsSinceNewBestTrailFound { get; private set; }

        public int TurnsRemaining { get; private set; }

        public void Stop()
        {
            IsRequestedToStop = true;
        }

        public PheromonesInformation PheromonesInformation()
        {
            var information = new PheromonesInformation
                              {
                                  Values = m_Tracker.PheromonesToArray(),
                                  Minimum = m_Tracker.MinimumValue,
                                  Maximum = m_Tracker.MaximumValue,
                                  Average = m_Tracker.AverageValue
                              };

            return information;
        }

        public event EventHandler <BestTrailChangedEventArgs> BestTrailChanged;
        public event EventHandler <FinishedEventArgs> Finished;
        public event EventHandler Stopped;
        public event EventHandler Started;

        [NotNull]
        private ITrailBuilder CreateUnknownTrailBuilder([NotNull] IPheromonesTracker tracker)
        {
            ITrailBuilder builder = m_TrailBuilderFactory.Create <IUnknownTrailBuilder>(Chromosome.Unknown,
                                                                                        tracker,
                                                                                        m_Graph,
                                                                                        m_Optimizer,
                                                                                        new int[]
                                                                                        {
                                                                                        });

            return builder;
        }

        [NotNull]
        private IEnumerable <int> CreateDefaultTrail(int numberOfNodes)
        {
            var nodes = new List <int>();

            for ( var i = 0 ; i < numberOfNodes ; i += 2 )
            {
                nodes.Add(i);
            }

            return nodes.ToArray();
        }

        [NotNull]
        private ITrailBuilder CreateFixedTrailBuilderWithDefaultTrail([NotNull] IPheromonesTracker tracker,
                                                                      [NotNull] IEnumerable <int> trail)
        {
            ITrailBuilder builder = m_TrailBuilderFactory.Create <IFixedTrailBuilder>(Chromosome.Unknown,
                                                                                      tracker,
                                                                                      m_Graph,
                                                                                      m_Optimizer,
                                                                                      trail);

            return builder;
        }

        internal void Cycle(int times)
        {
            PreCycle();

            StartTime = m_SystemTime.Now;

            DoCycles(times);

            FinishTime = m_SystemTime.Now;

            PostCycle();
        }

        internal void PreCycle()
        {
            CycleInitialize();
            OnStarted();
        }

        private void OnStarted()
        {
            EventHandler handler = Started;
            if ( handler != null )
            {
                handler(this,
                        EventArgs.Empty);
            }
        }

        internal void PostCycle()
        {
            SendBestTrailMessage(ColonyBestTrailBuilder);

            if ( IsRequestedToStop )
            {
                OnStopped();
                m_Logger.Info("Colony Stopped!");
            }
            else
            {
                RaiseFinishedEvent();
                m_Logger.Info("Colony Finished!");
            }
        }

        private void RaiseFinishedEvent()
        {
            var finishedEventArgs = new FinishedEventArgs
                                    {
                                        Times = Time,
                                        StartTime = StartTime,
                                        FinishTime = FinishTime
                                    };

            OnFinished(finishedEventArgs);
        }

        private void DoCycles(int times)
        {
            IsRequestedToStop = false;
            Time = 1;

            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while ( Time < times &&
                    !IsRequestedToStop )
            {
                TurnsRemaining--;
                TurnsSinceNewBestTrailFound--;

                Evolve();

                m_Queen.UpdateAnts();

                LogBestTrailBuilder("Best trail so far");

                if ( m_Queen.BestTrailBuilder.Length < ColonyBestTrailBuilder.Length )
                {
                    FoundNewBestTrail(m_Queen.BestTrailBuilder);
                }

                Time++;
            }
        }

        private void LogBestTrailBuilder([NotNull] string prefix)
        {
            var information = new LogTrailBuilderInformation(prefix,
                                                             Time,
                                                             ColonyBestTrailBuilder,
                                                             TurnsSinceNewBestTrailFound,
                                                             TurnsRemaining);

            m_Logger.LogTrailBuilder(information);
        }

        private void Evolve()
        {
            if ( TurnsSinceNewBestTrailFound <= 0 ) // If it's time to evolve the population...
            {
                EvolveRestartFromBestTrail(m_Queen);
            }
            else if ( TurnsRemaining <= 0 ) // If it's time to evolve the population...
            {
                EvolveNaturalSelection();
            }
        }

        private void FoundNewBestTrail([NotNull] ITrailBuilder bestTrailBuilder)
        {
            if ( IsInvalidTrail(bestTrailBuilder) )
            {
                NewBestTrailIsInvalid(bestTrailBuilder);
            }
            else
            {
                NewBestTrailIsValid(bestTrailBuilder);
            }
        }

        private void NewBestTrailIsValid([NotNull] ITrailBuilder bestTrailBuilder)
        {
            TurnsSinceNewBestTrailFound = TurnsBeforeSelection * 2;
            TurnsRemaining = TurnsBeforeSelection;

            m_TrailBuilderFactory.Release(ColonyBestTrailBuilder);
            ColonyBestTrailBuilder = bestTrailBuilder.Clone(m_TrailBuilderFactory,
                                                            m_ChromosomeFactory);

            m_NaturalSelection.TrailHistory.AddTrailInformation(ColonyBestTrailBuilder,
                                                                m_Queen.Settings,
                                                                Time);

            LogBestTrailBuilder("New best trail");

            SendBestTrailMessage(ColonyBestTrailBuilder);
        }

        internal void NewBestTrailIsInvalid([NotNull] ITrailBuilder bestTrailBuilder)
        {
            string trailText = string.Join(",",
                                           bestTrailBuilder.Trail);

            m_Logger.Error("Found invalid trail: [IsUnknown: {0}] {1}".Inject(bestTrailBuilder.IsUnknown,
                                                                              trailText));
        }

        internal bool IsInvalidTrail([NotNull] ITrailBuilder bestTrailBuilder)
        {
            return bestTrailBuilder.IsUnknown || !m_Graph.IsValidPath(bestTrailBuilder.Trail);
        }

        private void EvolveNaturalSelection()
        {
            m_Logger.Info("[Time: {0:D5}] Natural Selection!".Inject(Time));

            m_NaturalSelection.DoSelection();

            TurnsRemaining = TurnsBeforeSelection;
        }

        internal void EvolveRestartFromBestTrail([NotNull] IQueen queen)
        {
            m_Logger.Info("[Time: {0:D5}] RestartFromTrail Selection!".Inject(Time));

            queen.RestartFromTrail(ColonyBestTrailBuilder.Trail);

            TurnsSinceNewBestTrailFound = TurnsBeforeSelection * 2;
            TurnsRemaining = TurnsBeforeSelection;
        }

        internal void CycleInitialize()
        {
            // Attention: default trail could be invalid, thats why we set to UnknownTrailBuilder first
            ColonyBestTrailBuilder = CreateUnknownTrailBuilder(m_Tracker);

            SetColonyBestTrailBuilderToDefaultTrail(m_Graph,
                                                    m_Tracker);
        }

        internal void SendBestTrailMessage([NotNull] ITrailBuilder trailBuilder)
        {
            IChromosome chromosome = trailBuilder.Chromosome;

            var bestTrailChangedEventArgs = new BestTrailChangedEventArgs
                                            {
                                                Iteration = Time,
                                                Trail = trailBuilder.Trail,
                                                Length = trailBuilder.Length,
                                                AntType = trailBuilder.Type,
                                                Alpha = chromosome.Alpha,
                                                Beta = chromosome.Beta,
                                                Gamma = chromosome.Gamma
                                            };

            OnBestTrailChanged(bestTrailChangedEventArgs);
        }

        [UsedImplicitly]
        public void RandomSelection()
        {
            TurnsRemaining = NumberOfNodes; // TurnsBeforeSelection;
            m_Queen.RandomSelection();
        }

        private void OnBestTrailChanged([NotNull] BestTrailChangedEventArgs eventArgs)
        {
            EventHandler <BestTrailChangedEventArgs> handler = BestTrailChanged;
            if ( handler != null )
            {
                handler(this,
                        eventArgs);
            }
        }

        private void OnFinished([NotNull] FinishedEventArgs eventArgs)
        {
            EventHandler <FinishedEventArgs> handler = Finished;
            if ( handler != null )
            {
                handler(this,
                        eventArgs);
            }
        }

        private void OnStopped()
        {
            EventHandler handler = Stopped;
            if ( handler != null )
            {
                handler(this,
                        EventArgs.Empty);
            }
        }

        // ReSharper disable TooManyDependencies 
        public Colony([NotNull] IColonyLogger logger,
                      [NotNull] IDateTime systemTime,
                      [NotNull] IQueenFactory queenFactory,
                      [NotNull] IChromosomeFactory chromosomeFactory,
                      [NotNull] ITrailBuilderFactory trailBuilderFactory,
                      [NotNull] IPheromonesTrackerFactory trackerFactory,
                      [NotNull] IDistanceGraph graph,
                      [NotNull] IOptimizer optimizer,
                      [NotNull] INaturalSelectionFactory naturalSelectionFactory)
        {
            TurnsSinceNewBestTrailFound = DefaultTurnsBeforeSelection * 2;
            FinishTime = DateTime.Now;
            StartTime = DateTime.Now;
            TurnsBeforeSelection = DefaultTurnsBeforeSelection;
            m_SystemTime = systemTime;
            m_ChromosomeFactory = chromosomeFactory;
            m_TrailBuilderFactory = trailBuilderFactory;
            m_Graph = graph;
            m_Optimizer = optimizer;
            m_Logger = logger;
            m_Tracker = trackerFactory.Create(m_Graph);

            ColonyBestTrailBuilder = CreateUnknownTrailBuilder(m_Tracker);
            TurnsRemaining = m_Graph.NumberOfNodes / 2;

            m_Graph = graph;
            m_Optimizer.DistanceGraph = graph;
            m_Queen = queenFactory.Create(m_Graph,
                                          m_Tracker,
                                          m_Optimizer);

            m_NaturalSelection = naturalSelectionFactory.Create(m_Queen);

            m_Logger.Info("Colony created with Distance Graph:\r\n{0}".Inject(m_Graph));
        }

        private void SetColonyBestTrailBuilderToDefaultTrail([NotNull] IDistanceGraph graph,
                                                             [NotNull] IPheromonesTracker tracker)
        {
            IEnumerable <int> defaultTrail = CreateDefaultTrail(graph.NumberOfNodes);

            ITrailBuilder builder = CreateFixedTrailBuilderWithDefaultTrail(tracker,
                                                                            defaultTrail);
            FoundNewBestTrail(builder);
        }

        // ReSharper restore TooManyDependencies

        #region IColony Members

        public int Time { get; private set; }

        public int TurnsBeforeSelection { get; set; }

        public int NumberOfNodes
        {
            get
            {
                return m_Queen.NumberOfNodes;
            }
        }

        public int NumberOfAnts
        {
            get
            {
                return m_Queen.NumberOfAnts;
            }
        }

        public IEnumerable <IAnt> Ants
        {
            get
            {
                return m_Queen.Ants;
            }
        }

        public ITrailBuilder BestTrailBuilder
        {
            get
            {
                return m_Queen.BestTrailBuilder;
            }
        }

        public IEnumerable <ITrailBuilder> Alternatives
        {
            get
            {
                return m_Queen.Alternatives;
            }
        }

        public TimeSpan Runtime
        {
            get
            {
                return FinishTime - StartTime;
            }
        }

        public double[][] PheromonesToArray()
        {
            return m_Tracker.PheromonesToArray();
        }

        public void Start(int times)
        {
            m_Logger.Info("Starting colony...");

            m_Queen.Clear();

            Cycle(times);

            m_Logger.LogResult(Runtime);
        }

        public double PheromonesMinimum
        {
            get
            {
                return m_Tracker.MinimumValue;
            }
        }

        public double PheromonesMaximum
        {
            get
            {
                return m_Tracker.MaximumValue;
            }
        }

        public double PheromonesAverage
        {
            get
            {
                return m_Tracker.AverageValue;
            }
        }

        #endregion
    }
}