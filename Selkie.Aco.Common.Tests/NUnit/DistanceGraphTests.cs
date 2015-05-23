using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Selkie.Common;

namespace Selkie.Aco.Common.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class DistanceGraphTests
    {
        [SetUp]
        public void Setup()
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

            int[] costPerLine =
            {
                1,
                1,
                2,
                2
            };

            m_Graph = new DistanceGraph(new SelkieRandom(),
                                        new NearestNeighbours(),
                                        costmatrix,
                                        costPerLine);
        }

        private DistanceGraph m_Graph;

        [Test]
        public void AverageDistanceTest()
        {
            Assert.AreEqual(4.0,
                            m_Graph.AverageDistance);
        }

        [Test]
        public void CalculateNearestNeighboursTest()
        {
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

            int[][] actual = m_Graph.CalculateNearestNeighbours();

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
            int[][] actual = m_Graph.CreateRandom(3);

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
        public void IsValidPathReturnsFalseForDuplicateTest()
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

            int[] costPerLine =
            {
                1,
                1,
                2,
                2
            };

            var graph = new DistanceGraph(new SelkieRandom(),
                                          new NearestNeighbours(),
                                          costmatrix,
                                          costPerLine);
            int[] trail =
            {
                0,
                0
            };

            Assert.False(graph.IsValidPath(trail));
        }

        [Test]
        public void IsValidPathReturnsFalseForEmptyTest()
        {
            int[] trail =
            {
            };

            Assert.False(m_Graph.IsValidPath(trail));
        }

        [Test]
        public void IsValidPathReturnsFalseTest()
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

            int[] costPerLine =
            {
                1,
                1,
                2,
                2
            };

            var graph = new DistanceGraph(new SelkieRandom(),
                                          new NearestNeighbours(),
                                          costmatrix,
                                          costPerLine);
            int[] trail =
            {
                0,
                1,
                1,
                3
            };

            Assert.False(graph.IsValidPath(trail));
        }

        [Test]
        public void IsValidPathReturnsTrueTest()
        {
            int[] trail =
            {
                0,
                1,
                2,
                3
            };

            Assert.True(m_Graph.IsValidPath(trail));
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
            var builder = Substitute.For <ITrailBuilder>();
            builder.Trail.Returns(new[]
                                  {
                                      0,
                                      1,
                                      2,
                                      3
                                  });

            Assert.AreEqual(18.0,
                            m_Graph.Length(builder));
        }

        [Test]
        public void LengthForTrailTest()
        {
            var builder = Substitute.For <ITrailBuilder>();
            builder.Trail.Returns(new[]
                                  {
                                      0,
                                      1,
                                      2,
                                      3
                                  });

            Assert.AreEqual(18.0,
                            m_Graph.Length(builder));
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
    }
}