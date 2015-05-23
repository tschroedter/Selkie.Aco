﻿using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails;
using Selkie.Common;

namespace Selkie.Aco.Ants
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
                        [NotNull] IEnumerable <int> trail)
            : base(random,
                   trailBuilderFactory,
                   chromosome,
                   tracker,
                   graph,
                   optimizer,
                   trail)
        {
        }

        // ReSharper restore TooManyDependencies
        public override void Update()
        {
        }
    }
}