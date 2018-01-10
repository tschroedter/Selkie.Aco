﻿using System.Collections.Generic;
using Core2.Selkie.Aco.Ants.Interfaces;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Common.TypedFactories;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Ants
{
    public sealed class FixedAnt
        : BaseAnt <IFixedAnt, IFixedTrailBuilder>,
          IFixedAnt
    {
        // ReSharper disable TooManyDependencies
        public FixedAnt([NotNull] IRandom random,
                        [NotNull] ITrailBuilderFactory trailBuilderFactory,
                        [NotNull] IChromosome chromosome,
                        [NotNull] IPheromonesTracker tracker,
                        [NotNull] IDistanceGraph graph,
                        [NotNull] IOptimizer optimizer,
                        [NotNull] IAntSettings antSettings,
                        [NotNull] IEnumerable <int> trail)
            : base(random,
                   trailBuilderFactory,
                   chromosome,
                   tracker,
                   graph,
                   optimizer,
                   antSettings,
                   trail)
        {
        }

        // ReSharper restore TooManyDependencies

        protected override void UpdateWithFixedStartNode(int startNode)
        {
        }

        protected override void UpdateWithRandomStartNode()
        {
        }
    }
}