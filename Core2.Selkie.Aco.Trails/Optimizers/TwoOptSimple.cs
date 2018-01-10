using System.Collections.Generic;
using System.Linq;
using Core2.Selkie.Aco.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Trails.Optimizers
{
    public sealed class TwoOptSimple : ITwoOptSimple
    {
        private IDistanceGraph m_DistanceGraph = Common.DistanceGraph.Unknown;

        public IDistanceGraph DistanceGraph
        {
            private get
            {
                return m_DistanceGraph;
            }
            set
            {
                m_DistanceGraph = value;
            }
        }

        // ReSharper disable once MethodTooLong
        public IEnumerable <int> Optimize(IEnumerable <int> trail)
        {
            int[] trailArray = trail.ToArray();

            if ( trailArray.Length == 0 )
            {
                return new int[0];
            }

            var bestImprovement = 0;
            var bestI = 0;
            var bestJ = 0;

            do
            {
                // we only need to iterate through the first half of the edges,
                // since the rest would be redundant
                for ( var i = 0 ; i < trailArray.Length / 2 + 1 ; i++ )
                {
                    int firstEdgeU = i; // u represents the first vertex in the edge
                    int firstEdgeV = i + 1; // v represents the second vertex in the edge
                    // based on the second edge's v, we should stop 3 sooner

                    for ( var j = 0 ; j < trailArray.Length - 3 ; j++ )
                    {
                        int secondEdgeU = ( i + j + 2 ) % trailArray.Length;
                        int secondEdgeV = ( i + j + 3 ) % trailArray.Length;

                        int index0 = trailArray [ firstEdgeU ];
                        int index1 = trailArray [ firstEdgeV ];
                        int index2 = trailArray [ secondEdgeU ];
                        int index3 = trailArray [ secondEdgeV ];

                        int oldEdgeDists = DistanceGraph.GetCost(index0,
                                                                 index1) + DistanceGraph.GetCost(index2,
                                                                                                 index3);

                        int newEdgeDists = DistanceGraph.GetCost(index0,
                                                                 index2) + DistanceGraph.GetCost(index1,
                                                                                                 index3);

                        int improvement = oldEdgeDists - newEdgeDists;

                        if ( improvement <= bestImprovement )
                        {
                            continue;
                        }

                        bestImprovement = improvement;
                        bestI = firstEdgeU;
                        bestJ = secondEdgeU;
                    }

                    if ( bestImprovement <= 0 )
                    {
                        continue;
                    }

                    // swap the "middle" vertices
                    Swap(trailArray,
                         ( bestI + 1 ) % trailArray.Length,
                         bestJ);
                    bestImprovement = 0;
                }
            }
            while ( bestImprovement > 0 );

            return trailArray;
        }

        internal static void Swap([NotNull] int[] trail,
                                  int i,
                                  int j)
        {
            int temp = trail [ i ];
            trail [ i ] = trail [ j ];
            trail [ j ] = temp;
        }
    }
}