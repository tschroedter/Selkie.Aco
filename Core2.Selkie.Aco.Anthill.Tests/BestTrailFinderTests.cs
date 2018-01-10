using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Common.TypedFactories;
using Core2.Selkie.Aco.Trails;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Aco.Trails.Optimizers;
using Core2.Selkie.Common.Interfaces;

namespace Core2.Selkie.Aco.Anthill.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class BestTrailFinderTests
    {
        [SetUp]
        public void Setup()
        {
            m_AntFactory = new TestAntFactory();
            m_TrailAlternatives = new TrailAlternatives(new TestTrailBuilderFactory(),
                                                        new TestChromosomeFactory());
            m_Graph = CreateGraph();
            m_Tracker = Substitute.For <IPheromonesTracker>();
            m_Optimizer = new TwoOptSimple
                          {
                              DistanceGraph = m_Graph
                          };

            m_Sut = new BestTrailFinder(Substitute.For <IDisposer>(),
                                        m_AntFactory,
                                        m_Graph,
                                        m_Tracker,
                                        m_Optimizer,
                                        m_TrailAlternatives);
        }

        [TearDown]
        public void Teardown()
        {
            m_Sut.Dispose();
        }

        private IAntFactory m_AntFactory;
        private BestTrailFinder m_Sut;
        private IDistanceGraph m_Graph;
        private IOptimizer m_Optimizer;
        private IPheromonesTracker m_Tracker;
        private ITrailAlternatives m_TrailAlternatives;

        [NotNull]
        private IDistanceGraph CreateGraph()
        {
            int[][] nearestNeighbourValues =
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
                    2,
                    1,
                    0,
                    3
                },
                new[]
                {
                    3,
                    1,
                    2,
                    0
                }
            };

            var graph = Substitute.For <IDistanceGraph>();
            graph.MinimumDistance.Returns(1);

            const int numberOfNodes = 4;
            graph.NumberOfNodes.Returns(numberOfNodes);
            graph.NumberOfUniqueNodes.Returns(numberOfNodes / 2);
            graph.GetNeighbours(0).Returns(nearestNeighbourValues [ 0 ]);
            graph.GetNeighbours(1).Returns(nearestNeighbourValues [ 1 ]);
            graph.GetNeighbours(2).Returns(nearestNeighbourValues [ 2 ]);
            graph.GetNeighbours(3).Returns(nearestNeighbourValues [ 3 ]);

            return graph;
        }

        [NotNull]
        private static ITrailBuilder CreateTrailBuilder([NotNull] IEnumerable <int> trail,
                                                        double length)
        {
            var builder = Substitute.For <ITrailBuilder>();

            builder.Trail.Returns(trail);
            builder.Length.Returns(length);
            builder.Clone(Arg.Any <ITrailBuilderFactory>(),
                          Arg.Any <IChromosomeFactory>()).Returns(builder);

            return builder;
        }

        [NotNull]
        private static IAnt CreateAnt(int id,
                                      [NotNull] ITrailBuilder builder)
        {
            var ant = Substitute.For <IAnt>();

            ant.Id.Returns(id);
            ant.TrailBuilder.Returns(builder);

            return ant;
        }

        [NotNull]
        private static IChromosome CreateChromosome()
        {
            var chromosome = Substitute.For <IChromosome>();

            chromosome.Alpha.Returns(1.0);
            chromosome.Beta.Returns(2.0);
            chromosome.Gamma.Returns(3.0);

            return chromosome;
        }

        private static IAnt CreateAnt(ITrailBuilder trail1)
        {
            var ant1 = Substitute.For <IAnt>();
            ant1.Id.Returns(0);
            ant1.TrailBuilder.Returns(trail1);
            return ant1;
        }

        private static ITrailBuilder CreateTrailBuilder()
        {
            var trail1 = Substitute.For <ITrailBuilder>();
            trail1.Trail.Returns(new[]
                                 {
                                     0,
                                     1,
                                     2
                                 });
            trail1.Length.Returns(10.0);
            trail1.Clone(Arg.Any <ITrailBuilderFactory>(),
                         Arg.Any <IChromosomeFactory>()).Returns(trail1);
            return trail1;
        }

        [Test]
        public void Clear_SetsAlternativeTrailsToEmpty_WhenCalled()
        {
            // Arrange
            ITrailBuilder trail = CreateTrailBuilder();
            IAnt ant = CreateAnt(trail);
            IAnt[] ants =
            {
                ant
            };

            m_Sut.FindBestTrail(ants);

            Assert.AreEqual(trail,
                            m_Sut.BestTrailBuilder,
                            "BestTrailBuilder");
            Assert.AreEqual(1,
                            m_Sut.AlternativeTrails.Count(),
                            "Count");

            // Act
            m_Sut.Clear();

            // Assert
            Assert.AreEqual(0,
                            m_Sut.AlternativeTrails.Count(),
                            "Count");
        }

        [Test]
        public void Clear_SetsBestTrailBuilderToUnknown_WhenCalled()
        {
            // Arrange
            ITrailBuilder trail = CreateTrailBuilder();
            IAnt ant = CreateAnt(trail);
            IAnt[] ants =
            {
                ant
            };

            m_Sut.FindBestTrail(ants);

            Assert.AreEqual(trail,
                            m_Sut.BestTrailBuilder,
                            "BestTrailBuilder");
            Assert.AreEqual(1,
                            m_Sut.AlternativeTrails.Count(),
                            "Count");

            // Act
            m_Sut.Clear();

            // Assert
            Assert.True(m_Sut.BestTrailBuilder.IsUnknown,
                        "BestTrailBuilder");
            Assert.AreEqual(0,
                            m_Sut.AlternativeTrails.Count(),
                            "Count");
        }

        [Test]
        public void Dispose_CallsDisposer_WhenCalled()
        {
            // Arrange
            var disposer = Substitute.For <IDisposer>();

            var sut = new BestTrailFinder(disposer,
                                          m_AntFactory,
                                          m_Graph,
                                          m_Tracker,
                                          m_Optimizer,
                                          m_TrailAlternatives);

            // Act
            sut.Dispose();

            // Assert
            disposer.Received().Dispose();
        }

        [Test]
        public void FindBestTrail_SetsAlternativeTrails_ForFirstIsBest()
        {
            // Arrange
            ITrailBuilder builder1 = CreateTrailBuilder(new[]
                                                        {
                                                            0,
                                                            1,
                                                            2
                                                        },
                                                        10.0);
            IAnt ant1 = CreateAnt(0,
                                  builder1);

            ITrailBuilder builder2 = CreateTrailBuilder(new[]
                                                        {
                                                            2,
                                                            1,
                                                            0
                                                        },
                                                        20.0);
            IAnt ant2 = CreateAnt(1,
                                  builder2);

            IAnt[] ants =
            {
                ant1,
                ant2
            };

            // Act
            m_Sut.FindBestTrail(ants);

            // Assert
            Assert.AreEqual(1,
                            m_Sut.AlternativeTrails.Count(),
                            "AlternativeTrails Count");
            Assert.AreEqual(builder1,
                            m_Sut.AlternativeTrails.First(),
                            "AlternativeTrails");
        }

        [Test]
        public void FindBestTrail_SetsAlternativeTrails_ForSecondIsBest()
        {
            // Arrange
            ITrailBuilder builder1 = CreateTrailBuilder(new[]
                                                        {
                                                            0,
                                                            1,
                                                            2
                                                        },
                                                        20.0);
            IAnt ant1 = CreateAnt(0,
                                  builder1);

            ITrailBuilder builder2 = CreateTrailBuilder(new[]
                                                        {
                                                            2,
                                                            1,
                                                            0
                                                        },
                                                        10.0);
            IAnt ant2 = CreateAnt(1,
                                  builder2);

            IAnt[] ants =
            {
                ant1,
                ant2
            };

            // Act
            m_Sut.FindBestTrail(ants);

            // Assert
            Assert.AreEqual(1,
                            m_Sut.AlternativeTrails.Count(),
                            "AlternativeTrails Count");
            Assert.AreEqual(builder2,
                            m_Sut.AlternativeTrails.First(),
                            "AlternativeTrails");
        }

        [Test]
        public void FindBestTrail_SetsAlternativeTrails_ForTwoBestTrails()
        {
            // Arrange
            ITrailBuilder trail1 = CreateTrailBuilder(new[]
                                                      {
                                                          0,
                                                          1,
                                                          2
                                                      },
                                                      10.0);
            IAnt ant1 = CreateAnt(0,
                                  trail1);

            ITrailBuilder trail2 = CreateTrailBuilder(new[]
                                                      {
                                                          2,
                                                          1,
                                                          0
                                                      },
                                                      10.0);
            IAnt ant2 = CreateAnt(1,
                                  trail2);

            IAnt[] ants =
            {
                ant1,
                ant2
            };

            // Act
            m_Sut.FindBestTrail(ants);

            // Assert
            Assert.AreEqual(2,
                            m_Sut.AlternativeTrails.Count(),
                            "AlternativeTrails Count");
            Assert.AreEqual(trail1,
                            m_Sut.AlternativeTrails.First(),
                            "AlternativeTrails First");
            Assert.AreEqual(trail2,
                            m_Sut.AlternativeTrails.Last(),
                            "AlternativeTrails Last");
        }

        [Test]
        public void FindBestTrail_SetsBestTrailBuilder_ForFirstIsBest()
        {
            // Arrange
            ITrailBuilder builder1 = CreateTrailBuilder(new[]
                                                        {
                                                            0,
                                                            1,
                                                            2
                                                        },
                                                        10.0);
            IAnt ant1 = CreateAnt(0,
                                  builder1);

            ITrailBuilder builder2 = CreateTrailBuilder(new[]
                                                        {
                                                            2,
                                                            1,
                                                            0
                                                        },
                                                        20.0);
            IAnt ant2 = CreateAnt(1,
                                  builder2);

            IAnt[] ants =
            {
                ant1,
                ant2
            };

            // Act
            m_Sut.FindBestTrail(ants);

            // Assert
            Assert.AreEqual(builder1,
                            m_Sut.BestTrailBuilder,
                            "BestTrail");
        }

        [Test]
        public void FindBestTrail_SetsBestTrailBuilder_ForSecondIsBest()
        {
            // Arrange
            ITrailBuilder builder1 = CreateTrailBuilder(new[]
                                                        {
                                                            0,
                                                            1,
                                                            2
                                                        },
                                                        20.0);
            IAnt ant1 = CreateAnt(0,
                                  builder1);

            ITrailBuilder builder2 = CreateTrailBuilder(new[]
                                                        {
                                                            2,
                                                            1,
                                                            0
                                                        },
                                                        10.0);
            IAnt ant2 = CreateAnt(1,
                                  builder2);

            IAnt[] ants =
            {
                ant1,
                ant2
            };

            // Act
            m_Sut.FindBestTrail(ants);

            // Assert
            Assert.AreEqual(builder2,
                            m_Sut.BestTrailBuilder,
                            "BestTrail");
        }

        [Test]
        public void FindBestTrail_SetsBestTrailBuilder_ForTwoBestTrails()
        {
            // Arrange
            ITrailBuilder trail1 = CreateTrailBuilder(new[]
                                                      {
                                                          0,
                                                          1,
                                                          2
                                                      },
                                                      10.0);
            IAnt ant1 = CreateAnt(0,
                                  trail1);

            ITrailBuilder trail2 = CreateTrailBuilder(new[]
                                                      {
                                                          2,
                                                          1,
                                                          0
                                                      },
                                                      10.0);
            IAnt ant2 = CreateAnt(1,
                                  trail2);

            IAnt[] ants =
            {
                ant1,
                ant2
            };

            // Act
            m_Sut.FindBestTrail(ants);

            // Assert
            Assert.AreEqual(trail1,
                            m_Sut.BestTrailBuilder,
                            "BestTrail");
        }

        [Test]
        public void FindBestTrail_SetsSettings_WhenCalled()
        {
            // Arrange
            ITrailBuilder trail1 = CreateTrailBuilder(new[]
                                                      {
                                                          0,
                                                          1,
                                                          2
                                                      },
                                                      10.0);

            IChromosome chromosome = CreateChromosome();

            IAnt ant1 = CreateAnt(0,
                                  trail1);
            ant1.Chromosome.Returns(chromosome);

            m_Tracker.Rho.Returns(4.0);
            m_Tracker.Q.Returns(5.0);

            IAnt[] ants =
            {
                ant1
            };

            // Act
            m_Sut.FindBestTrail(ants);

            // Assert
            ISettings actual = m_Sut.Settings;

            Assert.AreEqual(chromosome.Alpha,
                            actual.Alpha,
                            "Alpha");
            Assert.AreEqual(chromosome.Beta,
                            actual.Beta,
                            "Beta");
            Assert.AreEqual(chromosome.Gamma,
                            actual.Gamma,
                            "Gamma");
            Assert.AreEqual(m_Tracker.Rho,
                            actual.Rho,
                            "Rho");
            Assert.AreEqual(m_Tracker.Q,
                            actual.Q,
                            "Q");
        }
    }
}