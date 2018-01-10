using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Core2.Selkie.Aco.Anthill.Interfaces;
using Core2.Selkie.Aco.Anthill.TypedFactories;
using Core2.Selkie.Aco.Ants;
using Core2.Selkie.Aco.Ants.Interfaces;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Common.TypedFactories;
using Core2.Selkie.Aco.Trails;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Aco.Trails.Optimizers;
using Core2.Selkie.Common;
using Core2.Selkie.Common.Interfaces;
using Core2.Selkie.Windsor.Interfaces;

namespace Core2.Selkie.Aco.Anthill.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    // ReSharper disable ClassTooBig
    // ReSharper disable MaximumChainedReferences
    internal sealed class QueenTests
    {
        private IAntFactory m_AntFactory;
        private IBestTrailFinder m_BestTrailFinder;
        private IBestTrailFinderFactory m_BestTrailFinderFactory;
        private IChromosomeFactory m_ChromosomeFactory;
        private ICrossover m_Crossover;
        private IDisposer m_Disposer;
        private IDistanceGraph m_Graph;
        private ISelkieLogger m_Logger;
        private int[][] m_Neighbours;
        private IOptimizer m_Optimizer;
        private Queen m_Sut;
        private IRandom m_Random;
        private ISquad m_Squad;
        private ISquadFactory m_SquadFactory;
        private IPheromonesTracker m_Tracker;
        private ITrailAlternatives m_TrailAlternatives;
        private IAntSettings m_AntSettings;

        [NotNull]
        private IDistanceGraph CreateGraph()
        {
            var graph = Substitute.For <IDistanceGraph>();

            const int numberOfNodes = 4;

            graph.NumberOfNodes.Returns(numberOfNodes);
            graph.NumberOfUniqueNodes.Returns(numberOfNodes / 2);
            graph.GetNeighbours(0).Returns(m_Neighbours [ 0 ]);
            graph.GetNeighbours(1).Returns(m_Neighbours [ 1 ]);
            graph.GetNeighbours(2).Returns(m_Neighbours [ 2 ]);
            graph.GetNeighbours(3).Returns(m_Neighbours [ 3 ]);
            graph.MinimumDistance.Returns(1.0);

            return graph;
        }

        [NotNull]
        private static int[][] CreateNeighbours()
        {
            return new[]
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
                           0,
                           1,
                           2,
                           3
                       },
                       new[]
                       {
                           0,
                           1,
                           2,
                           3
                       },
                       new[]
                       {
                           0,
                           1,
                           2,
                           3
                       }
                   };
        }

        private Queen CreateQueen([NotNull] IBestTrailFinderFactory factory,
                                  [NotNull] IPheromonesTracker tracker,
                                  [NotNull] ICrossover crossover)
        {
            var queen = new Queen(m_Logger,
                                  m_AntFactory,
                                  m_ChromosomeFactory,
                                  factory,
                                  m_Graph,
                                  tracker,
                                  m_Optimizer,
                                  crossover,
                                  m_AntSettings,
                                  m_SquadFactory);
            return queen;
        }

        private Queen CreateQueen([NotNull] IBestTrailFinderFactory factory,
                                  [NotNull] IPheromonesTracker tracker)
        {
            return CreateQueen(factory,
                               tracker,
                               m_Crossover);
        }

        private Queen CreateQueen([NotNull] IBestTrailFinderFactory factory)
        {
            return CreateQueen(factory,
                               m_Tracker,
                               m_Crossover);
        }

        private Queen CreateQueen([NotNull] ICrossover crossover)
        {
            return CreateQueen(m_BestTrailFinderFactory,
                               m_Tracker,
                               crossover);
        }

        private Queen CreateQueen()
        {
            return CreateQueen(m_BestTrailFinderFactory,
                               m_Tracker,
                               m_Crossover);
        }

        [Test]
        public void BestAntDefaultTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(typeof( IUnknownAnt ).Name,
                            m_Sut.BestAnt.Type);
        }

        [Test]
        public void BestTrailBuilderDefaultTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(typeof( IUnknownTrailBuilder ).Name,
                            m_Sut.BestTrailBuilder.Type);
        }

        [Test]
        public void BestTrailDefaultTest()
        {
            // Arrange
            // Act
            // Assert
            var builder = Substitute.For <ITrailBuilder>();
            var finder = Substitute.For <IBestTrailFinder>();
            finder.BestTrailBuilder.Returns(builder);
            var factory = Substitute.For <IBestTrailFinderFactory>();
            factory.Create(m_Graph,
                           m_Tracker,
                           m_Optimizer).ReturnsForAnyArgs(finder);

            Queen queen = CreateQueen(factory);

            ITrailBuilder actual = queen.BestTrailBuilder;

            Assert.AreEqual(builder,
                            actual);
        }

        [Test]
        public void ClearTest()
        {
            // Arrange
            // Act
            // Assert
            var finder = Substitute.For <IBestTrailFinder>();
            var factory = Substitute.For <IBestTrailFinderFactory>();
            factory.Create(m_Graph,
                           m_Tracker,
                           m_Optimizer).Returns(finder);

            Queen queen = CreateQueen(factory);

            queen.UpdateAnts();
            queen.Clear();

            finder.Received().Clear();
            m_Tracker.Received().Clear();
        }

        [Test]
        public void NaturalSelectionTest()
        {
            // Arrange
            // Act
            // Assert
            IChromosome[] old = m_Sut.Ants.Select(x => x.Chromosome).ToArray();

            m_Sut.NaturalSelection(new Chromosome(m_Random),
                                   new Chromosome(m_Random));

            IChromosome[] actual = m_Sut.Ants.Select(x => x.Chromosome).ToArray();

            Assert.AreEqual(10,
                            m_Sut.NumberOfAnts,
                            "NumberOfAnts");

            // add least one chromosome should be different
            var count = 0;

            for ( var i = 0 ; i < old.Length ; i++ )
            {
                if ( !old [ i ].Equals(actual [ i ]) )
                {
                    count++;
                }
            }

            Assert.True(count > 0,
                        "At least one chromosome should be different!");
        }

        [Test]
        public void NumberOfAntsDefaulTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(10,
                            m_Sut.NumberOfAnts);
        }

        [Test]
        public void NumberOfNodesDefaulTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(4,
                            m_Sut.NumberOfNodes);
        }

        [Test]
        public void RandomSelectionTest()
        {
            // Arrange
            // Act
            // Assert
            IChromosome[] old = m_Sut.Ants.Select(x => x.Chromosome.Clone(m_ChromosomeFactory)).ToArray();

            m_Sut.RandomSelection();

            IChromosome[] actual = m_Sut.Ants.Select(x => x.Chromosome).ToArray();

            Assert.AreEqual(10,
                            m_Sut.NumberOfAnts,
                            "NumberOfAnts");

            for ( var i = 0 ; i < old.Length ; i++ )
            {
                Assert.AreNotEqual(old [ i ].Alpha,
                                   actual [ i ].Alpha,
                                   "Alpha [" + i + "]");
                Assert.AreNotEqual(old [ i ].Beta,
                                   actual [ i ].Beta,
                                   "Beta [" + i + "]");
                Assert.AreNotEqual(old [ i ].Gamma,
                                   actual [ i ].Gamma,
                                   "Gamma [" + i + "]");
            }
        }

        [Test]
        public void UpdateAntsCallsFindBestTrailTest()
        {
            // Arrange
            // Act
            // Assert
            var finder = Substitute.For <IBestTrailFinder>();
            var factory = Substitute.For <IBestTrailFinderFactory>();
            factory.Create(m_Graph,
                           m_Tracker,
                           m_Optimizer).Returns(finder);

            Queen queen = CreateQueen(factory);

            queen.UpdateAnts();

            finder.Received().FindBestTrail(Arg.Any <IAnt[]>());
        }

        [Test]
        public void UpdateAntsCallsTrackerUpdateTest()
        {
            // Arrange
            // Act
            // Assert
            var tracker = Substitute.For <IPheromonesTracker>();

            var bestAnt = Substitute.For <IAnt>();
            bestAnt.Type.Returns("IStandardAnt");
            bestAnt.Id.Returns(1111);
            bestAnt.Clone(Arg.Any <IAntFactory>(),
                          Arg.Any <IChromosomeFactory>()).Returns(bestAnt);
            bestAnt.TrailBuilder.Length.Returns(123.0);

            var finder = Substitute.For <IBestTrailFinder>();
            finder.BestAnt.Returns(bestAnt);

            var factory = Substitute.For <IBestTrailFinderFactory>();
            factory.Create(m_Graph,
                           tracker,
                           m_Optimizer).ReturnsForAnyArgs(finder);

            Queen queen = CreateQueen(factory,
                                      tracker);

            queen.UpdateAnts(queen.Ants);

            tracker.Received().Update(bestAnt);
        }

        [Test]
        public void UpdateAntsCallsUpdateOnAntTest()
        {
            // Arrange
            // Act
            // Assert
            var ant1 = Substitute.For <IAnt>();
            ant1.Id.Returns(1);
            var ant2 = Substitute.For <IAnt>();
            ant2.Id.Returns(2);
            IAnt[] ants =
            {
                ant1,
                ant2
            };

            m_Sut.UpdateAnts(ants);

            ant1.Received().Update();
            ant2.Received().Update();
        }

        [Test]
        public void UpdateAntsCallsUpdateOnTrackerTest()
        {
            // Arrange
            // Act
            // Assert
            var ant1 = Substitute.For <IAnt>();
            ant1.Id.Returns(1);
            var ant2 = Substitute.For <IAnt>();
            ant2.Id.Returns(2);
            IAnt[] ants =
            {
                ant1,
                ant2
            };

            m_Sut.UpdateAnts(ants);

            m_Tracker.ReceivedWithAnyArgs().Update(Substitute.For <IAnt>());
        }

        [Test]
        public void UpdateAntsCallsUpdateTest()
        {
            // Arrange
            // Act
            // Assert
            m_Sut.UpdateAnts();

            m_Tracker.ReceivedWithAnyArgs().Update(Substitute.For <IAnt>());
        }

        [Test]
        public void UpdateAntsUpdatesBestAntTest()
        {
            // Arrange
            // Act
            // Assert
            var tracker = Substitute.For <IPheromonesTracker>();

            var bestAnt = Substitute.For <IAnt>();
            bestAnt.Id.Returns(1111);
            bestAnt.Clone(Arg.Any <IAntFactory>(),
                          Arg.Any <IChromosomeFactory>()).Returns(bestAnt);
            bestAnt.TrailBuilder.Length.Returns(123.0);

            var finder = Substitute.For <IBestTrailFinder>();
            finder.BestAnt.Returns(bestAnt);

            var factory = Substitute.For <IBestTrailFinderFactory>();
            factory.Create(m_Graph,
                           tracker,
                           m_Optimizer).ReturnsForAnyArgs(finder);

            Queen queen = CreateQueen(factory,
                                      tracker);

            queen.UpdateAnts(queen.Ants);

            Assert.AreEqual(bestAnt,
                            queen.TotalBestAnt);
        }

        [Test]
        public void UpdateAntsUsingOffspringCallsMutationTest()
        {
            // Arrange
            // Act
            // Assert
            var male = new Chromosome(m_Random);
            var female = new Chromosome(m_Random);
            var crossover = Substitute.For <ICrossover>();

            Queen queen = CreateQueen(crossover);

            crossover.ClearReceivedCalls();

            queen.UpdateAntsUsingOffspring(male,
                                           female);

            crossover.Received(10).Mutation(Arg.Any <IChromosome>());
        }

        [Test]
        public void UpdateAntsUsingOffspringCallsOffspringTest()
        {
            // Arrange
            // Act
            // Assert
            var male = new Chromosome(m_Random);
            var female = new Chromosome(m_Random);
            var crossover = Substitute.For <ICrossover>();

            Queen queen = CreateQueen(crossover);

            crossover.ClearReceivedCalls();

            queen.UpdateAntsUsingOffspring(male,
                                           female);

            crossover.Received(10).Offspring(Arg.Any <IChromosome>(),
                                             Arg.Any <IChromosome>());
        }

        [Test]
        public void UpdateBestAntCallsUpdateOnTrackerTest()
        {
            // Arrange
            // Act
            // Assert
            m_Sut.UpdateBestAnt();

            m_Tracker.ReceivedWithAnyArgs().Update(Substitute.For <IAnt>());
        }

        [Test]
        // ReSharper disable MethodTooLong
        public void UpdateBestAntDoesNotSetsNewBestWhenNewIsLongerTest()
        {
            // Arrange
            // Act
            // Assert
            var builderOne = Substitute.For <ITrailBuilder>();
            builderOne.Length.Returns(1111.0);
            var bestAnt = Substitute.For <IAnt>();
            bestAnt.Type.Returns("IStandardAnt");
            bestAnt.Id.Returns(1111);
            bestAnt.TrailBuilder.Returns(builderOne);
            bestAnt.Clone(Arg.Any <IAntFactory>(),
                          Arg.Any <IChromosomeFactory>()).Returns(bestAnt);

            var finder = Substitute.For <IBestTrailFinder>();
            finder.BestAnt.Returns(bestAnt);

            var factory = Substitute.For <IBestTrailFinderFactory>();
            factory.Create(m_Graph,
                           m_Tracker,
                           m_Optimizer).ReturnsForAnyArgs(finder);

            Queen queen = CreateQueen(factory);

            queen.UpdateBestAnt();

            // precondition
            Assert.AreEqual(bestAnt,
                            queen.BestAnt);

            var builderTwo = Substitute.For <ITrailBuilder>();
            builderTwo.Length.Returns(2222.0);
            var newBestAnt = Substitute.For <IAnt>();
            newBestAnt.Id.Returns(2222);
            newBestAnt.TrailBuilder.Returns(builderTwo);
            newBestAnt.Clone(Arg.Any <IAntFactory>(),
                             Arg.Any <IChromosomeFactory>()).Returns(newBestAnt);
            finder.BestAnt.Returns(newBestAnt);

            // test
            queen.UpdateBestAnt();

            Assert.AreEqual(bestAnt,
                            queen.TotalBestAnt);
        }

        // ReSharper restore MethodTooLong
        [Test]
        // ReSharper disable MethodTooLong
        public void UpdateBestAntSetsNewBestWhenNewIsShorterTest()
        {
            // Arrange
            // Act
            // Assert
            var bestAnt = Substitute.For <IAnt>();
            bestAnt.Type.Returns("IStandardAnt");
            bestAnt.Id.Returns(1111);
            bestAnt.Clone(Arg.Any <IAntFactory>(),
                          Arg.Any <IChromosomeFactory>()).Returns(bestAnt);
            bestAnt.TrailBuilder.Length.Returns(123.0);

            var finder = Substitute.For <IBestTrailFinder>();
            finder.BestAnt.Returns(bestAnt);

            var factory = Substitute.For <IBestTrailFinderFactory>();
            factory.Create(m_Graph,
                           m_Tracker,
                           m_Optimizer).ReturnsForAnyArgs(finder);

            Queen queen = CreateQueen(factory);

            queen.UpdateBestAnt();

            // precondition
            Assert.AreEqual(bestAnt,
                            queen.BestAnt);

            var newBestAnt = Substitute.For <IAnt>();
            newBestAnt.Type.Returns("IStandardAnt");
            newBestAnt.Id.Returns(2222);
            newBestAnt.TrailBuilder.Length.Returns(22);
            newBestAnt.Clone(Arg.Any <IAntFactory>(),
                             Arg.Any <IChromosomeFactory>()).Returns(newBestAnt);
            finder.BestAnt.Returns(newBestAnt);

            // test
            queen.UpdateBestAnt();

            Assert.AreEqual(newBestAnt,
                            queen.TotalBestAnt);
        }

        // ReSharper restore MethodTooLong
        [Test]
        public void UpdateBestAntSetsNewBestWhenStillUnknownTest()
        {
            // Arrange
            // Act
            // Assert
            var bestAnt = Substitute.For <IAnt>();
            bestAnt.Type.Returns("IStandardAnt");
            bestAnt.Id.Returns(1111);
            bestAnt.Clone(Arg.Any <IAntFactory>(),
                          Arg.Any <IChromosomeFactory>()).Returns(bestAnt);
            bestAnt.TrailBuilder.Length.Returns(123.0);

            var finder = Substitute.For <IBestTrailFinder>();
            finder.BestAnt.Returns(bestAnt);

            var factory = Substitute.For <IBestTrailFinderFactory>();
            factory.Create(m_Graph,
                           m_Tracker,
                           m_Optimizer).ReturnsForAnyArgs(finder);

            Queen queen = CreateQueen(factory);

            queen.UpdateBestAnt();

            Assert.AreEqual(bestAnt,
                            queen.TotalBestAnt);
        }

        [Test]
        public void UpdateChromosomesTest()
        {
            // Arrange
            // Act
            // Assert
            IChromosome[] old = m_Sut.Ants.Select(x => x.Chromosome).ToArray();

            m_Sut.UpdateChromosomes(new Chromosome(m_Random));

            IChromosome[] actual = m_Sut.Ants.Select(x => x.Chromosome).ToArray();

            Assert.AreEqual(10,
                            m_Sut.NumberOfAnts,
                            "NumberOfAnts");

            // add least one chromosome should be different
            var count = 0;

            for ( var i = 0 ; i < old.Length ; i++ )
            {
                if ( !old [ i ].Equals(actual [ i ]) )
                {
                    count++;
                }
            }

            Assert.True(count > 0,
                        "At least one chromosome should be different!");
        }

        [SetUp]
        // ReSharper disable MethodTooLong
        public void Setup()
        {
            m_Disposer = Substitute.For <IDisposer>();
            m_Neighbours = CreateNeighbours();
            m_Logger = Substitute.For <ISelkieLogger>();
            m_Random = new SelkieRandom();
            m_ChromosomeFactory = new TestChromosomeFactory();
            m_AntFactory = new TestAntFactory();
            m_TrailAlternatives = new TrailAlternatives(new TestTrailBuilderFactory(),
                                                        new TestChromosomeFactory());
            m_Graph = CreateGraph();

            m_Tracker = Substitute.For <IPheromonesTracker>();

            m_Optimizer = new TwoOptSimple
                          {
                              DistanceGraph = m_Graph
                          };

            m_BestTrailFinder = new BestTrailFinder(new Disposer(),
                                                    m_AntFactory,
                                                    m_Graph,
                                                    m_Tracker,
                                                    m_Optimizer,
                                                    m_TrailAlternatives);

            m_BestTrailFinderFactory = Substitute.For <IBestTrailFinderFactory>();
            m_BestTrailFinderFactory.Create(m_Graph,
                                            m_Tracker,
                                            m_Optimizer).Returns(m_BestTrailFinder);

            m_Crossover = new Crossover(m_Disposer,
                                        m_Logger,
                                        m_Random,
                                        m_ChromosomeFactory);

            m_AntSettings = Substitute.For <IAntSettings>();

            m_Squad = new Squad(m_Disposer,
                                m_Logger,
                                m_Random,
                                m_AntFactory,
                                m_Graph,
                                m_Tracker,
                                m_Optimizer,
                                m_AntSettings);

            m_SquadFactory = Substitute.For <ISquadFactory>();
            m_SquadFactory.Create(Arg.Any <IDistanceGraph>(),
                                  Arg.Any <IPheromonesTracker>(),
                                  Arg.Any <IOptimizer>(),
                                  Arg.Any <IAntSettings>()).Returns(m_Squad);

            m_Sut = CreateQueen();
        }

        [TearDown]
        public void Teardown()
        {
            m_Squad.Dispose();
            m_Crossover.Dispose();
        }

        // ReSharper restore MethodTooLong
    }
}