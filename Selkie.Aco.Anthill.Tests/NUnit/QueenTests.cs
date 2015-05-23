using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Castle.Core.Logging;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Anthill.TypedFactories;
using Selkie.Aco.Ants;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails;
using Selkie.Aco.Trails.Optimizers;
using Selkie.Common;

namespace Selkie.Aco.Anthill.Tests.NUnit
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
        private ILogger m_Logger;
        private int[][] m_Neighbours;
        private IOptimizer m_Optimizer;
        private Queen m_Queen;
        private IRandom m_Random;
        private ISquad m_Squad;
        private ISquadFactory m_SquadFactory;
        private IPheromonesTracker m_Tracker;
        private ITrailAlternatives m_TrailAlternatives;

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

        [Test]
        public void BestAntDefaultTest()
        {
            Assert.AreEqual(typeof ( IUnknownAnt ).Name,
                            m_Queen.BestAnt.Type);
        }

        [Test]
        public void BestTrailBuilderDefaultTest()
        {
            Assert.AreEqual(typeof ( IUnknownTrailBuilder ).Name,
                            m_Queen.BestTrailBuilder.Type);
        }

        [Test]
        public void BestTrailDefaultTest()
        {
            var builder = Substitute.For <ITrailBuilder>();
            var finder = Substitute.For <IBestTrailFinder>();
            finder.BestTrailBuilder.Returns(builder);
            var factory = Substitute.For <IBestTrailFinderFactory>();
            factory.Create(m_Graph,
                           m_Tracker,
                           m_Optimizer).ReturnsForAnyArgs(finder);

            var queen = new Queen(m_Logger,
                                  m_AntFactory,
                                  m_ChromosomeFactory,
                                  factory,
                                  m_Graph,
                                  m_Tracker,
                                  m_Optimizer,
                                  m_Crossover,
                                  m_SquadFactory);

            ITrailBuilder actual = queen.BestTrailBuilder;

            Assert.AreEqual(builder,
                            actual);
        }

        [Test]
        public void ClearTest()
        {
            var finder = Substitute.For <IBestTrailFinder>();
            var factory = Substitute.For <IBestTrailFinderFactory>();
            factory.Create(m_Graph,
                           m_Tracker,
                           m_Optimizer).Returns(finder);

            var queen = new Queen(m_Logger,
                                  m_AntFactory,
                                  m_ChromosomeFactory,
                                  factory,
                                  m_Graph,
                                  m_Tracker,
                                  m_Optimizer,
                                  m_Crossover,
                                  m_SquadFactory);

            queen.UpdateAnts();
            queen.Clear();

            finder.Received().Clear();
            m_Tracker.Received().Clear();
        }

        [Test]
        public void NaturalSelectionTest()
        {
            IChromosome[] old = m_Queen.Ants.Select(x => x.Chromosome).ToArray();

            m_Queen.NaturalSelection(new Chromosome(m_Random),
                                     new Chromosome(m_Random));

            IChromosome[] actual = m_Queen.Ants.Select(x => x.Chromosome).ToArray();

            Assert.AreEqual(10,
                            m_Queen.NumberOfAnts,
                            "NumberOfAnts");

            // add least one chromosome should be different
            var count = 0;

            for ( var i = 0 ; i < old.Count() ; i++ )
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
            Assert.AreEqual(10,
                            m_Queen.NumberOfAnts);
        }

        [Test]
        public void NumberOfNodesDefaulTest()
        {
            Assert.AreEqual(4,
                            m_Queen.NumberOfNodes);
        }

        [Test]
        public void RandomSelectionTest()
        {
            IChromosome[] old = m_Queen.Ants.Select(x => x.Chromosome.Clone(m_ChromosomeFactory)).ToArray();

            m_Queen.RandomSelection();

            IChromosome[] actual = m_Queen.Ants.Select(x => x.Chromosome).ToArray();

            Assert.AreEqual(10,
                            m_Queen.NumberOfAnts,
                            "NumberOfAnts");

            for ( var i = 0 ; i < old.Count() ; i++ )
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
            var finder = Substitute.For <IBestTrailFinder>();
            var factory = Substitute.For <IBestTrailFinderFactory>();
            factory.Create(m_Graph,
                           m_Tracker,
                           m_Optimizer).Returns(finder);

            var queen = new Queen(m_Logger,
                                  m_AntFactory,
                                  m_ChromosomeFactory,
                                  factory,
                                  m_Graph,
                                  m_Tracker,
                                  m_Optimizer,
                                  m_Crossover,
                                  m_SquadFactory);

            queen.UpdateAnts();

            finder.Received().FindBestTrail(Arg.Any <IAnt[]>());
        }

        [Test]
        public void UpdateAntsCallsTrackerUpdateTest()
        {
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

            var queen = new Queen(m_Logger,
                                  m_AntFactory,
                                  m_ChromosomeFactory,
                                  factory,
                                  m_Graph,
                                  tracker,
                                  m_Optimizer,
                                  m_Crossover,
                                  m_SquadFactory);

            queen.UpdateAnts(queen.Ants);

            tracker.Received().Update(bestAnt);
        }

        [Test]
        public void UpdateAntsCallsUpdateOnAntTest()
        {
            var ant1 = Substitute.For <IAnt>();
            ant1.Id.Returns(1);
            var ant2 = Substitute.For <IAnt>();
            ant2.Id.Returns(2);
            IAnt[] ants =
            {
                ant1,
                ant2
            };

            m_Queen.UpdateAnts(ants);

            ant1.Received().Update();
            ant2.Received().Update();
        }

        [Test]
        public void UpdateAntsCallsUpdateOnTrackerTest()
        {
            var ant1 = Substitute.For <IAnt>();
            ant1.Id.Returns(1);
            var ant2 = Substitute.For <IAnt>();
            ant2.Id.Returns(2);
            IAnt[] ants =
            {
                ant1,
                ant2
            };

            m_Queen.UpdateAnts(ants);

            m_Tracker.ReceivedWithAnyArgs().Update(Substitute.For <IAnt>());
        }

        [Test]
        public void UpdateAntsCallsUpdateTest()
        {
            m_Queen.UpdateAnts();

            m_Tracker.ReceivedWithAnyArgs().Update(Substitute.For <IAnt>());
        }

        [Test]
        public void UpdateAntsUpdatesBestAntTest()
        {
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

            var queen = new Queen(m_Logger,
                                  m_AntFactory,
                                  m_ChromosomeFactory,
                                  factory,
                                  m_Graph,
                                  tracker,
                                  m_Optimizer,
                                  m_Crossover,
                                  m_SquadFactory);

            queen.UpdateAnts(queen.Ants);

            Assert.AreEqual(bestAnt,
                            queen.TotalBestAnt);
        }

        [Test]
        public void UpdateAntsUsingOffspringCallsMutationTest()
        {
            var male = new Chromosome(m_Random);
            var female = new Chromosome(m_Random);
            var crossover = Substitute.For <ICrossover>();

            var queen = new Queen(m_Logger,
                                  m_AntFactory,
                                  m_ChromosomeFactory,
                                  m_BestTrailFinderFactory,
                                  m_Graph,
                                  m_Tracker,
                                  m_Optimizer,
                                  crossover,
                                  m_SquadFactory);

            crossover.ClearReceivedCalls();

            queen.UpdateAntsUsingOffspring(male,
                                           female);

            crossover.Received(10).Mutation(Arg.Any <IChromosome>());
        }

        [Test]
        public void UpdateAntsUsingOffspringCallsOffspringTest()
        {
            var male = new Chromosome(m_Random);
            var female = new Chromosome(m_Random);
            var crossover = Substitute.For <ICrossover>();

            var queen = new Queen(m_Logger,
                                  m_AntFactory,
                                  m_ChromosomeFactory,
                                  m_BestTrailFinderFactory,
                                  m_Graph,
                                  m_Tracker,
                                  m_Optimizer,
                                  crossover,
                                  m_SquadFactory);

            crossover.ClearReceivedCalls();

            queen.UpdateAntsUsingOffspring(male,
                                           female);

            crossover.Received(10).Offspring(Arg.Any <IChromosome>(),
                                             Arg.Any <IChromosome>());
        }

        [Test]
        public void UpdateBestAntCallsUpdateOnTrackerTest()
        {
            m_Queen.UpdateBestAnt();

            m_Tracker.ReceivedWithAnyArgs().Update(Substitute.For <IAnt>());
        }

        [Test]
        // ReSharper disable MethodTooLong
        public void UpdateBestAntDoesNotSetsNewBestWhenNewIsLongerTest()
        {
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

            var queen = new Queen(m_Logger,
                                  m_AntFactory,
                                  m_ChromosomeFactory,
                                  factory,
                                  m_Graph,
                                  m_Tracker,
                                  m_Optimizer,
                                  m_Crossover,
                                  m_SquadFactory);

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

            var queen = new Queen(m_Logger,
                                  m_AntFactory,
                                  m_ChromosomeFactory,
                                  factory,
                                  m_Graph,
                                  m_Tracker,
                                  m_Optimizer,
                                  m_Crossover,
                                  m_SquadFactory);

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

            var queen = new Queen(m_Logger,
                                  m_AntFactory,
                                  m_ChromosomeFactory,
                                  factory,
                                  m_Graph,
                                  m_Tracker,
                                  m_Optimizer,
                                  m_Crossover,
                                  m_SquadFactory);

            queen.UpdateBestAnt();

            Assert.AreEqual(bestAnt,
                            queen.TotalBestAnt);
        }

        [Test]
        public void UpdateChromosomesTest()
        {
            IChromosome[] old = m_Queen.Ants.Select(x => x.Chromosome).ToArray();

            m_Queen.UpdateChromosomes(new Chromosome(m_Random));

            IChromosome[] actual = m_Queen.Ants.Select(x => x.Chromosome).ToArray();

            Assert.AreEqual(10,
                            m_Queen.NumberOfAnts,
                            "NumberOfAnts");

            // add least one chromosome should be different
            var count = 0;

            for ( var i = 0 ; i < old.Count() ; i++ )
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
            m_Logger = Substitute.For <ILogger>();
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

            m_Squad = new Squad(m_Disposer,
                                m_Logger,
                                m_Random,
                                m_AntFactory,
                                m_Graph,
                                m_Tracker,
                                m_Optimizer);

            m_SquadFactory = Substitute.For <ISquadFactory>();
            m_SquadFactory.Create(Arg.Any <IDistanceGraph>(),
                                  Arg.Any <IPheromonesTracker>(),
                                  Arg.Any <IOptimizer>()).Returns(m_Squad);

            m_Queen = new Queen(m_Logger,
                                m_AntFactory,
                                m_ChromosomeFactory,
                                m_BestTrailFinderFactory,
                                m_Graph,
                                m_Tracker,
                                m_Optimizer,
                                m_Crossover,
                                m_SquadFactory);
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