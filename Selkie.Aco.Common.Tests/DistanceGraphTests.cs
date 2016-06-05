using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Common.Interfaces;
using Selkie.Common;

namespace Selkie.Aco.Common.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class DistanceGraphTests
    {
        [SetUp]
        public void Setup()
        {
            int[][] costmatrix = CreateCostmatrixForSetup();
            int[] costPerLine = CreateCostPerLineSetuo();

            m_Graph = CreateGraph(costmatrix,
                                  costPerLine);
        }

        private static int[] CreateCostPerLineSetuo()
        {
            int[] costPerLine =
            {
                1,
                1,
                2,
                2
            };
            return costPerLine;
        }

        private static int[][] CreateCostmatrixForSetup()
        {
            int[][] costmatrix =
            {
                new[]
                {
                    3,
                    2,
                    1,
                    0
                },
                new[]
                {
                    2,
                    4,
                    6,
                    5
                },
                new[]
                {
                    9,
                    6,
                    7,
                    8
                },
                new[]
                {
                    1,
                    2,
                    3,
                    5
                }
            };
            return costmatrix;
        }

        private DistanceGraph CreateGraph(int[][] costmatrix,
                                          int[] costPerLine)
        {
            return new DistanceGraph(new SelkieRandom(),
                                     new NearestNeighbours(),
                                     costmatrix,
                                     costPerLine);
        }

        private DistanceGraph m_Graph;

        private static int[] CreateTrail()
        {
            int[] trail =
            {
                0,
                1,
                1,
                3
            };
            return trail;
        }

        private static int[][] CreateCostmatrix()
        {
            int[][] costmatrix =
            {
                new[]
                {
                    0,
                    1,
                    1,
                    1
                },
                new[]
                {
                    1,
                    0,
                    1,
                    1
                },
                new[]
                {
                    1,
                    1,
                    0,
                    1
                },
                new[]
                {
                    1,
                    1,
                    1,
                    0
                }
            };
            return costmatrix;
        }

        private static int[] CreateCostPerLine()
        {
            int[] costPerLine =
            {
                1,
                1,
                2,
                2
            };
            return costPerLine;
        }

        [Test]
        public void AverageDistanceTest()
        {
            Assert.AreEqual(4.0,
                            m_Graph.AverageDistance);
        }

        [Test]
        public void CalculateNearestNeighboursTest()
        {
            // Arrange
            int[][] expected =
            {
                new[]
                {
                    3,
                    2,
                    1
                },
                new[]
                {
                    0,
                    3,
                    2
                },
                new[]
                {
                    1,
                    3,
                    0
                },
                new[]
                {
                    0,
                    1,
                    2
                }
            };

            // Act
            int[][] actual = m_Graph.CalculateNearestNeighbours();

            // Assert
            Assert.True(expected [ 0 ].SequenceEqual(actual [ 0 ]),
                        "actual[0]");
            Assert.True(expected [ 1 ].SequenceEqual(actual [ 1 ]),
                        "actual[1]");
            Assert.True(expected [ 2 ].SequenceEqual(actual [ 2 ]),
                        "actual[2]");
            Assert.True(expected [ 3 ].SequenceEqual(actual [ 3 ]),
                        "actual[3]");
        }

        [Test]
        public void CostMatrixLengthTest()
        {
            // Arrange
            // Act
            int[][] actual = m_Graph.CreateRandom(3);

            // Assert
            Assert.AreEqual(3,
                            actual.GetLength(0),
                            "GetLength(0)");
            Assert.AreEqual(3,
                            actual [ 0 ].Length,
                            "[0] Length");
            Assert.AreEqual(3,
                            actual [ 1 ].Length,
                            "[1] Length");
            Assert.AreEqual(3,
                            actual [ 2 ].Length,
                            "[2] Length");
        }

        [Test]
        public void CreateNearestNeighboursTest()
        {
            // Arrange
            int[][] expected =
            {
                new[]
                {
                    3,
                    2,
                    1
                },
                new[]
                {
                    0,
                    3,
                    2
                },
                new[]
                {
                    1,
                    3,
                    0
                },
                new[]
                {
                    0,
                    1,
                    2
                }
            };

            // Act
            // Assert
            Assert.True(expected [ 0 ].SequenceEqual(m_Graph.GetNeighbours(0)),
                        "actual[0]");
            Assert.True(expected [ 1 ].SequenceEqual(m_Graph.GetNeighbours(1)),
                        "actual[1]");
            Assert.True(expected [ 2 ].SequenceEqual(m_Graph.GetNeighbours(2)),
                        "actual[2]");
            Assert.True(expected [ 3 ].SequenceEqual(m_Graph.GetNeighbours(3)),
                        "actual[3]");
        }

        [Test]
        public void GetCostTest()
        {
            Assert.AreEqual(3,
                            m_Graph.GetCost(0,
                                            0),
                            "0,0");
            Assert.AreEqual(4,
                            m_Graph.GetCost(1,
                                            1),
                            "1,1");
            Assert.AreEqual(7,
                            m_Graph.GetCost(2,
                                            2),
                            "2,2");
            Assert.AreEqual(5,
                            m_Graph.GetCost(3,
                                            3),
                            "3,3");
        }

        [Test]
        public void GetCostThrowsForCostMatrixIsEmptyTest()
        {
            var doesnotMatter = new[]
                                {
                                    1,
                                    2
                                };

            DistanceGraph sut = CreateGraph(new int[0][],
                                            doesnotMatter);

            Assert.Throws <ArgumentException>(() => sut.GetCost(1,
                                                                3));
        }

        [Test]
        public void GetCostThrowsForFromIndexFarToBigTest()
        {
            Assert.Throws <ArgumentException>(() => m_Graph.GetCost(4000,
                                                                    0));
        }

        [Test]
        public void GetCostThrowsForFromIndexNegativeTest()
        {
            Assert.Throws <ArgumentException>(() => m_Graph.GetCost(-1,
                                                                    0));
        }

        [Test]
        public void GetCostThrowsForFromIndexToBigTest()
        {
            Assert.Throws <ArgumentException>(() => m_Graph.GetCost(4,
                                                                    0));
        }

        [Test]
        public void GetCostThrowsForToIndexFarToBigTest()
        {
            Assert.Throws <ArgumentException>(() => m_Graph.GetCost(0,
                                                                    4000));
        }

        [Test]
        public void GetCostThrowsForToIndexNegativeTest()
        {
            Assert.Throws <ArgumentException>(() => m_Graph.GetCost(0,
                                                                    -1));
        }

        [Test]
        public void GetCostThrowsForToIndexToBigTest()
        {
            Assert.Throws <ArgumentException>(() => m_Graph.GetCost(0,
                                                                    4));
        }

        [Test]
        public void IsUnknownReturnsFalseForKnownTest()
        {
            Assert.False(m_Graph.IsUnknown);
        }

        [Test]
        public void IsUnknownReturnsTrueForUnknownTest()
        {
            IDistanceGraph neighbhours = DistanceGraph.Unknown;

            Assert.True(neighbhours.IsUnknown);
        }

        [Test]
        public void IsValidCombinationOfCostMatrixAndCostPerLineReturnsFalseForInvalidCostMatrixTest()
        {
            // Arrange
            int[][] costmatrix =
            {
                new[]
                {
                    1,
                    2
                },
                new[]
                {
                    3
                }
            };

            int[] costPerLine =
            {
                1,
                1
            };

            // Act
            bool actual = m_Graph.IsValidCombinationOfCostMatrixAndCostPerLine(costmatrix,
                                                                               costPerLine);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void IsValidCombinationOfCostMatrixAndCostPerLineReturnsFalseForInvalidDataTest()
        {
            // Arrange
            int[][] costmatrix =
            {
                new[]
                {
                    1,
                    2
                },
                new[]
                {
                    3,
                    4
                }
            };

            int[] costPerLine =
            {
                12345
            };

            // Act
            bool actual = m_Graph.IsValidCombinationOfCostMatrixAndCostPerLine(costmatrix,
                                                                               costPerLine);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void IsValidCombinationOfCostMatrixAndCostPerLineReturnsTrueForValidDataTest()
        {
            // Arrange
            int[][] costmatrix = CreateCostmatrix();
            int[] costPerLine = CreateCostPerLine();

            // Act
            bool actual = m_Graph.IsValidCombinationOfCostMatrixAndCostPerLine(costmatrix,
                                                                               costPerLine);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void IsValidPathReturnsFalseForDuplicateTest()
        {
            // Arrange
            int[][] costmatrix =
            {
                new[]
                {
                    0,
                    1,
                    1,
                    1
                },
                new[]
                {
                    1,
                    0,
                    1,
                    1
                },
                new[]
                {
                    1,
                    1,
                    0,
                    1
                },
                new[]
                {
                    1,
                    1,
                    1,
                    0
                }
            };

            int[] costPerLine =
            {
                1,
                1,
                2,
                2
            };

            DistanceGraph graph = CreateGraph(costmatrix,
                                              costPerLine);

            int[] trail =
            {
                0,
                0
            };

            // Act
            bool actual = graph.IsValidPath(trail);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void IsValidPathReturnsFalseForEmptyTest()
        {
            // Arrange
            int[] trail =
            {
            };

            // Act
            bool actual = m_Graph.IsValidPath(trail);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void IsValidPathReturnsFalseTest()
        {
            // Arrange
            int[][] costmatrix = CreateCostmatrix();
            int[] costPerLine = CreateCostPerLine();
            int[] trail = CreateTrail();
            DistanceGraph graph = CreateGraph(costmatrix,
                                              costPerLine);

            // Act
            bool actual = graph.IsValidPath(trail);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void IsValidPathReturnsTrueTest()
        {
            // Arrange
            int[] trail =
            {
                0,
                1,
                2,
                3
            };

            // Act
            bool actual = m_Graph.IsValidPath(trail);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void IsValidReturnsTrueForValidDataTest()
        {
            Assert.True(m_Graph.IsValid());
        }

        [Test]
        public void LengthForEmptyTrailTest()
        {
            Assert.AreEqual(0.0,
                            m_Graph.Length(new int[0]));
        }

        [Test]
        public void LengthForTrailBuilderTest()
        {
            // Arrange
            var builder = Substitute.For <ITrailBuilder>();
            builder.Trail.Returns(new[]
                                  {
                                      0,
                                      1,
                                      2,
                                      3
                                  });

            // Act
            double actual = m_Graph.Length(builder);

            // Assert
            Assert.AreEqual(18.0,
                            actual);
        }

        [Test]
        public void LengthForTrailTest()
        {
            // Arrange
            var builder = Substitute.For <ITrailBuilder>();
            builder.Trail.Returns(new[]
                                  {
                                      0,
                                      1,
                                      2,
                                      3
                                  });

            // Act
            double length = m_Graph.Length(builder);

            // Assert
            Assert.AreEqual(18.0,
                            length);
        }

        [Test]
        public void MaximumDistanceTest()
        {
            Assert.AreEqual(9.0,
                            m_Graph.MaximumDistance);
        }

        [Test]
        public void MinimumDistanceTest()
        {
            Assert.AreEqual(1.0,
                            m_Graph.MinimumDistance);
        }

        [Test]
        public void NumberOfNodesTest()
        {
            Assert.AreEqual(4,
                            m_Graph.NumberOfNodes);
        }

        [Test]
        public void ToStringReturnsStringTest()
        {
            // Arrange
            const string expected = "CostMatrix\r\n" +
                                    "[0] 3, 2, 1, 0\r\n" +
                                    "[1] 2, 4, 6, 5\r\n" +
                                    "[2] 9, 6, 7, 8\r\n" +
                                    "[3] 1, 2, 3, 5\r\n\r\n" +
                                    "CostPerLine\r\n" +
                                    "1, 1, 2, 2\r\n\r\n" +
                                    "NearestNeighbours\r\n" +
                                    "[0] 3, 2, 1\r\n" +
                                    "[1] 0, 3, 2\r\n" +
                                    "[2] 1, 3, 0\r\n" +
                                    "[3] 0, 1, 2\r\n\r\n" +
                                    "IsValid? True\r\n";

            // Act
            string actual = m_Graph.ToString();

            // Assert
            Assert.True(string.Compare(expected,
                                       actual,
                                       StringComparison.InvariantCulture) == 0);
        }
    }
}