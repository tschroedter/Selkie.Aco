using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails;
using Selkie.Common;

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
            int startNode = Random.Next(0,
                                        DistanceGraph.NumberOfNodes - 1);

            TrailBuilder.Build(startNode);
        }
    }
}