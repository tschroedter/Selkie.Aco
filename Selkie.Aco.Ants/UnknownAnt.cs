using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Ants.Interfaces;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails.Interfaces;
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

        public override IAnt Clone(IAntFactory antFactory,
                                   IChromosomeFactory chromosomeFactory)
        {
            return this;
        }

        protected override void UpdateWithRandomStartNode()
        {
        }

        protected override void UpdateWithFixedStartNode(int startNode)
        {
        }
    }
}