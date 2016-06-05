using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails.Interfaces;
using Selkie.Aco.Trails.Optimizers;
using Selkie.Common.Interfaces;

namespace Selkie.Aco.Trails.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    // ReSharper disable once ClassTooBig
    internal sealed class CandidateListTrailBuilderTests
    {
        [SetUp]
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();
            m_Tracker = CreatePheromonesTracker();
            m_Graph = CreateDistanceGraph();

            m_Chromosome = Substitute.For <IChromosome>();
            m_Chromosome.Alpha.Returns(3.0);
            m_Chromosome.Beta.Returns(2.0);
            m_Chromosome.Gamma.Returns(1.0);

            m_Optimizer = new TwoOptSimple
                          {
                              DistanceGraph = m_Graph
                          };

            m_TrailBuilder = new CandidateListTrailBuilder(m_Random,
                                                           m_Chromosome,
                                                           m_Tracker,
                                                           m_Graph,
                                                           m_Optimizer);
        }

        [NotNull]
        private IDistanceGraph CreateDistanceGraph()
        {
            var graph = Substitute.For <IDistanceGraph>();
            graph.NumberOfNodes.Returns(4);
            graph.NumberOfUniqueNodes.Returns(2);

            PopulateCostMatrix(graph);
            PopulateNearestNeighbours(graph);
            PopulateDistances(graph);

            return graph;
        }

        private void PopulateDistances([NotNull] IDistanceGraph graph)
        {
            graph.MinimumDistance.Returns(10.0);
            graph.AverageDistance.Returns(65.0);
            graph.MaximumDistance.Returns(120.0);
        }

        private void PopulateNearestNeighbours([NotNull] IDistanceGraph graph)
        {
            int[][] neighbours =
            {
                new[]
                {
                    0,
                    1,
                    2,
                    3
                },
                new[]
                {
                    1,
                    0,
                    2,
                    3
                },
                new[]
                {
                    1,
                    2,
                    0,
                    3
                },
                new[]
                {
                    1,
                    2,
                    3,
                    0
                }
            };

            graph.GetNeighbours(0).Returns(neighbours [ 0 ]);
            graph.GetNeighbours(1).Returns(neighbours [ 1 ]);
            graph.GetNeighbours(2).Returns(neighbours [ 2 ]);
            graph.GetNeighbours(3).Returns(neighbours [ 3 ]);
        }

        // ReSharper disable once MethodTooLong
        private void PopulateCostMatrix([NotNull] IDistanceGraph graph)
        {
            graph.GetCost(0,
                          0).Returns(0);
            graph.GetCost(0,
                          1).Returns(10);
            graph.GetCost(0,
                          2).Returns(20);
            graph.GetCost(0,
                          3).Returns(30);
            graph.GetCost(1,
                          0).Returns(40);
            graph.GetCost(1,
                          1).Returns(0);
            graph.GetCost(1,
                          2).Returns(50);
            graph.GetCost(1,
                          3).Returns(60);
            graph.GetCost(2,
                          0).Returns(70);
            graph.GetCost(2,
                          1).Returns(80);
            graph.GetCost(2,
                          2).Returns(0);
            graph.GetCost(2,
                          3).Returns(90);
            graph.GetCost(3,
                          0).Returns(100);
            graph.GetCost(3,
                          1).Returns(110);
            graph.GetCost(3,
                          2).Returns(120);
            graph.GetCost(3,
                          3).Returns(0);
        }

        private CandidateListTrailBuilder m_TrailBuilder;
        private IPheromonesTracker m_Tracker;
        private IDistanceGraph m_Graph;
        private IChromosome m_Chromosome;
        private IOptimizer m_Optimizer;
        private IRandom m_Random;

        [NotNull]
        // ReSharper disable once MethodTooLong
        private static IPheromonesTracker CreatePheromonesTracker()
        {
            var tracker = Substitute.For <IPheromonesTracker>();

            tracker.GetValue(0,
                             0).Returns(1.0);
            tracker.GetValue(0,
                             1).Returns(2.0);
            tracker.GetValue(0,
                             2).Returns(3.0);
            tracker.GetValue(0,
                             3).Returns(4.0);

            tracker.GetValue(1,
                             0).Returns(5.0);
            tracker.GetValue(1,
                             1).Returns(6.0);
            tracker.GetValue(1,
                             2).Returns(7.0);
            tracker.GetValue(1,
                             3).Returns(8.0);

            tracker.GetValue(2,
                             0).Returns(9.0);
            tracker.GetValue(2,
                             1).Returns(10.0);
            tracker.GetValue(2,
                             2).Returns(11.0);
            tracker.GetValue(2,
                             3).Returns(12.0);

            tracker.GetValue(3,
                             0).Returns(13.0);
            tracker.GetValue(3,
                             1).Returns(14.0);
            tracker.GetValue(3,
                             2).Returns(15.0);
            tracker.GetValue(3,
                             3).Returns(16.0);

            return tracker;
        }

        [Test]
        public void BuildCandidateListForNumberOfCandidatesIsOneTest()
        {
            m_TrailBuilder.NumberOfCandidates = 1;

            int[] expected =
            {
                0
            };
            IEnumerable <int> actual = m_TrailBuilder.BuildCandidateList(0);

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void BuildCandidateListForNumberOfCandidatesIsTwoTest()
        {
            m_TrailBuilder.NumberOfCandidates = 2;

            int[] expected =
            {
                0,
                1
            };
            IEnumerable <int> actual = m_TrailBuilder.BuildCandidateList(0);

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void BuildTrailDoesNotContainRelatedCitiesTest()
        {
            m_TrailBuilder.BuildTrail(0);

            List <int> actual = m_TrailBuilder.Trail.ToList();

            for ( var i = 0 ; i < m_Graph.NumberOfUniqueNodes ; i++ )
            {
                int selected = actual [ i ];
                int reverse = BaseTrailBuilder <CandidateListTrailBuilder>.FindRelatedCity(selected);

                Assert.False(actual.Contains(reverse),
                             "Should not contain reverse " + reverse);
            }
        }

        [Test]
        public void BuildTrailLengthTest()
        {
            m_TrailBuilder.BuildTrail(0);

            Assert.AreEqual(m_Graph.NumberOfUniqueNodes,
                            m_TrailBuilder.Trail.Count());
        }

        [Test]
        public void BuildTrailStartsWithTheGivenCityTest()
        {
            m_TrailBuilder.BuildTrail(1);

            Assert.AreEqual(1,
                            m_TrailBuilder.Trail.First());
        }

        [Test]
        public void CloneTest()
        {
            var trailBuilderFactory = Substitute.For <ITrailBuilderFactory>();
            var chromosomeFactory = Substitute.For <IChromosomeFactory>();

            m_TrailBuilder.Clone(trailBuilderFactory,
                                 chromosomeFactory);

            trailBuilderFactory.ReceivedWithAnyArgs().Create <ICandidateListTrailBuilder>(Arg.Any <IChromosome>(),
                                                                                          Arg.Any <IPheromonesTracker>(),
                                                                                          Arg.Any <IDistanceGraph>(),
                                                                                          Arg.Any <IOptimizer>(),
                                                                                          Arg.Any <IEnumerable <int>>());
        }

        [Test]
        public void DecideNumberOfCandidatesForMoreThanFourTest()
        {
            Assert.AreEqual(25,
                            CandidateListTrailBuilder.DecideNumberOfCandidates(100),
                            "100");
            Assert.AreEqual(50,
                            CandidateListTrailBuilder.DecideNumberOfCandidates(200),
                            "200");
        }

        [Test]
        public void DecideNumberOfCandidatesForOneToFourTest()
        {
            Assert.AreEqual(1,
                            CandidateListTrailBuilder.DecideNumberOfCandidates(1),
                            "1");
            Assert.AreEqual(1,
                            CandidateListTrailBuilder.DecideNumberOfCandidates(2),
                            "2");
            Assert.AreEqual(1,
                            CandidateListTrailBuilder.DecideNumberOfCandidates(3),
                            "3");
            Assert.AreEqual(1,
                            CandidateListTrailBuilder.DecideNumberOfCandidates(4),
                            "4");
        }

        [Test]
        public void DecideNumberOfCandidatesForZeroTest()
        {
            Assert.AreEqual(0,
                            CandidateListTrailBuilder.DecideNumberOfCandidates(0));
        }

        [Test]
        public void EqualsOperatorReturnsTrueForSameTest()
        {
            // ReSharper disable EqualExpressionComparison
            Assert.True(m_TrailBuilder == m_TrailBuilder);
            // ReSharper restore EqualExpressionComparison
        }

        [Test]
        public void EqualsReturnsFalseForDifferentChromosomeTest()
        {
            var builderTwo = new CandidateListTrailBuilder(m_Random,
                                                           m_Chromosome,
                                                           m_Tracker,
                                                           m_Graph,
                                                           m_Optimizer);

            Assert.True(m_TrailBuilder.Equals(builderTwo));
        }

        [Test]
        public void EqualsReturnsFalseForDifferentTypeTest()
        {
            var unknown = Substitute.For <IChromosome>();
            unknown.IsUnknown.Returns(true);

            var builderTwo = new UnknownTrailBuilder(m_Random,
                                                     unknown,
                                                     m_Tracker,
                                                     m_Graph,
                                                     m_Optimizer);

            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.False(m_TrailBuilder.Equals(builderTwo));
        }

        [Test]
        public void EqualsReturnsFalseForNullTest()
        {
            Assert.False(m_TrailBuilder.Equals(null));
        }

        [Test]
        public void EqualsReturnsFalseForObjectNullTest()
        {
            Assert.False(m_TrailBuilder.Equals(( object ) null));
        }

        [Test]
        public void EqualsReturnsFalseForWrongObjectTypeTest()
        {
            Assert.False(m_TrailBuilder.Equals(new object()));
        }

        [Test]
        public void EqualsReturnsTrueForSameTest()
        {
            Assert.True(m_TrailBuilder.Equals(m_TrailBuilder));
        }

        [Test]
        public void EqualsReturnsTrueForSameValuesTest()
        {
            var chromosome = Substitute.For <IChromosome>();
            chromosome.Alpha.Returns(m_Chromosome.Alpha);
            chromosome.Beta.Returns(m_Chromosome.Beta);
            chromosome.Gamma.Returns(m_Chromosome.Gamma + 1.0);

            var builderTwo = new StandardTrailBuilder(m_Random,
                                                      chromosome,
                                                      m_Tracker,
                                                      m_Graph,
                                                      m_Optimizer);

            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.False(m_TrailBuilder.Equals(builderTwo));
        }

        [Test]
        public void FindCandidatesForAllCandidatesNotVisitedTest()
        {
            m_TrailBuilder.NumberOfCandidates = 4;
            bool[] visited =
            {
                false,
                false,
                false,
                false
            };

            int[] expected =
            {
                0,
                1,
                2,
                3
            };
            int[] actual = m_TrailBuilder.FindCandidates(0,
                                                         visited);

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void FindCandidatesForCandidateZeroIsVisitedTest()
        {
            m_TrailBuilder.NumberOfCandidates = 4;
            bool[] visited =
            {
                true,
                true,
                false,
                false
            };

            int[] expected =
            {
                2,
                3
            };
            int[] actual = m_TrailBuilder.FindCandidates(0,
                                                         visited);

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void GetHashCodeTest()
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.DoesNotThrow(() => m_TrailBuilder.GetHashCode());
        }

        [Test]
        public void IsUnknownReturnsFalseTest()
        {
            Assert.False(m_TrailBuilder.IsUnknown);
        }

        [Test]
        public void NextCityReturnsCityOneForOtherTwoVisitedAndForwardTest()
        {
            bool[] visited =
            {
                true,
                true,
                false,
                true
            };

            int actual = m_TrailBuilder.NextCity(0,
                                                 visited,
                                                 0.9);

            Assert.AreEqual(2,
                            actual);
        }

        [Test]
        public void NextCityReturnsCityOneForOtherTwoVisitedAndReverseExpectedTest()
        {
            bool[] visited =
            {
                true,
                true,
                true,
                false
            };

            int actual = m_TrailBuilder.NextCity(0,
                                                 visited,
                                                 0.9);

            Assert.AreEqual(3,
                            actual);
        }

        [Test]
        public void NextCityReturnsCityTwoForOtherTwoUnvisitedAndForwardExpectedTest()
        {
            bool[] visited =
            {
                true,
                true,
                false,
                false
            };

            int actual = m_TrailBuilder.NextCity(0,
                                                 visited,
                                                 0.25);

            Assert.AreEqual(2,
                            actual);
        }

        [Test]
        public void NextCityReturnsCityTwoForOtherTwoUnvisitedAndReverseExpectedTest()
        {
            bool[] visited =
            {
                true,
                true,
                false,
                false
            };

            int actual = m_TrailBuilder.NextCity(0,
                                                 visited,
                                                 0.9);

            Assert.AreEqual(3,
                            actual);
        }

        [Test]
        public void NextCityThrowsForNoCityLeftTest()
        {
            bool[] visited =
            {
                true,
                true,
                true,
                true
            };

            Assert.Throws <ArgumentException>(() => m_TrailBuilder.NextCity(0,
                                                                            visited,
                                                                            0.9));
        }

        [Test]
        public void NotEqualsOperatorReturnsTrueForSameTest()
        {
            // ReSharper disable EqualExpressionComparison
            Assert.False(m_TrailBuilder != m_TrailBuilder);
            // ReSharper restore EqualExpressionComparison
        }

        [Test]
        public void NumberOfCandidatesForLessThanZeroTest()
        {
            m_TrailBuilder.NumberOfCandidates = -1;

            Assert.AreEqual(1,
                            m_TrailBuilder.NumberOfCandidates);
        }

        [Test]
        public void NumberOfCandidatesForMoreThanNumberOfUniqueNodesTest()
        {
            m_TrailBuilder.NumberOfCandidates = m_Graph.NumberOfUniqueNodes + 1;

            Assert.AreEqual(3,
                            m_TrailBuilder.NumberOfCandidates);
        }

        [Test]
        public void NumberOfCandidatesForNumberOfUniqueNodesTest()
        {
            m_TrailBuilder.NumberOfCandidates = m_Graph.NumberOfUniqueNodes;

            Assert.AreEqual(m_Graph.NumberOfUniqueNodes,
                            m_TrailBuilder.NumberOfCandidates);
        }

        [Test]
        public void NumberOfCandidatesForValidValueTest()
        {
            m_TrailBuilder.NumberOfCandidates = 2;

            Assert.AreEqual(2,
                            m_TrailBuilder.NumberOfCandidates);
        }

        [Test]
        public void NumberOfCandidatesForZeroTest()
        {
            m_TrailBuilder.NumberOfCandidates = 0;

            Assert.AreEqual(1,
                            m_TrailBuilder.NumberOfCandidates);
        }

        [Test]
        public void NumberOfCandidatesTest()
        {
            Assert.AreEqual(1,
                            m_TrailBuilder.NumberOfCandidates);
        }

        [Test]
        public void SearchGeneralForOtherTwoUnvisitedTest()
        {
            bool[] visited =
            {
                true,
                true,
                false,
                false
            };
            int[] trail =
            {
                0,
                2
            };

            m_TrailBuilder.SearchGeneral(visited,
                                         trail);

            Assert.True(visited [ 0 ],
                        "[0]");
            Assert.True(visited [ 1 ],
                        "[1]");
            Assert.True(visited [ 2 ],
                        "[2]");
            Assert.True(visited [ 3 ],
                        "[3]");
        }

        [Test]
        public void TypeTest()
        {
            Assert.AreEqual(typeof( ICandidateListTrailBuilder ).Name,
                            m_TrailBuilder.Type);
        }
    }
}