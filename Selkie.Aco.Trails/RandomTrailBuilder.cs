using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Trails.Interfaces;
using Selkie.Common.Interfaces;

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

        [NotNull]
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
    }
}