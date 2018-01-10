using System.Collections.Generic;
using System.Text;
using Core2.Selkie.Aco.Anthill.Interfaces;
using Core2.Selkie.Aco.Ants;
using Core2.Selkie.Aco.Ants.Interfaces;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Common.Interfaces;
using Core2.Selkie.Windsor;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill
{
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class Squad : ISquad
    {
        // ReSharper disable TooManyDependencies
        public Squad([NotNull] IDisposer disposer,
                     [NotNull] ISelkieLogger logger,
                     [NotNull] IRandom random,
                     [NotNull] IAntFactory antFactory,
                     [NotNull] IDistanceGraph graph,
                     [NotNull] IPheromonesTracker tracker,
                     [NotNull] IOptimizer optimizer,
                     [NotNull] IAntSettings antSettings)
        {
            m_Disposer = disposer;
            m_Logger = logger;
            m_Random = random;
            m_AntFactory = antFactory;
            m_Graph = graph;
            m_Tracker = tracker;
            m_Optimizer = optimizer;
            m_AntSettings = antSettings;

            CreateAnts(DefaultNumberOfAnts);

            m_Disposer.AddResource(ReleaseAllAnts);
        }

        // ReSharper restore TooManyDependencies
        internal const int DefaultNumberOfAnts = 10;

        public IEnumerable <IAnt> BestAnts => m_BestAnts;

        private readonly IAntFactory m_AntFactory;
        private readonly IList <IAnt> m_Ants = new List <IAnt>();
        private readonly IAntSettings m_AntSettings;
        private readonly IList <IAnt> m_BestAnts = new List <IAnt>();
        private readonly IDisposer m_Disposer;
        private readonly IDistanceGraph m_Graph;
        private readonly ISelkieLogger m_Logger;
        private readonly IOptimizer m_Optimizer;
        private readonly IRandom m_Random;
        private readonly IPheromonesTracker m_Tracker;

        public int NumberOfAnts => m_Ants.Count;

        public IEnumerable <IAnt> Ants => m_Ants;

        public void Dispose()
        {
            m_Disposer.Dispose();
        }

        public void Restart()
        {
            ReleaseAnts(m_Ants);

            CreateAnts(DefaultNumberOfAnts);
        }

        public void Clear()
        {
            ReleaseAllAnts();

            CreateAnts(DefaultNumberOfAnts);
        }

        public void AddBestAnt(IAnt ant)
        {
            m_BestAnts.Add(ant);
        }

        public void SetNumberOfAnts(int numberOfAnts)
        {
            if ( numberOfAnts < 1 )
            {
                m_Logger.Error($"Can't set NumberOfAnts to {numberOfAnts}!");

                return;
            }

            ReleaseAllAnts();
            CreateAnts(numberOfAnts);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"Number of Ants: {m_Ants.Count} ");
            sb.AppendLine($"Number of Best Ants: {m_BestAnts.Count}");

            var counter = 0;

            foreach ( IAnt ant in m_Ants )
            {
                sb.AppendLine($"[{counter++}] {ant.Type} {ant.Chromosome}");
            }

            return sb.ToString();
        }

        internal void CreateAnts(int numberOfAnts)
        {
            CreateNewAnts(numberOfAnts);
            AddBestAnts();
        }

        internal void ReleaseAllAnts()
        {
            ReleaseAnts(m_Ants);
            ReleaseAnts(m_BestAnts);

            m_Ants.Clear();
            m_BestAnts.Clear();
        }

        internal void ReleaseAnts([NotNull] IEnumerable <IAnt> ants)
        {
            foreach ( IAnt ant in ants )
            {
                m_AntFactory.Release(ant);
            }
        }

        private void AddBestAnts()
        {
            foreach ( IAnt ant in m_BestAnts )
            {
                m_Ants.Add(ant);
            }
        }

        private void CreateNewAnts(int numberOfAnts)
        {
            m_Ants.Clear();

            for ( var i = 0 ; i < numberOfAnts ; i++ )
            {
                IAnt ant = i % 2 == 0
                               ? m_AntFactory.Create <IStandardAnt>(new Chromosome(m_Random),
                                                                    m_Tracker,
                                                                    m_Graph,
                                                                    m_Optimizer,
                                                                    m_AntSettings,
                                                                    new int[0])
                               : m_AntFactory.Create <ICandidateListAnt>(new Chromosome(m_Random),
                                                                         m_Tracker,
                                                                         m_Graph,
                                                                         m_Optimizer,
                                                                         m_AntSettings,
                                                                         new int[0]) as IAnt;

                m_Ants.Add(ant);
            }
        }
    }
}