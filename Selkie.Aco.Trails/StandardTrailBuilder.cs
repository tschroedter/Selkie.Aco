using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Trails.Interfaces;
using Selkie.Common.Interfaces;

namespace Selkie.Aco.Trails
{
    public class StandardTrailBuilder
        : BaseTrailBuilder <IStandardTrailBuilder>,
          IStandardTrailBuilder
    {
        // ReSharper disable once TooManyDependencies
        public StandardTrailBuilder([NotNull] IRandom random,
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

        // ReSharper disable once TooManyDependencies
        [UsedImplicitly]
        public StandardTrailBuilder([NotNull] IRandom random,
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

        internal override int NextCity(int cityX,
                                       [NotNull] bool[] visited,
                                       double dicider)
        {
            double[] probs = CalculateProbabilities(cityX,
                                                    visited);

            double[] cumul = CalculateCuMul(probs);

            for ( var i = 0 ; i < cumul.Length - 1 ; ++i )
            {
                if ( dicider >= cumul [ i ] &&
                     dicider < cumul [ i + 1 ] )
                {
                    return i;
                }
            }

            throw new ArgumentException("Failure to return valid city in NextCity");
        }
    }
}