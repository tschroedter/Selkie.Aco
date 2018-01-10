using System.Collections.Generic;
using System.Linq;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Trails
{
    public class FixedTrailBuilder
        : BaseTrailBuilder <IFixedTrailBuilder>,
          IFixedTrailBuilder
    {
        [UsedImplicitly]
        public FixedTrailBuilder([NotNull] IRandom random,
                                 [NotNull] IChromosome chromosome,
                                 [NotNull] IPheromonesTracker tracker,
                                 [NotNull] IDistanceGraph graph,
                                 [NotNull] IOptimizer optimizer)
            : base(random,
                   chromosome,
                   tracker,
                   graph,
                   optimizer)
        {
        }

        [UsedImplicitly]
        public FixedTrailBuilder([NotNull] IRandom random,
                                 [NotNull] IChromosome chromosome,
                                 [NotNull] IPheromonesTracker tracker,
                                 [NotNull] IDistanceGraph graph,
                                 [NotNull] IOptimizer optimizer,
                                 [NotNull] IEnumerable <int> trail)
            : base(random,
                   chromosome,
                   tracker,
                   graph,
                   optimizer)
        {
            Trail = trail.ToArray();
            Length = CalculateLength(Trail);
            BuildDictionaryIndexOfTarget(Trail);
        }

        internal override void BuildTrail(int startNode)
        {
        }
    }
}