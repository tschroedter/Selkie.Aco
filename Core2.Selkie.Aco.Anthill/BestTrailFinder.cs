using System;
using System.Collections.Generic;
using Core2.Selkie.Aco.Anthill.Interfaces;
using Core2.Selkie.Aco.Ants;
using Core2.Selkie.Aco.Ants.Interfaces;
using Core2.Selkie.Aco.Common;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Common.Interfaces;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill
{
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class BestTrailFinder : IBestTrailFinder
    {
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
            BestAnt = CreateUnknownAnt();

            m_Disposer.AddResource(() => m_AntFactory.Release(BestAnt));

            Settings = new Settings(BestAnt,
                                    m_Tracker);
        }

        private const double Epsilon = 0.01;
        private readonly IAntFactory m_AntFactory;
        private readonly IDisposer m_Disposer;
        private readonly IDistanceGraph m_Graph;
        private readonly IOptimizer m_Optimizer;
        private readonly IPheromonesTracker m_Tracker;
        private readonly ITrailAlternatives m_TrailAlternatives;

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
            BestAnt = CreateUnknownAnt();

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

        [NotNull]
        private IUnknownAnt CreateUnknownAnt()
        {
            return m_AntFactory.Create <IUnknownAnt>(Chromosome.Unknown,
                                                     m_Tracker,
                                                     m_Graph,
                                                     m_Optimizer,
                                                     AntSettings.Unknown,
                                                     new int[0]);
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