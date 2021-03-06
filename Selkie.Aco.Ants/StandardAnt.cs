﻿using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Ants.Interfaces;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails.Interfaces;
using Selkie.Common.Interfaces;

namespace Selkie.Aco.Ants
{
    public class StandardAnt
        : BaseAnt <IStandardAnt, IStandardTrailBuilder>,
          IStandardAnt
    {
        // ReSharper disable once TooManyDependencies
        public StandardAnt([NotNull] IRandom random,
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
    }
}