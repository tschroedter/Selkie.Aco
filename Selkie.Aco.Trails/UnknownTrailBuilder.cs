using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails.Interfaces;
using Selkie.Common.Interfaces;

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

        internal override void BuildTrail(int startNode)
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