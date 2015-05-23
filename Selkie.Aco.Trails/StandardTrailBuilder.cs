using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Common;

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

        // ReSharper disable once MethodTooLong
        public override void BuildTrail(int start)
        {
            if ( start >= DistanceGraph.NumberOfNodes ||
                 start < 0 )
            {
                throw new ArgumentException("start = " + start);
            }

            int reverseStart = FindRelatedCity(start);

            var trail = new int[DistanceGraph.NumberOfUniqueNodes];
            var visited = new bool[DistanceGraph.NumberOfNodes];

            trail [ 0 ] = start;

            visited [ start ] = true;
            visited [ reverseStart ] = true;

            for ( var i = 0 ; i < DistanceGraph.NumberOfUniqueNodes - 1 ; ++i )
            {
                int cityX = trail [ i ];
                double dicider = Random.NextDouble();
                int next = NextCity(cityX,
                                    visited,
                                    dicider);
                trail [ i + 1 ] = next;
                visited [ next ] = true;

                int nextRelatedCity = FindRelatedCity(next);
                visited [ nextRelatedCity ] = true;
            }

            Trail = trail;
        }

        internal int NextCity(int cityX,
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