using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Common;
using Core2.Selkie.Common.Interfaces;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

[assembly : InternalsVisibleTo("Core2.Selkie.ACO.Common.Tests")]

namespace Core2.Selkie.Aco.Common
{
    [ProjectComponent(Lifestyle.Transient)]
    public class DistanceGraph : IDistanceGraph
    {
        private DistanceGraph(bool isUnknown)
        {
            IsUnknown = isUnknown;
            m_Random = new SelkieRandom();
            m_NearestNeighbours = NearestNeighbours.Unknown;
            m_CostMatrix = new int[0][];
            m_CostPerLine = new int[0];
        }

        public DistanceGraph([NotNull] IRandom random,
                             [NotNull] INearestNeighbours nearestNeighbours,
                             [NotNull] int[][] costMatrix,
                             [NotNull] int[] costPerLine)
        {
            m_Random = random;
            m_NearestNeighbours = nearestNeighbours;
            m_CostMatrix = costMatrix;
            m_CostPerLine = costPerLine;

            m_NearestNeighbours.Initialize(costMatrix);
            NumberOfNodes = m_CostMatrix.Length;
            NumberOfUniqueNodes = m_CostMatrix.Length / 2;
            AverageDistance = CalculateAverageDistance();
            MinimumDistance = CalculateMinimumDistance();
            MaximumDistance = CalculateMaximumDistance();
        }

        [UsedImplicitly]
        public static readonly IDistanceGraph Unknown = new DistanceGraph(true);

        private readonly int[][] m_CostMatrix;
        private readonly int[] m_CostPerLine;
        private readonly INearestNeighbours m_NearestNeighbours;
        private readonly IRandom m_Random;

        public bool IsUnknown { get; }

        public bool IsValid()
        {
            return IsValidCombinationOfCostMatrixAndCostPerLine(m_CostMatrix,
                                                                m_CostPerLine);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine("CostMatrix");
            builder.AppendLine(CostMatrixToString(m_CostMatrix));

            builder.AppendLine("CostPerLine");
            builder.AppendLine(CostPerLineToString(m_CostPerLine));

            builder.AppendLine();

            builder.AppendLine("NearestNeighbours");
            builder.AppendLine(m_NearestNeighbours.ToString());

            builder.AppendLine($"IsValid? {IsValid()}");

            return builder.ToString();
        }

