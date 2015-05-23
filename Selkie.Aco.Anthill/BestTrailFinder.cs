using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Ants;
using Selkie.Aco.Common;
using Selkie.Aco.Trails;
using Selkie.Common;
using Selkie.Windsor;

namespace Selkie.Aco.Anthill
{
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class BestTrailFinder : IBestTrailFinder
    {
        private const double Epsilon = 0.01;
        private readonly IAntFactory m_AntFactory;
        private readonly IDisposer m_Disposer;
        private readonly IDistanceGraph m_Graph;
        private readonly IOptimizer m_Optimizer;
        private readonly IPheromonesTracker m_Tracker;
        private readonly ITrailAlternatives m_TrailAlternatives;

        public BestTrailFinder([NotNull] IDisposer disposer,
                               [NotNull] IAntFactory antFactory,
                               [NotNull] IDistanceGraph graph,
                               [NotNull] IPheromonesTracker tracker,
                               [NotNull] IOptimizer optimizer,
                               [NotNull] ITrailAlternatives trailAlternatives)
        {
            m_Disposer = disposer;
            m_AntFactory = antFactory;
            m_Graph = graph;
            m_Tracker = tracker;
            m_Optimizer = optimizer;
            m_TrailAlternatives = trailAlternatives;
            BestAnt = m_AntFactory.Create <IUnknownAnt>(Chromosome.Unknown,
                                                        m_Tracker,
                                                        m_Graph,
                                                        m_Optimizer,
                                                        new int[0]);

            m_Disposer.AddResource(() => m_AntFactory.Release(BestAnt));

            Settings = new Settings(BestAnt,
                                    m_Tracker);
        }

        public ISettings Settings { get; private set; }

        public IEnumerable <ITrailBuilder> AlternativeTrails
        {
            get
            {
                return m_TrailAlternatives.Trails;
            }
        }

        public ITrailBuilder BestTrailBuilder
        {
            get
            {
                return BestAnt.TrailBuilder;
            }
        }

        public IAnt BestAnt { get; private set; }

        public void Clear()
        {
            BestAnt = m_AntFactory.Create <IUnknownAnt>(Chromosome.Unknown,
                                                        m_Tracker,
                                                        m_Graph,
                                                        m_Optimizer,
                                                        new int[0]);

            m_Disposer.AddResource(() => m_AntFactory.Release(BestAnt));

            m_TrailAlternatives.Clear();
        }

        public void FindBestTrail(IEnumerable <IAnt> ants)
        {
            double bestLength = double.MaxValue;

            if ( !BestAnt.TrailBuilder.IsUnknown )
            {
                bestLength = BestTrailBuilder.Length;
            }

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach ( IAnt currentAnt in ants )
            {
                bestLength = FindBestLengthForCurrentAnt(currentAnt,
                                                         bestLength);
            }
        }

        public void Dispose()
        {
            m_Disposer.Dispose();
        }

        private double FindBestLengthForCurrentAnt([NotNull] IAnt currentAnt,
                                                   double bestLength)
        {
            double length = currentAnt.TrailBuilder.Length;

            if ( length < bestLength )
            {
                bestLength = length;

                BestAnt = currentAnt;

                m_TrailAlternatives.Clear();
                m_TrailAlternatives.AddAlternative(currentAnt.Id,
                                                   currentAnt.TrailBuilder);
                Settings = new Settings(currentAnt,
                                        m_Tracker);
            }
            else
            {
                if ( Math.Abs(length - bestLength) < Epsilon )
                {
                    m_TrailAlternatives.AddAlternative(currentAnt.Id,
                                                       currentAnt.TrailBuilder);
                }
            }
            return bestLength;
        }
    }
}