using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Trails.Interfaces;
using Selkie.Common;

namespace Selkie.Aco.Trails
{
    public class FixedTrailBuilder
        : BaseTrailBuilder <IFixedTrailBuilder>,
          IFixedTrailBuilder
    {
        // ReSharper disable TooManyDependencies
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

        // ReSharper restore TooManyDependencies
        // ReSharper disable TooManyDependencies
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