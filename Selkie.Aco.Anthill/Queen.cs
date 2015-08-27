using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Aco.Anthill.TypedFactories;
using Selkie.Aco.Ants;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Aco.Anthill
{
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class Queen : IQueen
    {
        private readonly IAntFactory m_AntFactory;
        private readonly IBestTrailFinder m_BestTrailFinder;
        private readonly IChromosomeFactory m_ChromosomeFactory;
        private readonly ICrossover m_Crossover;
        private readonly IDistanceGraph m_Graph;
        private readonly ISelkieLogger m_Logger;
        private readonly IOptimizer m_Optimizer;
        private readonly ISquad m_Squad;
        private readonly IPheromonesTracker m_Tracker;
        // ReSharper disable TooManyDependencies
        public Queen([NotNull] ISelkieLogger logger,
                     [NotNull] IAntFactory antFactory,
                     [NotNull] IChromosomeFactory chromosomeFactory,
                     [NotNull] IBestTrailFinderFactory bestTrailFinderFactory,
                     [NotNull] IDistanceGraph graph,
                     [NotNull] IPheromonesTracker tracker,
                     [NotNull] IOptimizer optimizer,
                     [NotNull] ICrossover crossover,
                     [NotNull] ISquadFactory squadFactory)
        {
            m_Logger = logger;
            m_AntFactory = antFactory;
            m_ChromosomeFactory = chromosomeFactory;
            m_Graph = graph;
            m_Tracker = tracker;
            m_Optimizer = optimizer;
            m_Crossover = crossover;

            m_Squad = squadFactory.Create(m_Graph,
                                          m_Tracker,
                                          m_Optimizer);

            m_BestTrailFinder = bestTrailFinderFactory.Create(m_Graph,
                                                              m_Tracker,
                                                              m_Optimizer);

            TotalBestAnt = m_AntFactory.Create <IUnknownAnt>(Chromosome.Unknown,
                                                             m_Tracker,
                                                             m_Graph,
                                                             m_Optimizer,
                                                             new int[0]);
        }

        // ReSharper restore TooManyDependencies
        internal void UpdateAnts([NotNull] IEnumerable <IAnt> ants)
        {
            IAnt[] antsArray = ants.ToArray();

            foreach ( IAnt ant in antsArray )
            {
                ant.Update();
            }

            m_BestTrailFinder.FindBestTrail(antsArray);

            UpdateBestAnt();
        }

        internal void UpdateBestAnt()
        {
            ITrailBuilder totalTrailBuilder = TotalBestAnt.TrailBuilder;
            ITrailBuilder trailBuilder = m_BestTrailFinder.BestAnt.TrailBuilder;

            if ( TotalBestAnt.Type == typeof ( IUnknownAnt ).Name ||
                 totalTrailBuilder.Length > trailBuilder.Length )
            {
                TotalBestAnt = m_BestTrailFinder.BestAnt.Clone(m_AntFactory,
                                                               m_ChromosomeFactory);

                m_Squad.AddBestAnt(TotalBestAnt);
            }

            m_Tracker.Update(TotalBestAnt);
        }

        #region IQueen Members

        public ISettings Settings
        {
            get
            {
                return m_BestTrailFinder.Settings;
            }
        }

        public IEnumerable <IAnt> Ants
        {
            get
            {
                return m_Squad.Ants;
            }
        }

        public int NumberOfAnts
        {
            get
            {
                return m_Squad.NumberOfAnts;
            }
        }

        public IEnumerable <ITrailBuilder> Alternatives
        {
            get
            {
                return m_BestTrailFinder.AlternativeTrails;
            }
        }

        public IAnt BestAnt
        {
            get
            {
                return m_BestTrailFinder.BestAnt;
            }
        }

        public ITrailBuilder BestTrailBuilder
        {
            get
            {
                return m_BestTrailFinder.BestTrailBuilder;
            }
        }

        public int NumberOfNodes
        {
            get
            {
                return m_Graph.NumberOfNodes;
            }
        }

        public void UpdateAnts()
        {
            UpdateAnts(m_Squad.Ants);
        }

        public void Clear()
        {
            m_BestTrailFinder.Clear();
            m_Tracker.Clear();
            m_Squad.Clear();
        }

        public IAnt TotalBestAnt { get; private set; }

        public void RandomSelection()
        {
            foreach ( IAnt ant in m_Squad.Ants )
            {
                ant.RandomizeChromosome();
            }

            m_Logger.Debug("[Look] RandomSelection - {0}".Inject(m_Squad));
        }

        public void NaturalSelection(IChromosome male,
                                     IChromosome female)
        {
            m_Tracker.Clear();

            m_Tracker.Update(TotalBestAnt);

            UpdateAntsUsingOffspring(male,
                                     female);

            m_Logger.Debug("[Look] NaturalSelection - {0}".Inject(m_Squad));
        }

        public void UpdateChromosomes(IChromosome chromosome)
        {
            foreach ( IAnt ant in m_Squad.Ants )
            {
                ant.Chromosome = m_Crossover.Offspring(ant.Chromosome,
                                                       chromosome);
            }
        }

        public void RestartFromTrail(IEnumerable <int> trail)
        {
            m_Tracker.Clear();

            var ant = m_AntFactory.Create <IFixedAnt>(Chromosome.Unknown,
                                                      m_Tracker,
                                                      m_Graph,
                                                      m_Optimizer,
                                                      trail);

            m_Tracker.Update(new IAnt[]
                             {
                                 ant
                             });

            m_Squad.Restart();

            m_Logger.Debug("[Look] NaturalSelection - {0}".Inject(m_Squad));
        }

        internal void UpdateAntsUsingOffspring([NotNull] IChromosome male,
                                               [NotNull] IChromosome female)
        {
            foreach ( IAnt ant in m_Squad.Ants )
            {
                IChromosome chromosome = m_Crossover.Offspring(male,
                                                               female);
                chromosome = m_Crossover.Mutation(chromosome);
                ant.Chromosome = chromosome;
            }
        }

        #endregion
    }
}