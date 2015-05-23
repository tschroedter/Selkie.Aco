using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails;
using Selkie.Common;

namespace Selkie.Aco.Ants
{
    public sealed class UnknownAnt
        : BaseAnt <IUnknownAnt, IUnknownTrailBuilder>,
          IUnknownAnt
    {
        // ReSharper disable once TooManyDependencies
        public UnknownAnt([NotNull] IRandom random,
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

        public override void Update()
        {
        }

        public override IAnt Clone(IAntFactory antFactory,
                                   IChromosomeFactory chromosomeFactory)
        {
            return this;
        }
    }
}