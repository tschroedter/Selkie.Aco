using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Common;

namespace Selkie.Aco.Trails
{
    public class UnknownTrailBuilder
        : BaseTrailBuilder <IUnknownTrailBuilder>,
          IUnknownTrailBuilder
    {
        public override ITrailBuilder Clone(ITrailBuilderFactory trailBuilderFactory,
                                            IChromosomeFactory chromosomeFactory)
        {
            return this;
        }

        public override void BuildTrail(int start)
        {
        } // ReSharper disable TooManyDependencies

        public UnknownTrailBuilder([NotNull] IRandom random,
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
            Id = UnknownId;
            Trail = new int[0];
        }

        // ReSharper restore TooManyDependencies
        [UsedImplicitly]
        public UnknownTrailBuilder([NotNull] IRandom random,
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
            Id = UnknownId;
            Trail = trail.ToArray();
        }

        // ReSharper restore TooManyDependencies
    }
}