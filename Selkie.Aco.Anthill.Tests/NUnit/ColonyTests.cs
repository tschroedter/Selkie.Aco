using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Anthill.TypedFactories;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails;
using Selkie.Aco.Trails.Optimizers;
using Selkie.Common;
using Selkie.NUnit.Extensions;

namespace Selkie.Aco.Anthill.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    // ReSharper disable once ClassTooBig
    internal sealed class ColonyTests
    {
        [SetUp]
        // ReSharper disable once MethodTooLong
        public void Setup()
        {
            m_Random = new SelkieRandom();
            m_SystemTime = Substitute.For <IDateTime>();
            m_ColonyLogger = Substitute.For <IColonyLogger>();
            m_QueenFactory = new TestQueenFactory();
            m_AntFactory = new TestAntFactory();
            m_ChromosomeFactory = new TestChromosomeFactory();
            m_TrailBuilderFactory = new TestTrailBuilderFactory();
            m_TrailAlternatives = new TrailAlternatives(new TestTrailBuilderFactory(),
                                                        new TestChromosomeFactory());

            m_StartTime = new DateTime(2000,
                                       1,
                                       1);
            m_FinishTime = new DateTime(2000,
                                        1,
                                        2);
            m_SystemTime.Now.Returns(m_StartTime,
                                     m_FinishTime);

            m_CostMatrix = new int[4][];

            m_CostMatrix [ 0 ] = new[]
                                 {
                                     1,
                                     2,
                                     3,
                                     4
                                 };
            m_CostMatrix [ 1 ] = new[]
                                 {
                                     5,
                                     6,
                                     7,
                                     8
                                 };
            m_CostMatrix [ 2 ] = new[]
                                 {
                                     9,
                                     10,
                                     11,
                                     12
                                 };
            m_CostMatrix [ 3 ] = new[]
                                 {
                                     13,
                                     14,
                                     15,
                                     16
                                 };

            m_Graph = new DistanceGraph(m_Random,
                                        new NearestNeighbours(),
                                        m_CostMatrix,
                                        new[]
                                        {
                                            1,
                                            1,
                                            1,
                                            1
                                        });

            m_Pheromones = new Pheromones();
            m_Tracker = new PheromonesTracker(m_Random,
                                              m_Pheromones,
                                              m_Graph);

            m_Optimizer = new TwoOptSimple
                          {
                              DistanceGraph = m_Graph
                          };

            m_NaturalSelectionFactory = new TestNaturalSelectionFactory();

            m_BestTrailFinder = new BestTrailFinder(new Disposer(),
                                                    m_AntFactory,
                                                    m_Graph,
                                                    m_Tracker,
                                                    m_Optimizer,
                                                    m_TrailAlternatives);

            m_BestTrailFinderFactory = Substitute.For <IBestTrailFinderFactory>();
            IBestTrailFinder bestTrailFinder = m_BestTrailFinderFactory.Create(m_Graph,
                                                                               m_Tracker,
                                                                               m_Optimizer);
            bestTrailFinder.ReturnsForAnyArgs(m_BestTrailFinder);
        }

        [TearDown]
        public void Teardown()
        {
            m_BestTrailFinder.Dispose();
        }

        private const int CostToMyself = 0;
        private int[][] m_CostMatrix;
        private IDistanceGraph m_Graph;
        private IPheromonesTracker m_Tracker;

        private readonly int[][] m_CostMatrixSimple =
        {
            new[]
            {
                CostToMyself,
                CostToMyself,
                10,
                1
            },
            new[]
            {
                CostToMyself,
                CostToMyself,
                10,
                10
            },
            new[]
            {
                10,
                10,
                CostToMyself,
                CostToMyself
            },
            new[]
            {
                10,
                10,
                CostToMyself,
                CostToMyself
            }
        };

        private readonly int[][] m_CostMatrixAllSame =
        {
            // Line 1 Forward: Line1-Start to Line1-Start, Line1-Start to Line1-End,	Line1-End   to Line2-Start, Line1-End   to Line2-End
            // Line 1 Reverse: Line1-End   to Line1-Start, Line1-End   to Line1-End,	Line1-Start to Line2-Start, Line1-Start to Line2-End
            // Line 2 Forward: Line2-Start to Line1-Start, Line2-Start to Line1-End,	Line2-End   to Line1-Start, Line2-End   to Line1-End
            // Line 2 Reverse: Line2-End   to Line1-Start, Line2-End   to Line1-End,	Line2-Start to Line1-Start, Line2-Start to Line1-End
            new[]
            {
                CostToMyself,
                CostToMyself,
                50,
                50
            },
            new[]
            {
                CostToMyself,
                CostToMyself,
                50,
                50
            },
            new[]
            {
                50,
                50,
                CostToMyself,
                CostToMyself
            },
            new[]
            {
                50,
                50,
                CostToMyself,
                CostToMyself
            }
        };

        private readonly int[][] m_CostMatricTwoPaths =
        {
            new[]
            {
                CostToMyself,
                CostToMyself,
                10,
                50
            },
            new[]
            {
                CostToMyself,
                CostToMyself,
                50,
                50
            },
            new[]
            {
                50,
                50,
                CostToMyself,
                CostToMyself
            },
            new[]
            {
                50,
                10,
                CostToMyself,
                CostToMyself
            }
        };

        private readonly int[][] m_CostMatrixThreeLines =
        {
            new[]
            {
                CostToMyself,
                CostToMyself,
                6657,
                5472,
                11630,
                10434
            },
            new[]
            {
                CostToMyself,
                CostToMyself,
                6000,
                6000,
                11000,
                11000
            },
            new[]
            {
                11000,
                10472,
                CostToMyself,
                CostToMyself,
                15849,
                11000
            },
            new[]
            {
                11000,
                11657,
                CostToMyself,
                CostToMyself,
                11000,
                15849
            },
            new[]
            {
                22000,
                21434,
                21849,
                17000,
                CostToMyself,
                CostToMyself
            },
            new[]
            {
                22000,
                22630,
                17000,
                21849,
                CostToMyself,
                CostToMyself
            }
        };

        private ITrailBuilderFactory m_TrailBuilderFactory;
        private IOptimizer m_Optimizer;
        private IAntFactory m_AntFactory;
        private IBestTrailFinderFactory m_BestTrailFinderFactory;
        private BestTrailFinder m_BestTrailFinder;
        private ITrailAlternatives m_TrailAlternatives;
        private IQueenFactory m_QueenFactory;
        private IPheromones m_Pheromones;
        private IRandom m_Random;
        private IDateTime m_SystemTime;
        private DateTime m_StartTime;
        private DateTime m_FinishTime;
        private IColonyLogger m_ColonyLogger;
        private INaturalSelectionFactory m_NaturalSelectionFactory;
        private TestChromosomeFactory m_ChromosomeFactory;

        [NotNull]
        private static int[] CreateCostPerLine([NotNull] int[][] costMatrix)
        {
            var costs = new int[costMatrix.GetLength(0)];

            for ( var i = 0 ; i < costs.Length ; i++ )
            {
                costs [ i ] = 100;
            }

            return costs;
        }

        [NotNull]
        private Colony CreateColony([NotNull] IPheromonesTracker tracker,
                                    [NotNull] IDistanceGraph graph)
        {
            return CreateColony(tracker,
                                m_TrailBuilderFactory,
                                graph);
        }

        [NotNull]
        private Colony CreateColony([NotNull] ITrailBuilderFactory factory,
                                    [NotNull] IPheromonesTracker tracker,
                                    [NotNull] IDistanceGraph graph)
        {
            return CreateColony(tracker,
                                factory,
                                graph);
        }

        [NotNull]
        private Colony CreateColony([NotNull] IPheromonesTracker tracker,
                                    [NotNull] ITrailBuilderFactory factory,
                                    [NotNull] IDistanceGraph graph)
        {
            lock ( this )
            {
                var trackerFactory = Substitute.For <IPheromonesTrackerFactory>();
                IPheromonesTracker pheromonesTracker = trackerFactory.Create(graph);
                pheromonesTracker.Returns(tracker);

                return new Colony(m_ColonyLogger,
                                  m_SystemTime,
                                  m_QueenFactory,
                                  m_ChromosomeFactory,
                                  factory,
                                  trackerFactory,
                                  graph,
                                  m_Optimizer,
                                  m_NaturalSelectionFactory);
            }
        }

        [NotNull]
        private DistanceGraph CreateDistanceGraph([NotNull] int[] costPerLine)
        {
            return CreateDistanceGraph(new NearestNeighbours(),
                                       m_CostMatrixAllSame,
                                       costPerLine);
        }

        [NotNull]
        private DistanceGraph CreateDistanceGraph([NotNull] int[][] costMatrix,
                                                  [NotNull] int[] costPerLine)
        {
            return CreateDistanceGraph(new NearestNeighbours(),
                                       costMatrix,
                                       costPerLine);
        }

        [NotNull]
        private DistanceGraph CreateDistanceGraph([NotNull] INearestNeighbours nearestNeighbours,
                                                  [NotNull] int[][] costMatrix,
                                                  [NotNull] int[] costPerLine)
        {
            var graph = new DistanceGraph(m_Random,
                                          nearestNeighbours,
                                          costMatrix,
                                          costPerLine);

            return graph;
        }

        [NotNull]
        private PheromonesTracker CreatePheromonesTracker()
        {
            return CreatePheromonesTracker(m_Graph);
        }

        [NotNull]
        private PheromonesTracker CreatePheromonesTracker([NotNull] IDistanceGraph graph)
        {
            var tracker = new PheromonesTracker(m_Random,
                                                m_Pheromones,
                                                graph);
            return tracker;
        }

        [NotNull]
        private Colony CreateColonyWithCostMatrixAllSame()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);
            return colony;
        }

        [NotNull]
        private static int[][] CreateCostMatrix()
        {
            var costMatrix = new int[4][];

            costMatrix [ 0 ] = new[]
                               {
                                   1,
                                   2,
                                   3,
                                   4
                               };
            costMatrix [ 1 ] = new[]
                               {
                                   5,
                                   6,
                                   7,
                                   8
                               };
            costMatrix [ 2 ] = new[]
                               {
                                   9,
                                   10,
                                   11,
                                   12
                               };
            costMatrix [ 3 ] = new[]
                               {
                                   13,
                                   14,
                                   15,
                                   16
                               };
            return costMatrix;
        }

        [Test]
        public void BestTrailsTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);
            colony.TurnsBeforeSelection = 10;

            colony.Start(100);

            ITrailHistory trailHistory = colony.TrailHistory;

            Assert.NotNull(trailHistory);
            Assert.True(trailHistory.Information.Any(),
                        "Information.Count()");
        }

        [Test]
        public void ConstructorForThreeAntsAndThreeNodeTest()
        {
            int[][] costMatrix = CreateCostMatrix();

            int[] costPerLine =
            {
                1,
                1,
                2,
                2
            };

            var graph = new DistanceGraph(m_Random,
                                          new NearestNeighbours(),
                                          costMatrix,
                                          costPerLine);

            PheromonesTracker tracker = CreatePheromonesTracker();

            Colony colony = CreateColony(tracker,
                                         graph);

            Assert.AreEqual(10,
                            colony.NumberOfAnts,
                            "NumberOfAnts");
            Assert.AreEqual(4,
                            colony.NumberOfNodes,
                            "NumberOfNodes");
            Assert.AreEqual(10,
                            colony.Ants.Count(),
                            "Ants");
            Assert.True(colony.BestTrailBuilder.IsUnknown,
                        "BestTrail");
            Assert.AreEqual(0,
                            colony.Alternatives.Count(),
                            "AlternativeTrails");
        }

        [Test]
        public void ConstructorForTwoAntsAndTwoNodesTest()
        {
            var costMatrix = new int[2][];

            costMatrix [ 0 ] = new[]
                               {
                                   1,
                                   2
                               };
            costMatrix [ 1 ] = new[]
                               {
                                   3,
                                   4
                               };

            int[] costPerLine =
            {
                1,
                1,
                2,
                2
            };

            var graph = new DistanceGraph(m_Random,
                                          new NearestNeighbours(),
                                          costMatrix,
                                          costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker();

            Colony colony = CreateColony(tracker,
                                         graph);

            Assert.AreEqual(10,
                            colony.NumberOfAnts,
                            "NumberOfAnts");
            Assert.AreEqual(2,
                            colony.NumberOfNodes,
                            "NumberOfNodes");
            Assert.AreEqual(10,
                            colony.Ants.Count(),
                            "Ants");
            Assert.True(colony.BestTrailBuilder.IsUnknown,
                        "BestTrail");
            Assert.AreEqual(0,
                            colony.Alternatives.Count(),
                            "AlternativeTrails");
        }

        [Test]
        public void CostMatricAllSameAlternativesTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            colony.Start(100);

            Assert.True(colony.BestTrailBuilder.Length > 0.0,
                        "BestTrailBuilder.Length");
            Assert.True(colony.Alternatives.Any(),
                        "AlternativeTrails.Count()");
        }

        [Test]
        public void CostMatricAllSameBestTrailTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            colony.Start(100);

            ITrailBuilder actual = colony.BestTrailBuilder;

            NUnitHelper.AssertIsEquivalent(150.0,
                                           actual.Length,
                                           Colony.Epsilon,
                                           actual.ToString());
        }

        [Test]
        public void CostMatricTwoPathsAlternativesTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatricTwoPaths);
            DistanceGraph graph = CreateDistanceGraph(new NearestNeighbours(),
                                                      m_CostMatricTwoPaths,
                                                      costPerLine);

            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            colony.Start(100);

            Assert.True(colony.BestTrailBuilder.Length > 0.0,
                        "BestTrailBuilder.Length");
            Assert.True(colony.Alternatives.Any(),
                        "AlternativeTrails.Count()");
        }

        [Test]
        public void CostMatricTwoPathsBestTrailTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatricTwoPaths);
            DistanceGraph graph = CreateDistanceGraph(new NearestNeighbours(),
                                                      m_CostMatricTwoPaths,
                                                      costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            colony.Start(100);

            ITrailBuilder actual = colony.BestTrailBuilder;

            NUnitHelper.AssertIsEquivalent(110.0,
                                           actual.Length,
                                           Colony.Epsilon,
                                           actual.ToString());
        }

        [Test]
        public void CostMatrixSimpleAlternativesTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixSimple);
            DistanceGraph graph = CreateDistanceGraph(new NearestNeighbours(),
                                                      m_CostMatrixSimple,
                                                      costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            colony.Start(100);

            Assert.True(colony.BestTrailBuilder.Length > 0.0,
                        "BestTrailBuilder.Length");
            Assert.True(colony.Alternatives.Any(),
                        "AlternativeTrails.Count()");
        }

        [Test]
        public void CostMatrixSimpleBestTrailTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixSimple);
            DistanceGraph graph = CreateDistanceGraph(new NearestNeighbours(),
                                                      m_CostMatrixSimple,
                                                      costPerLine);

            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            colony.Start(100);

            int[] expected =
            {
                0,
                3
            };
            IEnumerable <int> actual = colony.BestTrailBuilder.Trail;

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void CostMatrixThreeLinesAlternativesTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixThreeLines);
            DistanceGraph graph = CreateDistanceGraph(m_CostMatrixThreeLines,
                                                      costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            colony.Start(10);

            double bestLength = colony.BestTrailBuilder.Length;

            Assert.True(bestLength > 0.0,
                        "BestTrailBuilder.Length");
            Assert.True(colony.Alternatives.Any(),
                        "AlternativeTrails.Count");
        }

        [Test]
        public void CostMatrixThreeLinesBestTrailTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixThreeLines);
            DistanceGraph graph = CreateDistanceGraph(m_CostMatrixThreeLines,
                                                      costPerLine);

            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            colony.Start(100);

            ITrailBuilder actual = colony.BestTrailBuilder;

            Assert.IsNotNull(actual);
            Assert.False(colony.BestTrailBuilder.IsUnknown);
        }

        [Test]
        public void CycleCallsDoCycleTest()
        {
            Colony colony = CreateColonyWithCostMatrixAllSame();

            colony.Cycle(1);

            Assert.True(1 == colony.Time);
        }

        [Test]
        public void CycleCallsPostCycleTest()
        {
            FinishedEventArgs eventArgs = null;

            Colony colony = CreateColonyWithCostMatrixAllSame();
            colony.Finished += ( (sender,
                                  args) => eventArgs = args );
            colony.Cycle(1);

            Assert.NotNull(eventArgs);
        }

        [Test]
        public void CycleCallsPreCycleTest()
        {
            EventArgs eventArgs = null;

            Colony colony = CreateColonyWithCostMatrixAllSame();
            colony.Started += ( (sender,
                                 args) => eventArgs = args );
            colony.Cycle(1);

            Assert.NotNull(eventArgs);
        }

        [Test]
        public void CycleInitializeSetBestTrailBuilderTest()
        {
            var trailBuilder = Substitute.For <IUnknownTrailBuilder>();
            trailBuilder.Clone(Arg.Any <ITrailBuilderFactory>(),
                               Arg.Any <IChromosomeFactory>()).Returns(trailBuilder);
            trailBuilder.Trail.Returns(new[]
                                       {
                                           1,
                                           2
                                       });
            trailBuilder.Length.Returns(2);

            var factory = Substitute.For <ITrailBuilderFactory>();
            var factoryCreate = factory.Create <IUnknownTrailBuilder>(Arg.Any <IChromosome>(),
                                                                      m_Tracker,
                                                                      m_Graph,
                                                                      m_Optimizer,
                                                                      Arg.Any <IEnumerable <int>>());
            factoryCreate.ReturnsForAnyArgs(trailBuilder);

            Colony colony = CreateColony(factory,
                                         m_Tracker,
                                         m_Graph);

            colony.CycleInitialize();

            Assert.AreEqual(trailBuilder,
                            colony.ColonyBestTrailBuilder);
        }

        [Test]
        public void CycleSetFinishTimeTest()
        {
            Colony colony = CreateColonyWithCostMatrixAllSame();

            colony.Cycle(1);

            Assert.AreEqual(m_FinishTime,
                            colony.FinishTime);
        }

        [Test]
        public void CycleSetStartTimeTest()
        {
            Colony colony = CreateColonyWithCostMatrixAllSame();

            colony.Cycle(1);

            Assert.AreEqual(m_StartTime,
                            colony.StartTime);
        }

        [Test]
        public void DefaultPheromonesMaximumTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            NUnitHelper.AssertIsEquivalent(0.02,
                                           colony.PheromonesMaximum);
        }

        [Test]
        public void DefaultPheromonesMinimumTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);

            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            NUnitHelper.AssertIsEquivalent(0.02,
                                           colony.PheromonesMinimum);
        }

        [Test]
        public void DefaultTurnsBeforeSelectionTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            Assert.AreEqual(Colony.DefaultTurnsBeforeSelection,
                            colony.TurnsBeforeSelection);
        }

        [Test]
        public void DefaultTurnsRemainingTest()
        {
            Colony colony = CreateColony(m_Tracker,
                                         m_Graph);

            Assert.AreEqual(2,
                            colony.TurnsRemaining);
        }

        [Test]
        public void DefaultTurnsSinceNewBestTrailFoundTest()
        {
            Colony colony = CreateColony(m_Tracker,
                                         m_Graph);

            Assert.AreEqual(200,
                            colony.TurnsSinceNewBestTrailFound);
        }

        [Test]
        public void EvolveRestartFromBestTrailCallsRestartTest()
        {
            var queen = Substitute.For <IQueen>();

            Colony colony = CreateColony(m_Tracker,
                                         m_Graph);

            colony.EvolveRestartFromBestTrail(queen);

            queen.Received().RestartFromTrail(Arg.Any <IEnumerable <int>>());
        }

        [Test]
        public void EvolveRestartFromBestTrailUpdatesTurnsRemainingTest()
        {
            var queen = Substitute.For <IQueen>();

            Colony colony = CreateColony(m_Tracker,
                                         m_Graph);

            colony.EvolveRestartFromBestTrail(queen);

            Assert.AreEqual(100,
                            colony.TurnsRemaining);
        }

        [Test]
        public void EvolveRestartFromBestTrailUpdatesTurnsSinceNewBestTrailFoundTest()
        {
            var queen = Substitute.For <IQueen>();

            Colony colony = CreateColony(m_Tracker,
                                         m_Graph);

            colony.EvolveRestartFromBestTrail(queen);

            Assert.AreEqual(100 * 2,
                            colony.TurnsSinceNewBestTrailFound);
        }

        [Test]
        public void FindCallColonyLoggerTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            colony.Start(0);

            m_ColonyLogger.Received().LogResult(Arg.Any <TimeSpan>());
        }

        [Test]
        public void IsInvalidTrailReturnsTrueForInvalidTest()
        {
            var builder = Substitute.For <IUnknownTrailBuilder>();
            builder.IsUnknown.Returns(false);
            builder.Trail.Returns(new[]
                                  {
                                      2,
                                      2
                                  });

            Colony colony = CreateColonyWithCostMatrixAllSame();

            bool actual = colony.IsInvalidTrail(builder);

            Assert.True(actual);
        }

        [Test]
        public void IsInvalidTrailReturnsTrueForUnknownTest()
        {
            var builder = Substitute.For <IUnknownTrailBuilder>();
            builder.IsUnknown.Returns(true);

            Colony colony = CreateColonyWithCostMatrixAllSame();

            bool actual = colony.IsInvalidTrail(builder);

            Assert.True(actual);
        }

        [Test]
        public void NewBestTrailIsInvalidLogsErrorTest()
        {
            var builder = Substitute.For <IUnknownTrailBuilder>();
            builder.IsUnknown.Returns(true);

            Colony colony = CreateColonyWithCostMatrixAllSame();

            m_ColonyLogger.ClearReceivedCalls();

            colony.NewBestTrailIsInvalid(builder);

            m_ColonyLogger.Received().Error(Arg.Is <string>(x => x.StartsWith("Found invalid trail")));
        }

        [Test]
        public void PheromonesAverageTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);

            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            colony.Start(10);

            Assert.True(colony.PheromonesMinimum < colony.PheromonesAverage);
        }

        [Test]
        public void PheromonesInformationReturnsInformationTest()
        {
            Colony colony = CreateColony(m_Tracker,
                                         m_Graph);

            PheromonesInformation actual = colony.PheromonesInformation();

            Assert.NotNull(actual);
        }

        [Test]
        public void PheromonesTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            colony.Start(100);

            double[][] actual = colony.PheromonesToArray();

            Assert.AreEqual(4,
                            actual.GetLength(0),
                            ".GetLength(0)");
            Assert.AreEqual(4,
                            actual [ 0 ].Length,
                            "Length");
        }

        [Test]
        public void PostCycleSendsBestTrailMessageTest()
        {
            BestTrailChangedEventArgs eventArgs = null;

            Colony colony = CreateColony(m_Tracker,
                                         m_Graph);

            colony.BestTrailChanged += ( (sender,
                                          args) => eventArgs = args );
            colony.CycleInitialize();
            colony.PostCycle();

            Assert.NotNull(eventArgs);
        }

        [Test]
        public void PostCycleSendsFinishedMessageTest()
        {
            FinishedEventArgs eventArgs = null;

            Colony colony = CreateColony(m_Tracker,
                                         m_Graph);

            colony.Finished += ( (sender,
                                  args) => eventArgs = args );

            colony.CycleInitialize();
            colony.PostCycle();

            Assert.NotNull(eventArgs);
        }

        [Test]
        public void PostCycleSendsStoppedMessageTest()
        {
            EventArgs eventArgs = null;

            Colony colony = CreateColonyWithCostMatrixAllSame();
            colony.Stopped += ( (sender,
                                 args) => eventArgs = args );

            colony.CycleInitialize();
            colony.Stop();
            colony.PostCycle();

            Assert.NotNull(eventArgs);
        }

        [Test]
        public void PreCycleCallsCycleInitializeTest()
        {
            var trailBuilder = Substitute.For <IUnknownTrailBuilder>();
            trailBuilder.Clone(Arg.Any <ITrailBuilderFactory>(),
                               Arg.Any <IChromosomeFactory>()).Returns(trailBuilder);
            trailBuilder.Trail.Returns(new[]
                                       {
                                           1,
                                           2
                                       });
            trailBuilder.Length.Returns(2);

            var factory = Substitute.For <ITrailBuilderFactory>();
            var factoryCreate = factory.Create <IUnknownTrailBuilder>(Arg.Any <IChromosome>(),
                                                                      m_Tracker,
                                                                      m_Graph,
                                                                      m_Optimizer,
                                                                      Arg.Any <IEnumerable <int>>());
            factoryCreate.ReturnsForAnyArgs(trailBuilder);

            Colony colony = CreateColony(factory,
                                         m_Tracker,
                                         m_Graph);

            colony.PreCycle();

            Assert.AreEqual(trailBuilder,
                            colony.ColonyBestTrailBuilder);
        }

        [Test]
        public void RoundTripTurnsBeforeSelectionTest()
        {
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            Assert.AreEqual(100,
                            colony.TurnsBeforeSelection);
        }

        [Test]
        public void RunTimeTest()
        {
            var costMatrix = new int[4][];

            costMatrix [ 0 ] = new[]
                               {
                                   1,
                                   2,
                                   3,
                                   4
                               };
            costMatrix [ 1 ] = new[]
                               {
                                   5,
                                   6,
                                   7,
                                   8
                               };
            costMatrix [ 2 ] = new[]
                               {
                                   9,
                                   10,
                                   11,
                                   12
                               };
            costMatrix [ 3 ] = new[]
                               {
                                   13,
                                   14,
                                   15,
                                   16
                               };

            int[] costPerLine =
            {
                1,
                1,
                2,
                2
            };

            DistanceGraph graph = CreateDistanceGraph(costMatrix,
                                                      costPerLine);

            PheromonesTracker tracker = CreatePheromonesTracker(m_Graph);

            Colony colony = CreateColony(tracker,
                                         graph);

            TimeSpan actual = colony.Runtime;

            Assert.NotNull(actual);
        }

        [Test]
        public void SendBestTrailMessageRaisesEventTest()
        {
            var trailBuilder = Substitute.For <ITrailBuilder>();
            BestTrailChangedEventArgs args = null;

            Colony colony = CreateColony(m_Tracker,
                                         m_Graph);
            colony.BestTrailChanged += ( (sender,
                                          eventArgs) => args = eventArgs );

            colony.SendBestTrailMessage(trailBuilder);

            Assert.NotNull(args);
        }

        [Test]
        public void StoppedHandlerSetsFlagToTrueTest()
        {
            Colony colony = CreateColony(m_Tracker,
                                         m_Graph);

            colony.Stop();

            Assert.True(colony.IsRequestedToStop);
        }

        [Test]
        public void TimeDefaultTest()
        {
            Colony colony = CreateColonyWithCostMatrixAllSame();

            Assert.AreEqual(0,
                            colony.Time);
        }

        [Test]
        public void TimeTest()
        {
            Colony colony = CreateColonyWithCostMatrixAllSame();

            colony.Start(10);

            Assert.AreEqual(10,
                            colony.Time);
        }
    }
}