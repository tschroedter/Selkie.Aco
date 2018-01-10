using System;
using System.Collections.Generic;
using System.Linq;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Trails
{
    public class RandomTrailBuilder
        : BaseTrailBuilder <IRandomTrailBuilder>,
          IRandomTrailBuilder
    {
        [NotNull]
        [UsedImplicitly]
        internal static int[] Create(int numberOfNodes)
        {
            var trail = new int[numberOfNodes];

            for ( var i = 0 ; i < numberOfNodes ; ++i )
            {
                trail [ i ] = i;
            }

            return trail;
        }

        [NotNull]
        [UsedImplicitly]
        internal static int[] RemoveReverseNodes([NotNull] IEnumerable <int> trail,
                                                 int startNode)
        {
            var reverseIds = new List <int>();

            IEnumerable <int> nodeIds = trail as int[] ?? trail.ToArray();

            foreach ( int currentId in nodeIds )
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

            IEnumerable <int> trails = nodeIds.Where(id => !reverseIds.Contains(id));

            return trails.ToArray();
        }

        internal override void BuildTrail(int startNode)
        {
            Trail = CreateTrail(startNode,
                                DistanceGraph.NumberOfNodes);
        }

        [NotNull]
        [UsedImplicitly]
        internal int[] CreateTrail(int startNode,
                                   int numberOfNodes)
        {
            int[] trail = Create(numberOfNodes);

            trail = Randomize(trail,
                              numberOfNodes);

            trail = RemoveReverseNodes(trail,
                                       startNode);
            BuildDictionaryIndexOfTarget(trail);

            trail = MoveNodeToStartPosition(trail,
                                            startNode);

            return trail;
        }

        [UsedImplicitly]
        internal int[] MoveNodeToStartPosition([NotNull] IEnumerable <int> trail,
                                               int nodeIndex)
        {
            int[] nodeIds = trail as int[] ?? trail.ToArray();

            int index = Array.IndexOf(nodeIds,
                                      nodeIndex);
            int temp = nodeIds [ 0 ];
            nodeIds [ 0 ] = nodeIds [ index ];
            nodeIds [ index ] = temp;

            return nodeIds;
        }

        [UsedImplicitly]
        internal int[] Randomize([NotNull] IEnumerable <int> trail,
                                 int numberOfNodes)
        {
            int[] randomTrail = trail.ToArray();

            for ( var i = 0 ; i < numberOfNodes ; ++i )
            {
                int r = Random.Next(i,
                                    numberOfNodes);

                int tmp = randomTrail [ r ];

                randomTrail [ r ] = randomTrail [ i ];
                randomTrail [ i ] = tmp;
            }

            return randomTrail;
        }

        [UsedImplicitly]
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

        [UsedImplicitly]
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
    }
}