        [NotNull]
        [UsedImplicitly]
        internal int[][] CalculateNearestNeighbours()
        {
            int size = m_CostMatrix.Length;

            var neighbours = new int[size][];

            for ( var i = 0 ; i < size ; i++ )
            {
                var tuples = new List <Tuple <int, double>>();

                for ( var j = 0 ; j < size ; j++ )
                {
                    var tuple = new Tuple <int, double>(j,
                                                        m_CostMatrix [ i ] [ j ]);

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

        [NotNull]
        [UsedImplicitly]
        internal int[][] CreateRandom(int numCities)
        {
            var dists = new int[numCities][];

            for ( var i = 0 ; i < dists.Length ; ++i )
            {
                dists [ i ] = new int[numCities];
            }

            for ( var i = 0 ; i < numCities ; ++i )
            {
                for ( int j = i + 1 ; j < numCities ; ++j )
                {
                    int d = m_Random.Next(1,
                                          9);
                    dists [ i ] [ j ] = d;
                    dists [ j ] [ i ] = d;
                }
            }
            return dists;
        }

        // todo move this into separat class DistanceGraphValidator
        [UsedImplicitly]
        internal bool IsValidCombinationOfCostMatrixAndCostPerLine([NotNull] int[][] costMatrix,
                                                                   [NotNull] int[] costPerLine)
        {
            int numberOfNodes = costMatrix [ 0 ].Length;
            int numberOfLines = costPerLine.Length;

            if ( costMatrix.Any(matrix => matrix.Length != NumberOfNodes) )
            {
                return false;
            }

            return costMatrix.Length == numberOfNodes &&
                   numberOfNodes == numberOfLines;
        }

        private double CalculateAverageDistance()
        {
            double average = m_CostMatrix.Sum(ints => ints.Average());

            average /= m_CostMatrix.GetLength(0);

            return average;
        }

        private double CalculateMaximumDistance()
        {
            double maximum = double.MinValue;

            foreach ( int[] ints in m_CostMatrix )
            {
                int currentMaximum = ints.Max();

                if ( currentMaximum > maximum &&
                     currentMaximum < double.MaxValue )
                {
                    maximum = currentMaximum;
                }
            }

            return maximum;
        }

        private double CalculateMinimumDistance()
        {
            IEnumerable <double> minimum = m_CostMatrix.Select(FindMinimum);
            IEnumerable <double> concat = minimum.Concat(new[]
                                                         {
                                                             double.MaxValue
                                                         });

            return concat.Min();
        }

        private string CostMatrixToString(IEnumerable <int[]> costMatrix)
        {
            var builder = new StringBuilder();
            var count = 0;

            foreach ( int[] matrix in costMatrix )
            {
                string values = string.Join(", ",
                                            matrix);

                builder.AppendLine($"[{count++}] {values}");
            }

            return builder.ToString();
        }

        private string CostPerLineToString(IEnumerable <int> costPerLine)
        {
            string values = string.Join(", ",
                                        costPerLine);

            return values;
        }

        private double FindMinimum([NotNull] int[] ints)
        {
            double minimum = double.MaxValue;

            foreach ( int i in ints )
            {
                if ( i > 0.0 &&
                     i < minimum )
                {
                    minimum = i;
                }
            }

            return minimum;
        }

        #region IDistanceGraph Members

        public double AverageDistance { get; }

        public IEnumerable <int> GetNeighbours(int node)
        {
            return m_NearestNeighbours.GetNeighbours(node);
        }

        public double MinimumDistance { get; }

        public double MaximumDistance { get; }

        public int GetCost(int fromIndex,
                           int toIndex)
        {
            CanExecuteGetCost(fromIndex,
                              toIndex);

            return m_CostMatrix [ fromIndex ] [ toIndex ];
        }

        private void CanExecuteGetCost(int fromIndex,
                                       int toIndex)
        {
            if ( m_CostMatrix.Length == 0 ||
                 m_CostMatrix [ 0 ].Length == 0 )
            {
                throw new ArgumentException($"CostMatrix not set! - fromIndex: {fromIndex} toIndex: {toIndex}]");
            }

            if ( fromIndex < 0 ||
                 fromIndex >= m_CostMatrix.Length )
            {
                throw new ArgumentException($"fromIndex {fromIndex} out of bounds [0-{m_CostMatrix.Length}]",
                                            nameof(fromIndex));
            }

            if ( toIndex < 0 ||
                 toIndex >= m_CostMatrix [ 0 ].Length )
            {
                throw new ArgumentException($"fromIndex {fromIndex} out of bounds [0-{m_CostMatrix.Length}]",
                                            nameof(fromIndex));
            }
        }

        public int NumberOfNodes { get; }

        public int NumberOfUniqueNodes { get; }

        [UsedImplicitly]
        public double Length(ITrailBuilder trailBuilder)
        {
            return Length(trailBuilder.Trail);
        }

        public double Length(IEnumerable <int> trail)
        {
            int[] trailArray = trail.ToArray();

            if ( trailArray.Length == 0 )
            {
                return 0.0;
            }

            var result = 0.0;

            for ( var i = 0 ; i < trailArray.Length - 1 ; ++i )
            {
                int fromIndex = trailArray [ i ];
                int toIndex = trailArray [ i + 1 ];

                int cost = GetCost(fromIndex,
                                   toIndex);

                if ( cost <= 0.0 )
                {
                    return int.MaxValue;
                }

                result += cost;
            }

            int last = trailArray.Last();

            result += m_CostPerLine [ last ];

            return result;
        }

        public bool IsValidPath(IEnumerable <int> trail)
        {
            int[] trailArray = trail.ToArray();

            if ( trailArray.Length == 0 )
            {
                return false;
            }

            for ( var i = 0 ; i < trailArray.Length - 1 ; ++i )
            {
                int cost = GetCost(trailArray [ i ],
                                   trailArray [ i + 1 ]);

                if ( cost <= 0.0 )
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}