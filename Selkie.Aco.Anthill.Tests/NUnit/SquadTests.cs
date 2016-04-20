using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Ants;
using Selkie.Aco.Ants.Interfaces;
using Selkie.Aco.Common;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Trails.Optimizers;
using Selkie.Common;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Aco.Anthill.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    // ReSharper disable once ClassTooBig
    internal sealed class SquadTests
    {
        [SetUp]
        public void Setup()
        {
            m_Disposer = Substitute.For <IDisposer>();
            m_Logger = Substitute.For <ISelkieLogger>();
            m_Random = Substitute.For <IRandom>();
            m_AntFactory = new TestAntFactory();

            m_CostMatrix = CreateCostMatrix();

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

            m_AntSettings = Substitute.For <IAntSettings>();

            m_AntOne = m_AntFactory.Create <IStandardAnt>(new Chromosome(m_Random),
                                                          m_Tracker,
                                                          m_Graph,
                                                          m_Optimizer,
                                                          m_AntSettings);

            m_AntTwo = m_AntFactory.Create <IStandardAnt>(new Chromosome(m_Random),
                                                          m_Tracker,
                                                          m_Graph,
                                                          m_Optimizer,
                                                          m_AntSettings);

            m_Optimizer = new TwoOptSimple
                          {
                              DistanceGraph = m_Graph
                          };

            m_Squad = new Squad(m_Disposer,
                                m_Logger,
                                m_Random,
                                m_AntFactory,
                                m_Graph,
                                m_Tracker,
                                m_Optimizer,
                                m_AntSettings);
        }

        [TearDown]
        public void Teardown()
        {
            m_Squad.Dispose();
            m_AntFactory.Release(m_AntOne);
            m_AntFactory.Release(m_AntTwo);
        }

        private TestAntFactory m_AntFactory;
        private IStandardAnt m_AntOne;
        private IStandardAnt m_AntTwo;
        private int[][] m_CostMatrix;
        private IDisposer m_Disposer;
        private IDistanceGraph m_Graph;
        private ISelkieLogger m_Logger;
        private IOptimizer m_Optimizer;
        private IPheromones m_Pheromones;
        private IRandom m_Random;
        private Squad m_Squad;
        private PheromonesTracker m_Tracker;
        private IAntSettings m_AntSettings;

        [NotNull]
        private int[][] CreateCostMatrix()
        {
            var matrix = new int[4][];

            matrix [ 0 ] = new[]
                           {
                               1,
                               2,
                               3,
                               4
                           };
            matrix [ 1 ] = new[]
                           {
                               5,
                               6,
                               7,
                               8
                           };
            matrix [ 2 ] = new[]
                           {
                               9,
                               10,
                               11,
                               12
                           };
            matrix [ 3 ] = new[]
                           {
                               13,
                               14,
                               15,
                               16
                           };

            return matrix;
        }

        [NotNull]
        private Squad CreateSquad()
        {
            var squad = new Squad(m_Disposer,
                                  m_Logger,
                                  m_Random,
                                  m_AntFactory,
                                  m_Graph,
                                  m_Tracker,
                                  m_Optimizer,
                                  m_AntSettings);
            return squad;
        }

        [Test]
        public void AddBestAntTest()
        {
            m_Squad.AddBestAnt(m_AntOne);

            Assert.AreEqual(1,
                            m_Squad.BestAnts.Count(),
                            "Count");
            Assert.True(m_Squad.BestAnts.Contains(m_AntOne),
                        "Contains");
        }

        [Test]
        public void AntsCountDefaultTest()
        {
            Squad squad = CreateSquad();

            IEnumerable <IAnt> actual = squad.Ants;

            Assert.AreEqual(Squad.DefaultNumberOfAnts,
                            actual.Count());

            squad.Dispose();
        }

        [Test]
        public void AntsDefaultTest()
        {
            Squad squad = CreateSquad();

            foreach ( IAnt ant in squad.Ants )
            {
                Assert.NotNull(ant);
            }

            squad.Dispose();
        }

        [Test]
        public void BestAntsDefaultTest()
        {
            Squad squad = CreateSquad();

            Assert.AreEqual(0,
                            squad.BestAnts.Count());

            squad.Dispose();
        }

        [Test]
        public void ClearClearsBestAntsTest()
        {
            m_Squad.AddBestAnt(m_AntOne);
            m_Squad.AddBestAnt(m_AntTwo);

            m_Squad.Clear();

            Assert.AreEqual(0,
                            m_Squad.BestAnts.Count());
        }

        [Test]
        public void ClearCreatesAntsTest()
        {
            m_Squad.Clear();

            Assert.AreEqual(10,
                            m_Squad.Ants.Count(),
                            "Count");
        }

        [Test]
        public void ClearReleasesAntsTest()
        {
            List <IAnt> ants = m_Squad.Ants.ToList();

            m_Squad.Clear();

            foreach ( IAnt ant in ants )
            {
                Assert.True(m_AntFactory.Released(ant),
                            "Ant: {0}".Inject(ant));
            }
        }

        [Test]
        public void ClearReleasesBestAntsTest()
        {
            m_Squad.AddBestAnt(m_AntOne);
            m_Squad.AddBestAnt(m_AntTwo);

            m_Squad.Clear();

            Assert.True(m_AntFactory.Released(m_AntOne),
                        "one");
            Assert.True(m_AntFactory.Released(m_AntTwo),
                        "two");
        }

        [Test]
        public void ConstructorAddsToDisposerTest()
        {
            m_Disposer.Received().AddResource(Arg.Any <Action>());
        }

        [Test]
        public void CreateAntsClearsBestAntsTest()
        {
            m_Squad.CreateAnts(2);

            Assert.AreEqual(0,
                            m_Squad.BestAnts.Count(),
                            "BestAnts.Count");
        }

        [Test]
        public void CreateAntsCreatesNewAntsTest()
        {
            m_Squad.CreateAnts(2);

            Assert.AreEqual(2,
                            m_Squad.NumberOfAnts,
                            "NumberOfAnts");
            Assert.AreEqual(2,
                            m_Squad.Ants.Count(),
                            "Ants.Count");
            Assert.AreEqual(0,
                            m_Squad.BestAnts.Count(),
                            "BestAnts.Count");
        }

        [Test]
        public void CreateAntsReleasesBestAntsTest()
        {
            m_Squad.AddBestAnt(m_AntOne);
            m_Squad.AddBestAnt(m_AntTwo);

            m_Squad.SetNumberOfAnts(2);

            Assert.True(m_AntFactory.Released(m_AntOne),
                        "one");
            Assert.True(m_AntFactory.Released(m_AntTwo),
                        "two");
        }

        [Test]
        public void NumberOfAntsDefaultTest()
        {
            Squad squad = CreateSquad();

            Assert.AreEqual(Squad.DefaultNumberOfAnts,
                            squad.NumberOfAnts);

            squad.Dispose();
        }

        [Test]
        public void ReleaseAllAntsClearsBestAntsTest()
        {
            m_Squad.AddBestAnt(m_AntOne);
            m_Squad.AddBestAnt(m_AntTwo);

            m_Squad.ReleaseAllAnts();

            Assert.AreEqual(0,
                            m_Squad.BestAnts.Count());
        }

        [Test]
        public void ReleaseAllAntsReleasesAntsTest()
        {
            List <IAnt> ants = m_Squad.Ants.ToList();

            m_Squad.ReleaseAllAnts();

            foreach ( IAnt ant in ants )
            {
                Assert.True(m_AntFactory.Released(ant),
                            "Ant: {0}".Inject(ant));
            }
        }

        [Test]
        public void ReleaseAllAntsReleasesBestAntsTest()
        {
            m_Squad.AddBestAnt(m_AntOne);
            m_Squad.AddBestAnt(m_AntTwo);

            m_Squad.ReleaseAllAnts();

            Assert.True(m_AntFactory.Released(m_AntOne),
                        "one");
            Assert.True(m_AntFactory.Released(m_AntTwo),
                        "two");
        }

        [Test]
        public void ReleaseAntsTest()
        {
            IStandardAnt[] ants =
            {
                m_AntOne,
                m_AntTwo
            };

            m_Squad.ReleaseAnts(ants);

            Assert.True(m_AntFactory.Released(m_AntOne),
                        "one");
            Assert.True(m_AntFactory.Released(m_AntTwo),
                        "two");
        }

        [Test]
        public void RestartCreatesAntsContainingBestAntsTest()
        {
            m_Squad.AddBestAnt(m_AntOne);
            m_Squad.AddBestAnt(m_AntTwo);

            m_Squad.Restart();

            Assert.AreEqual(12,
                            m_Squad.Ants.Count(),
                            "Count");
        }

        [Test]
        public void RestartDoesNotClearBestAntsTest()
        {
            m_Squad.AddBestAnt(m_AntOne);
            m_Squad.AddBestAnt(m_AntTwo);

            m_Squad.Restart();

            Assert.False(m_AntFactory.Released(m_AntOne),
                         "one");
            Assert.False(m_AntFactory.Released(m_AntTwo),
                         "two");
        }

        [Test]
        public void RestartReleasesAntsTest()
        {
            List <IAnt> ants = m_Squad.Ants.ToList();

            m_Squad.Restart();

            foreach ( IAnt ant in ants )
            {
                Assert.True(m_AntFactory.Released(ant),
                            "Ant: {0}".Inject(ant));
            }
        }

        [Test]
        public void SetNumberOfAntsClearsBestAntsTest()
        {
            m_Squad.SetNumberOfAnts(2);

            Assert.AreEqual(0,
                            m_Squad.BestAnts.Count(),
                            "BestAnts.Count");
        }

        [Test]
        public void SetNumberOfAntsCreatesNewAntsTest()
        {
            m_Squad.SetNumberOfAnts(2);

            Assert.AreEqual(2,
                            m_Squad.NumberOfAnts,
                            "NumberOfAnts");
            Assert.AreEqual(2,
                            m_Squad.Ants.Count(),
                            "Ants.Count");
            Assert.AreEqual(0,
                            m_Squad.BestAnts.Count(),
                            "BestAnts.Count");
        }

        [Test]
        public void SetNumberOfAntsDoesNothingForLessThanOneTest()
        {
            m_Squad.SetNumberOfAnts(0);

            Assert.AreEqual(Squad.DefaultNumberOfAnts,
                            m_Squad.NumberOfAnts);
        }

        [Test]
        public void SetNumberOfAntsReleasesAntsTest()
        {
            List <IAnt> ants = m_Squad.Ants.ToList();

            m_Squad.SetNumberOfAnts(2);

            foreach ( IAnt ant in ants )
            {
                Assert.True(m_AntFactory.Released(ant),
                            "Ant: {0}".Inject(ant));
            }
        }

        [Test]
        public void SetNumberOfAntsReleasesBestAntsTest()
        {
            m_Squad.AddBestAnt(m_AntOne);
            m_Squad.AddBestAnt(m_AntTwo);

            m_Squad.SetNumberOfAnts(2);

            Assert.True(m_AntFactory.Released(m_AntOne),
                        "one");
            Assert.True(m_AntFactory.Released(m_AntTwo),
                        "two");
        }

        [Test]
        public void ToStringTest()
        {
            Squad squad = CreateSquad();
            squad.SetNumberOfAnts(2);

            const string expected = "Number of Ants: 2";
            string actual = squad.ToString();

            Assert.True(actual.StartsWith(expected));
        }
    }
}