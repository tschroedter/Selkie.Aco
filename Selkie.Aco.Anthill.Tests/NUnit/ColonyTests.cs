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
                                            1000,
                                            1,
                                            1000,
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
        public void Constructor_ForThreeAntsAndThreeNode()
        {
            // Arrange
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

            // Act
            Colony sut = CreateColony(tracker,
                                      graph);

            // Assert
            Assert.AreEqual(10,
                            sut.NumberOfAnts,
                            "NumberOfAnts");
            Assert.AreEqual(4,
                            sut.NumberOfNodes,
                            "NumberOfNodes");
            Assert.AreEqual(10,
                            sut.Ants.Count(),
                            "Ants");
            Assert.True(sut.BestTrailBuilder.IsUnknown,
                        "BestTrail");
            Assert.AreEqual(0,
                            sut.Alternatives.Count(),
                            "AlternativeTrails");
        }

        [Test]
        public void Constructor_ForTwoAntsAndTwoNodes()
        {
            // Arrange
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

            // Act
            Colony sut = CreateColony(tracker,
                                      graph);

            // Assert
            Assert.AreEqual(10,
                            sut.NumberOfAnts,
                            "NumberOfAnts");
            Assert.AreEqual(2,
                            sut.NumberOfNodes,
                            "NumberOfNodes");
            Assert.AreEqual(10,
                            sut.Ants.Count(),
                            "Ants");
            Assert.True(sut.BestTrailBuilder.IsUnknown,
                        "BestTrail");
            Assert.AreEqual(0,
                            sut.Alternatives.Count(),
                            "AlternativeTrails");
        }

        [Test]
        public void Cycle_CallsDoCycle_WhenCalled()
        {
            // Arrange
            Colony sut = CreateColonyWithCostMatrixAllSame();

            // Act
            sut.Cycle(1);

            // Assert
            Assert.AreEqual(1,
                            sut.Time);
        }

        [Test]
        public void Cycle_CallsPostCycle_WhenCalledt()
        {
            // Arrange
            FinishedEventArgs eventArgs = null;

            Colony sut = CreateColonyWithCostMatrixAllSame();
            sut.Finished += (sender,
                             args) => eventArgs = args;

            // Act
            sut.Cycle(1);

            // Assert
            Assert.NotNull(eventArgs);
        }

        [Test]
        public void Cycle_CallsPreCycle_WhenCalled()
        {
            // Arrange
            EventArgs eventArgs = null;

            Colony sut = CreateColonyWithCostMatrixAllSame();
            sut.Started += (sender,
                            args) => eventArgs = args;

            // Act
            sut.Cycle(1);

            // Assert
            Assert.NotNull(eventArgs);
        }

        [Test]
        public void Cycle_InitializeSetBestTrailBuilder_WhenCalled()
        {
            // Arrange
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

            Colony sut = CreateColony(factory,
                                      m_Tracker,
                                      m_Graph);

            // Act
            sut.CycleInitialize();

            // Assert
            Assert.AreEqual(trailBuilder,
                            sut.ColonyBestTrailBuilder);
        }

        [Test]
        public void Cycle_SetFinishTime_WhenCalled()
        {
            // Arrange

            Colony sut = CreateColonyWithCostMatrixAllSame();

            // Act
            sut.Cycle(1);

            // Assert
            Assert.AreEqual(m_FinishTime,
                            sut.FinishTime);
        }

        [Test]
        public void Cycle_SetStartTime_WhenCalled()
        {
            // Arrange
            Colony sut = CreateColonyWithCostMatrixAllSame();

            // Act
            sut.Cycle(1);

            // Assert
            Assert.AreEqual(m_StartTime,
                            sut.StartTime);
        }

        [Test]
        public void EvolveRestartFromBestTrail_CallsRestart_WhenCalled()
        {
            // Arrange
            var queen = Substitute.For <IQueen>();

            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);

            // Act
            sut.EvolveRestartFromBestTrail(queen);

            // Assert
            queen.Received().RestartFromTrail(Arg.Any <IEnumerable <int>>());
        }

        [Test]
        public void EvolveRestartFromBestTrailUpdatesTurnsRemaining_ReturnsDefault_WhenCalled()
        {
            // Arrange
            var queen = Substitute.For <IQueen>();

            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);

            // Act
            sut.EvolveRestartFromBestTrail(queen);

            // Assert
            Assert.AreEqual(100,
                            sut.TurnsRemaining);
        }

        [Test]
        public void EvolveRestartFromBestTrailUpdatesTurnsSinceNewBestTrailFound_ReturnsDefault_WhenCalled()
        {
            // Arrange
            var queen = Substitute.For <IQueen>();

            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);

            // Act
            sut.EvolveRestartFromBestTrail(queen);

            // Assert
            Assert.AreEqual(100 * 2,
                            sut.TurnsSinceNewBestTrailFound);
        }

        [Test]
        public void Find_CallsColonyLogger_WhenCalled()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(0);

            // Assert
            m_ColonyLogger.Received().LogResult(Arg.Any <TimeSpan>());
        }

        [Test]
        public void IsInvalidTrail_ReturnsTrue_ForInvalid()
        {
            // Arrange
            var builder = Substitute.For <IUnknownTrailBuilder>();
            builder.IsUnknown.Returns(false);
            builder.Trail.Returns(new[]
                                  {
                                      2,
                                      2
                                  });

            // Act
            Colony sut = CreateColonyWithCostMatrixAllSame();

            // Assert
            Assert.True(sut.IsInvalidTrail(builder));
        }

        [Test]
        public void IsInvalidTrail_ReturnsTrue_ForUnknown()
        {
            // Arrange
            // Act
            var builder = Substitute.For <IUnknownTrailBuilder>();
            builder.IsUnknown.Returns(true);

            Colony sut = CreateColonyWithCostMatrixAllSame();

            // Assert
            Assert.True(sut.IsInvalidTrail(builder));
        }

        [Test]
        public void NewBestTrailIsInvalid_LogsError_ForInvalidTrail()
        {
            // Arrange
            var builder = Substitute.For <IUnknownTrailBuilder>();
            builder.IsUnknown.Returns(true);

            Colony sut = CreateColonyWithCostMatrixAllSame();

            m_ColonyLogger.ClearReceivedCalls();

            // Act
            sut.NewBestTrailIsInvalid(builder);

            // Assert
            m_ColonyLogger.Received().Error(Arg.Is <string>(x => x.StartsWith("Found invalid trail")));
        }

        [Test]
        public void Pheromones_ReturnsDefault_WhenCalled()
        {
            // Arrange

            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            // Act
            Colony sut = CreateColony(tracker,
                                      graph);

            // Assert
            NUnitHelper.AssertIsEquivalent(0.02,
                                           sut.PheromonesMaximum);
        }

        [Test]
        public void PheromonesInformation_ReturnsInformation_WhenCalled()
        {
            // Arrange
            // Act
            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);

            // Assert
            Assert.NotNull(sut.PheromonesInformation());
        }

        [Test]
        public void PheromonesMinimum_ReturnsDefault_WhenCalled()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);

            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            // Act
            Colony sut = CreateColony(tracker,
                                      graph);

            // Assert
            NUnitHelper.AssertIsEquivalent(0.02,
                                           sut.PheromonesMinimum);
        }

        [Test]
        public void PostCycle_SendsBestTrailMessage_WhenCalled()
        {
            // Arrange
            BestTrailChangedEventArgs eventArgs = null;

            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);

            sut.BestTrailChanged += (sender,
                                     args) => eventArgs = args;
            sut.CycleInitialize();

            // Act
            sut.PostCycle();

            // Assert
            Assert.NotNull(eventArgs);
        }

        [Test]
        public void PostCycle_SendsFinishedMessage_WhenCalled()
        {
            // Arrange
            FinishedEventArgs eventArgs = null;

            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);

            sut.Finished += (sender,
                             args) => eventArgs = args;

            sut.CycleInitialize();

            // Act
            sut.PostCycle();

            // Assert
            Assert.NotNull(eventArgs);
        }

        [Test]
        public void PostCycle_SendsStoppedMessage_WhenCalled()
        {
            // Arrange
            EventArgs eventArgs = null;

            Colony sut = CreateColonyWithCostMatrixAllSame();
            sut.Stopped += (sender,
                            args) => eventArgs = args;

            sut.CycleInitialize();
            sut.Stop();

            // Act
            sut.PostCycle();

            // Assert
            Assert.NotNull(eventArgs);
        }

        [Test]
        public void PreCycle_CallsCycleInitialize_WhenCalled()
        {
            // Arrange
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

            Colony sut = CreateColony(factory,
                                      m_Tracker,
                                      m_Graph);

            // Act
            sut.PreCycle();

            // Assert
            Assert.AreEqual(trailBuilder,
                            sut.ColonyBestTrailBuilder);
        }

        [Test]
        public void PreCycle_SetsColonyBestTrailBuilderToDefault_WhenCalled()
        {
            // Arrange
            var expected = new[]
                           {
                               0,
                               2
                           };

            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);

            // Act
            sut.PreCycle();

            // Assert
            ITrailBuilder actual = sut.TrailHistory.Information.First().TrailBuilder;

            Assert.True(actual is IFixedTrailBuilder,
                        "First builder should be IFixedTrailBuilder");

            Assert.True(expected.SequenceEqual(actual.Trail),
                        "Default trail is wrong!");
        }

        [Test]
        public void RunTime_ReturnsDateTime_WhenCalled()
        {
            // Arrange
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

            // Act
            Colony sut = CreateColony(tracker,
                                      graph);

            // Assert
            Assert.NotNull(sut.Runtime);
        }

        [Test]
        public void SendBestTrailMessage_RaisesEvent_WhenCalled()
        {
            // Arrange
            var trailBuilder = Substitute.For <ITrailBuilder>();
            BestTrailChangedEventArgs args = null;

            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);
            sut.BestTrailChanged += (sender,
                                     eventArgs) => args = eventArgs;

            // Act
            sut.SendBestTrailMessage(trailBuilder);

            // Assert
            Assert.NotNull(args);
        }

        [Test]
        public void Start_PopulatesTrailHistory_WhenCalled()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);
            sut.TurnsBeforeSelection = 10;

            // Act
            sut.Start(100);

            // Assert
            ITrailHistory actual = sut.TrailHistory;

            Assert.NotNull(actual);
            Assert.True(actual.Information.Any(),
                        "Information.Count()");
        }

        [Test]
        public void Start_PopulatesTrailHistoryWithDefaultTrailFirst_WhenCalled()
        {
            // Arrange
            var expected = new[]
                           {
                               0,
                               2
                           };

            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);
            sut.TurnsBeforeSelection = 10;

            // Act
            sut.Start(100);

            // Assert
            ITrailBuilder actual = sut.TrailHistory.Information.First().TrailBuilder;

            Assert.True(expected.SequenceEqual(actual.Trail),
                        "Expected: {0} Actual: {1}".Inject(string.Join(",",
                                                                       expected),
                                                           string.Join(",",
                                                                       actual)));
        }

        [Test]
        public void Start_PopulatesTrailHistoryWithOtherTrailSecond_WhenCalled()
        {
            // Arrange
            var expected = new[]
                           {
                               1,
                               3
                           };

            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);
            sut.TurnsBeforeSelection = 10;

            // Act
            sut.Start(100);

            // Assert
            int[] actual = sut.TrailHistory
                              .Information
                              .Skip(1)
                              .First()
                              .TrailBuilder
                              .Trail.ToArray();

            Assert.True(expected.SequenceEqual(actual),
                        "Expected: {0} Actual: {1}".Inject(string.Join(",",
                                                                       expected),
                                                           string.Join(",",
                                                                       actual)));
        }

        [Test]
        public void Start_SetsAlternatives_CostMatrixSimpleAlternatives()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixSimple);
            DistanceGraph graph = CreateDistanceGraph(new NearestNeighbours(),
                                                      m_CostMatrixSimple,
                                                      costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(100);

            // Assert
            Assert.True(sut.Alternatives.Any(),
                        "AlternativeTrails.Count()");
        }

        [Test]
        public void Start_SetsAlternatives_CostMatrixThreeLinesAlternativesTest()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixThreeLines);
            DistanceGraph graph = CreateDistanceGraph(m_CostMatrixThreeLines,
                                                      costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(10);

            // Assert
            Assert.True(sut.Alternatives.Any(),
                        "AlternativeTrails.Count");
        }

        [Test]
        public void Start_SetsAlternatives_ForCostMatricAllSameAlternatives()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(100);

            // Assert
            Assert.True(sut.Alternatives.Any(),
                        "AlternativeTrails.Count()");
        }

        [Test]
        public void Start_SetsAlternatives_ForCostMatricTwoPathsAlternatives()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatricTwoPaths);
            DistanceGraph graph = CreateDistanceGraph(new NearestNeighbours(),
                                                      m_CostMatricTwoPaths,
                                                      costPerLine);

            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(100);

            // Assert
            Assert.True(sut.Alternatives.Any(),
                        "AlternativeTrails.Count()");
        }

        [Test]
        public void Start_SetsBestTrailBuilder_CostMatricTwoPathsBestTrail()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatricTwoPaths);
            DistanceGraph graph = CreateDistanceGraph(new NearestNeighbours(),
                                                      m_CostMatricTwoPaths,
                                                      costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(100);

            // Assert
            ITrailBuilder actual = sut.BestTrailBuilder;

            NUnitHelper.AssertIsEquivalent(110.0,
                                           actual.Length,
                                           Colony.Epsilon,
                                           actual.ToString());
        }

        [Test]
        public void Start_SetsBestTrailBuilder_CostMatrixSimpleAlternatives()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixSimple);
            DistanceGraph graph = CreateDistanceGraph(new NearestNeighbours(),
                                                      m_CostMatrixSimple,
                                                      costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(100);

            // Assert
            Assert.True(sut.BestTrailBuilder.Length > 0.0,
                        "BestTrailBuilder.Length");
        }

        [Test]
        public void Start_SetsBestTrailBuilder_CostMatrixThreeLinesAlternativesTest()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixThreeLines);
            DistanceGraph graph = CreateDistanceGraph(m_CostMatrixThreeLines,
                                                      costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(10);

            // Assert
            double bestLength = sut.BestTrailBuilder.Length;

            Assert.True(bestLength > 0.0,
                        "BestTrailBuilder.Length");
        }

        [Test]
        public void Start_SetsBestTrailBuilder_CostMatrixThreeLinesBestTrailTest()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixThreeLines);
            DistanceGraph graph = CreateDistanceGraph(m_CostMatrixThreeLines,
                                                      costPerLine);

            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(100);

            // Assert
            Assert.False(sut.BestTrailBuilder.IsUnknown);
        }

        [Test]
        public void Start_SetsBestTrailBuilder_ForCostMatricAllSameAlternatives()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(100);

            // Assert
            Assert.True(sut.BestTrailBuilder.Length > 0.0,
                        "BestTrailBuilder.Length");
        }

        [Test]
        public void Start_SetsBestTrailBuilder_ForCostMatricAllSameBestTrail()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(100);

            // Assert
            ITrailBuilder actual = sut.BestTrailBuilder;

            NUnitHelper.AssertIsEquivalent(150.0,
                                           actual.Length,
                                           Colony.Epsilon,
                                           actual.ToString());
        }

        [Test]
        public void Start_SetsBestTrailBuilder_ForCostMatricTwoPathsAlternatives()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatricTwoPaths);
            DistanceGraph graph = CreateDistanceGraph(new NearestNeighbours(),
                                                      m_CostMatricTwoPaths,
                                                      costPerLine);

            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(100);

            // Assert
            Assert.True(sut.BestTrailBuilder.Length > 0.0,
                        "BestTrailBuilder.Length");
        }

        [Test]
        public void Start_SetsBestTrailBuilderTrail_SetsCostMatrixSimpleBestTrailTest()
        {
            // Arrange
            int[] expected =
            {
                0,
                3
            };
            int[] costPerLine = CreateCostPerLine(m_CostMatrixSimple);
            DistanceGraph graph = CreateDistanceGraph(new NearestNeighbours(),
                                                      m_CostMatrixSimple,
                                                      costPerLine);

            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(100);

            // Assert
            IEnumerable <int> actual = sut.BestTrailBuilder.Trail;

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void Start_SetsColonyBestTrailBuilderToDefault_WhenCalled()
        {
            // Arrange
            var expected = new[]
                           {
                               0,
                               2
                           };

            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);

            // Act
            sut.Start(2);

            // Assert
            ITrailBuilder actual = sut.TrailHistory.Information.First().TrailBuilder;

            Assert.True(actual is IFixedTrailBuilder,
                        "First builder should be IFixedTrailBuilder");

            Assert.True(expected.SequenceEqual(actual.Trail),
                        "Default trail is wrong!");
        }

        [Test]
        public void Start_SetsPheromones_WhenCalled()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(100);

            // Assert
            double[][] actual = sut.PheromonesToArray();

            Assert.AreEqual(4,
                            actual.GetLength(0),
                            ".GetLength(0)");
            Assert.AreEqual(4,
                            actual [ 0 ].Length,
                            "Length");
        }

        [Test]
        public void Start_SetsStartTime_WhenCalled()
        {
            // Arrange
            Colony sut = CreateColonyWithCostMatrixAllSame();

            // Act
            sut.Start(10);

            // Assert
            Assert.AreEqual(10,
                            sut.Time);
        }

        [Test]
        public void Start_UpdatesPheromonesAverage_WhenCalled()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);

            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            Colony sut = CreateColony(tracker,
                                      graph);

            // Act
            sut.Start(10);

            // Assert
            Assert.True(sut.PheromonesMinimum < sut.PheromonesAverage);
        }

        [Test]
        public void Stop_SetsIsRequestedToStopToTrue_WhenCalled()
        {
            // Arrange
            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);

            // Act
            sut.Stop();

            // Assert
            Assert.True(sut.IsRequestedToStop);
        }

        [Test]
        public void Time_ReturnsDefault_WhenCalled()
        {
            // Arrange
            // Act
            Colony sut = CreateColonyWithCostMatrixAllSame();

            // Assert
            Assert.AreEqual(0,
                            sut.Time);
        }

        [Test]
        public void TurnsBeforeSelection_ReturnsDefault_WhenCalled()
        {
            // Arrang
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            // Act
            Colony sut = CreateColony(tracker,
                                      graph);

            // Assert
            Assert.AreEqual(Colony.DefaultTurnsBeforeSelection,
                            sut.TurnsBeforeSelection);
        }

        [Test]
        public void TurnsBeforeSelection_RoundTrip()
        {
            // Arrange
            int[] costPerLine = CreateCostPerLine(m_CostMatrixAllSame);
            DistanceGraph graph = CreateDistanceGraph(costPerLine);
            PheromonesTracker tracker = CreatePheromonesTracker(graph);

            // Act
            Colony sut = CreateColony(tracker,
                                      graph);

            // Assert
            Assert.AreEqual(100,
                            sut.TurnsBeforeSelection);
        }

        [Test]
        public void TurnsRemaining_ReturnsDefault_WhenCalled()
        {
            // Arrange
            // Act
            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);

            // Assert
            Assert.AreEqual(2,
                            sut.TurnsRemaining);
        }

        [Test]
        public void TurnsSinceNewBestTrailFound_ReturnsDefault_WhenCalled()
        {
            // Arrange
            // Act
            Colony sut = CreateColony(m_Tracker,
                                      m_Graph);

            // Assert
            Assert.AreEqual(200,
                            sut.TurnsSinceNewBestTrailFound);
        }
    }
}