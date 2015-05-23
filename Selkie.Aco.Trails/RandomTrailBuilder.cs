using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Common;

namespace Selkie.Aco.Trails
{
    public class RandomTrailBuilder
        : BaseTrailBuilder <IRandomTrailBuilder>,
          IRandomTrailBuilder
    {
        // ReSharper disable TooManyDependencies
        public RandomTrailBuilder([NotNull] IRandom random,
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
        public RandomTrailBuilder([NotNull] IRandom random,
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

        // ReSharper restore TooManyDependencies
        public override void BuildTrail(int start)
        {
            if ( start >= DistanceGraph.NumberOfNodes ||
                 start < 0 )
            {
                throw new ArgumentException("start = " + start);
            }

            Trail = CreateTrail(start,
                                DistanceGraph.NumberOfNodes);
        }

        [NotNull]
        internal int[] CreateTrail(int startNode,
                                   int numberOfNodes)
        {
            if ( numberOfNodes <= 1 ||
                 numberOfNodes % 2 != 0 )
            {
                throw new ArgumentException("numberOfNodes is " + numberOfNodes);
            }

            int[] trail = Create(numberOfNodes);

            Randomize(trail,
                      numberOfNodes);
            trail = RemoveReverseNodes(trail,
                                       startNode);
            BuildDictionaryIndexOfTarget(trail);
            MoveNodeToStart(trail,
                            startNode);

            return trail;
        }

        [NotNull]
        internal static int[] RemoveReverseNodes([NotNull] int[] trail,
                                                 int startNode)
        {
            var reverseIds = new List <int>();

            foreach ( int currentId in trail )
            {
                if ( reverseIds.Contains(currentId) )
                {
                    continue;
                }

                int reverseId = FindRelatedCity(currentId);

                if ( reverseId == startNode )
                {
                    continue;
                }

                reverseIds.Add(reverseId);
            }

            IEnumerable <int> trails = trail.Where(id => !reverseIds.Contains(id));

            return trails.ToArray();
        }

        internal void MoveNodeToStart([NotNull] int[] trail,
                                      int nodeIndex)
        {
            int index = IndexOfTarget(nodeIndex);
            int temp = trail [ 0 ];
            trail [ 0 ] = trail [ index ];
            trail [ index ] = temp;
        }

        [NotNull]
        internal static int[] Create(int numberOfNodes)
        {
            var trail = new int[numberOfNodes];

            for ( var i = 0 ; i < numberOfNodes ; ++i )
            {
                trail [ i ] = i;
            }

            return trail;
        }

        internal void Randomize([NotNull] int[] trail,
                                int numberOfNodes)
        {
            for ( var i = 0 ; i < numberOfNodes ; ++i )
            {
                int r = Random.Next(i,
                                    numberOfNodes);

                int tmp = trail [ r ];

                trail [ r ] = trail [ i ];
                trail [ i ] = tmp;
            }
        }
    }
}