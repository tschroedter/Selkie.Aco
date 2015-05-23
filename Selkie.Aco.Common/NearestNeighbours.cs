using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Windsor;

namespace Selkie.Aco.Common
{
    [ProjectComponent(Lifestyle.Transient)]
    public class NearestNeighbours : INearestNeighbours
    {
        public static readonly INearestNeighbours Unknown = new NearestNeighbours(true);
        private readonly bool m_IsUnknown;
        private int[][] m_NearestNeighbours;

        public NearestNeighbours(bool isUnknown = false)
        {
            m_IsUnknown = isUnknown;

            m_NearestNeighbours = new int[0][];
        }

        public bool IsUnknown
        {
            get
            {
                return m_IsUnknown;
            }
        }

        public void Initialize(int[][] costMatrix)
        {
            m_NearestNeighbours = CalculateNearestNeighbours(costMatrix);
        }

        public IEnumerable <int> GetNeighbours(int index)
        {
            return m_IsUnknown
                       ? new int[0]
                       : m_NearestNeighbours [ index ];
        }

        [NotNull]
        public static int[][] CalculateNearestNeighbours([NotNull] int[][] costMatrix)
        {
            int size = costMatrix.Length;

            var neighbours = new int[size][];

            for ( var i = 0 ; i < size ; i++ )
            {
                var tuples = new List <Tuple <int, double>>();

                for ( var j = 0 ; j < size ; j++ )
                {
                    var tuple = new Tuple <int, double>(j,
                                                        costMatrix [ i ] [ j ]);

                    tuples.Add(tuple);
                }

                int currentNodeId = i;

                IEnumerable <int> nodeIds = from tuple1 in tuples
                                            orderby tuple1.Item2
                                            where tuple1.Item1 != currentNodeId
                                            select tuple1.Item1;

                neighbours [ i ] = nodeIds.ToArray();
            }

            return neighbours;
        }
    }
}