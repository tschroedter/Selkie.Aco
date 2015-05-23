using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Common;
using Selkie.NUnit.Extensions;

namespace Selkie.Aco.Common.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class PheromonesTrackerTests
    {
        [SetUp]
        // ReSharper disable once MethodTooLong
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();
            m_Random.NextDouble().Returns(0.1,
                                          0.2,
                                          0.3,
                                          0.4,
                                          0.5);

            m_TrailBuilderOne = Substitute.For <ITrailBuilder>();
            m_TrailBuilderOne.Trail.Returns(new[]
                                            {
                                                0,
                                                1,
                                                2
                                            });
            m_TrailBuilderOne.EdgeInTrail(0,
                                          0).Returns(true);

            m_AntOne = Substitute.For <IAnt>();
            m_AntOne.Id.Returns(0);
            m_AntOne.TrailBuilder.Returns(m_TrailBuilderOne);

            m_TrailBuilderTwo = Substitute.For <ITrailBuilder>();
            m_TrailBuilderTwo.Trail.Returns(new[]
                                            {
                                                1,
                                                2,
                                                0
                                            });
            m_TrailBuilderTwo.EdgeInTrail(0,
                                          0).Returns(true);

            m_AntTwo = Substitute.For <IAnt>();
            m_AntTwo.Id.Returns(1);
            m_AntTwo.TrailBuilder.Returns(m_TrailBuilderTwo);

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

            m_Graph = new DistanceGraph(m_Random,
                                        new NearestNeighbours(),
                                        costMatrix,
                                        costPerLine);
            m_Pheromones = new Pheromones();
            m_Tracker = new PheromonesTracker(m_Random,
                                              m_Graph,
                                              m_Pheromones,
                                              0.01,
                                              2.0);
        }

        private IDistanceGraph m_Graph;
        private PheromonesTracker m_Tracker;
        private ITrailBuilder m_TrailBuilderOne;
        private IAnt m_AntOne;
        private ITrailBuilder m_TrailBuilderTwo;
        private IAnt m_AntTwo;
        private IPheromones m_Pheromones;
        private IRandom m_Random;

        [Test]
        public void AverageValueTest()
        {
            NUnitHelper.AssertIsEquivalent(0.84999999999999998d,
                                           m_Tracker.AverageValue,
                                           0.001);
        }

        [Test]
        public void ClearTest()
        {
            m_Pheromones.SetValue(0,
                                  0,
                                  100.0);

            m_Tracker.Clear();

            for ( var i = 0 ; i < m_Tracker.NumberOfNodes ; i++ )
            {
                for ( var j = 0 ; j < m_Tracker.NumberOfNodes ; j++ )
                {
                    Assert.AreEqual(m_Tracker.InitialValue,
                                    m_Tracker.GetValue(i,
                                                       j),
                                    "pheromone[" + i + "][" + j + "]");
                }
            }
        }

        [Test]
        public void ConstructorWithThreeParamaetersTest()
        {
            var actual = new PheromonesTracker(m_Random,
                                               m_Graph,
                                               m_Pheromones,
                                               111.0,
                                               222.0);

            Assert.AreEqual(111.0,
                            actual.Rho,
                            "Rho");
            Assert.AreEqual(222.0,
                            actual.Q,
                            "Q");

            NUnitHelper.AssertIsEquivalent(6.9375,
                                           actual.MinimumValue,
                                           1E-10,
                                           "MinimumValue");
            NUnitHelper.AssertIsEquivalent(111.0,
                                           actual.MaximumValue,
                                           1E-10,
                                           "MaximumValue");
        }

        [Test]
        public void MaximumValueTest()
        {
            NUnitHelper.AssertIsEquivalent(1.0,
                                           m_Tracker.MaximumValue,
                                           1E-10,
                                           "MaximumValue");
        }

        [Test]
        public void MinimumValueTest()
        {
            NUnitHelper.AssertIsEquivalent(0.0625,
                                           m_Tracker.MinimumValue,
                                           1E-10,
                                           "MinimumValue");
        }

        [Test]
        public void PheromonesContentTest()
        {
            double[][] actual = m_Pheromones.ToArray();

            for ( var i = 0 ; i < actual.Length ; i++ )
            {
                for ( var j = 0 ; j < actual [ i ].Length ; j++ )
                {
                    Assert.AreEqual(m_Tracker.InitialValue,
                                    actual [ i ] [ j ],
                                    "[" + i + "][" + j + "]");
                }
            }
        }

        [Test]
        public void PheromonesLengthTest()
        {
            double[][] actual = m_Pheromones.ToArray();

            Assert.AreEqual(4,
                            actual.GetLength(0),
                            "GetLength(0)");
            Assert.AreEqual(4,
                            actual [ 0 ].Length,
                            "actual[0].Length");
            Assert.AreEqual(4,
                            actual [ 1 ].Length,
                            "actual[1].Length");
            Assert.AreEqual(4,
                            actual [ 2 ].Length,
                            "actual[2].Length");
            Assert.AreEqual(4,
                            actual [ 3 ].Length,
                            "actual[3].Length");
        }

        [Test]
        public void RandomizeTest()
        {
            var random = Substitute.For <IRandom>();
            random.NextDouble().Returns(0.1,
                                        0.2,
                                        0.3);

            var tracker = new PheromonesTracker(random,
                                                m_Graph,
                                                m_Pheromones,
                                                0.01,
                                                2.0);

            tracker.Randomize();

            Assert.True(
                        tracker.Rho >= PheromonesTracker.RhoMinimumValue &&
                        tracker.Rho <= PheromonesTracker.RhoMaximumValue,
                        "Rho");

            Assert.True(tracker.Q >= PheromonesTracker.QMinimumValue && tracker.Q <= PheromonesTracker.QMaximumValue,
                        "Q");
        }

        [Test]
        public void UpdateAllEdgeInTrailIsFalseForPheromonesEvaporateTest()
        {
            IAnt[] ants =
            {
                m_AntOne,
                m_AntTwo
            };

            for ( var i = 0 ; i < 100 ; i++ )
            {
                m_Tracker.Update(ants);
            }

            const double expectedValue = 0.11388272362926746d;

            NUnitHelper.AssertIsEquivalent(m_Tracker.InitialValue,
                                           m_Tracker.GetValue(0,
                                                              0),
                                           0.0001,
                                           "[0][0]");
            NUnitHelper.AssertIsEquivalent(expectedValue,
                                           m_Tracker.GetValue(0,
                                                              1),
                                           0.0001,
                                           "[0][1]");
            NUnitHelper.AssertIsEquivalent(expectedValue,
                                           m_Tracker.GetValue(0,
                                                              2),
                                           0.0001,
                                           "[0][2]");
            NUnitHelper.AssertIsEquivalent(expectedValue,
                                           m_Tracker.GetValue(1,
                                                              0),
                                           0.0001,
                                           "[1][0]");
            NUnitHelper.AssertIsEquivalent(m_Tracker.InitialValue,
                                           m_Tracker.GetValue(1,
                                                              1),
                                           0.0001,
                                           "[1][1]");
            NUnitHelper.AssertIsEquivalent(expectedValue,
                                           m_Tracker.GetValue(1,
                                                              2),
                                           0.0001,
                                           "[1][2]");
            NUnitHelper.AssertIsEquivalent(expectedValue,
                                           m_Tracker.GetValue(2,
                                                              0),
                                           0.0001,
                                           "[2][0]");
            NUnitHelper.AssertIsEquivalent(expectedValue,
                                           m_Tracker.GetValue(2,
                                                              1),
                                           0.0001,
                                           "[2][1]");
            NUnitHelper.AssertIsEquivalent(m_Tracker.InitialValue,
                                           m_Tracker.GetValue(2,
                                                              2),
                                           0.0001,
                                           "[2][2]");
        }

        [Test]
        public void UpdateAllEdgeInTrailIsFalseTest()
        {
            IAnt[] ants =
            {
                m_AntOne,
                m_AntTwo
            };

            m_Tracker.Update(ants);

            const double expectedValue = 0.83308499999999996d;

            NUnitHelper.AssertIsEquivalent(m_Tracker.InitialValue,
                                           m_Tracker.GetValue(0,
                                                              0),
                                           0.0001,
                                           "[0][0]");
            NUnitHelper.AssertIsEquivalent(expectedValue,
                                           m_Tracker.GetValue(0,
                                                              1),
                                           0.0001,
                                           "[0][1]");
            NUnitHelper.AssertIsEquivalent(expectedValue,
                                           m_Tracker.GetValue(0,
                                                              2),
                                           0.0001,
                                           "[0][2]");
            NUnitHelper.AssertIsEquivalent(expectedValue,
                                           m_Tracker.GetValue(1,
                                                              0),
                                           0.0001,
                                           "[1][0]");
            NUnitHelper.AssertIsEquivalent(m_Tracker.InitialValue,
                                           m_Tracker.GetValue(1,
                                                              1),
                                           0.0001,
                                           "[1][1]");
            NUnitHelper.AssertIsEquivalent(expectedValue,
                                           m_Tracker.GetValue(1,
                                                              2),
                                           0.0001,
                                           "[1][2]");
            NUnitHelper.AssertIsEquivalent(expectedValue,
                                           m_Tracker.GetValue(2,
                                                              0),
                                           0.0001,
                                           "[2][0]");
            NUnitHelper.AssertIsEquivalent(expectedValue,
                                           m_Tracker.GetValue(2,
                                                              1),
                                           0.0001,
                                           "[2][1]");
            NUnitHelper.AssertIsEquivalent(m_Tracker.InitialValue,
                                           m_Tracker.GetValue(2,
                                                              2),
                                           0.0001,
                                           "[2][2]");
        }

        [Test]
        // ReSharper disable once MethodTooLong
        public void UpdateAllEdgeInTrailIsTrueForPheromonesDoNotEvaporateTest()
        {
            m_TrailBuilderOne.EdgeInTrail(0,
                                          1).Returns(true);
            m_TrailBuilderOne.EdgeInTrail(1,
                                          2).Returns(true);
            m_TrailBuilderOne.EdgeInTrail(2,
                                          0).Returns(true);

            IAnt[] ants =
            {
                m_AntOne,
                m_AntTwo
            };

            m_Tracker.Update(ants);

            const double expectedValue1 = 0.98999999999999999d;
            const double expectedValue2 = 0.83308499999999996d;

            NUnitHelper.AssertIsEquivalent(m_Tracker.InitialValue,
                                           m_Tracker.GetValue(0,
                                                              0),
                                           0.0001,
                                           "[0][0]");
            NUnitHelper.AssertIsEquivalent(expectedValue1,
                                           m_Tracker.GetValue(0,
                                                              1),
                                           0.0001,
                                           "[0][1]");
            NUnitHelper.AssertIsEquivalent(expectedValue2,
                                           m_Tracker.GetValue(0,
                                                              2),
                                           0.0001,
                                           "[0][2]");
            NUnitHelper.AssertIsEquivalent(expectedValue1,
                                           m_Tracker.GetValue(1,
                                                              0),
                                           0.0001,
                                           "[1][0]");
            NUnitHelper.AssertIsEquivalent(m_Tracker.InitialValue,
                                           m_Tracker.GetValue(1,
                                                              1),
                                           0.0001,
                                           "[1][1]");
            NUnitHelper.AssertIsEquivalent(expectedValue1,
                                           m_Tracker.GetValue(1,
                                                              2),
                                           0.0001,
                                           "[1][2]");
            NUnitHelper.AssertIsEquivalent(expectedValue2,
                                           m_Tracker.GetValue(2,
                                                              0),
                                           0.0001,
                                           "[2][0]");
            NUnitHelper.AssertIsEquivalent(expectedValue1,
                                           m_Tracker.GetValue(2,
                                                              1),
                                           0.0001,
                                           "[2][1]");
            NUnitHelper.AssertIsEquivalent(m_Tracker.InitialValue,
                                           m_Tracker.GetValue(2,
                                                              2),
                                           0.0001,
                                           "[2][2]");
        }

        [Test]
        // ReSharper disable once MethodTooLong
        public void UpdateAllEdgeInTrailIsTrueTest()
        {
            m_TrailBuilderOne.EdgeInTrail(0,
                                          1).Returns(true);
            m_TrailBuilderOne.EdgeInTrail(1,
                                          2).Returns(true);
            m_TrailBuilderOne.EdgeInTrail(2,
                                          0).Returns(true);

            IAnt[] ants =
            {
                m_AntOne,
                m_AntTwo
            };

            m_Tracker.Update(ants);

            const double expectedValue1 = 0.98999999999999999d;
            const double expectedValue2 = 0.83308499999999996d;

            NUnitHelper.AssertIsEquivalent(m_Tracker.InitialValue,
                                           m_Tracker.GetValue(0,
                                                              0),
                                           0.0001,
                                           "[0][0]");
            NUnitHelper.AssertIsEquivalent(expectedValue1,
                                           m_Tracker.GetValue(0,
                                                              1),
                                           0.0001,
                                           "[0][1]");
            NUnitHelper.AssertIsEquivalent(expectedValue2,
                                           m_Tracker.GetValue(0,
                                                              2),
                                           0.0001,
                                           "[0][2]");
            NUnitHelper.AssertIsEquivalent(expectedValue1,
                                           m_Tracker.GetValue(1,
                                                              0),
                                           0.0001,
                                           "[1][0]");
            NUnitHelper.AssertIsEquivalent(m_Tracker.InitialValue,
                                           m_Tracker.GetValue(1,
                                                              1),
                                           0.0001,
                                           "[1][1]");
            NUnitHelper.AssertIsEquivalent(expectedValue1,
                                           m_Tracker.GetValue(1,
                                                              2),
                                           0.0001,
                                           "[1][2]");
            NUnitHelper.AssertIsEquivalent(expectedValue2,
                                           m_Tracker.GetValue(2,
                                                              0),
                                           0.0001,
                                           "[2][0]");
            NUnitHelper.AssertIsEquivalent(expectedValue1,
                                           m_Tracker.GetValue(2,
                                                              1),
                                           0.0001,
                                           "[2][1]");
            NUnitHelper.AssertIsEquivalent(m_Tracker.InitialValue,
                                           m_Tracker.GetValue(2,
                                                              2),
                                           0.0001,
                                           "[2][2]");
        }

        [Test]
        public void UpdateAntsUpdatesAverageTest()
        {
            IAnt[] ants =
            {
                Substitute.For <IAnt>()
            };

            m_Pheromones.SetValue(0,
                                  0,
                                  100.0);
            m_Tracker.Update(ants);

            const double expected = 3.9420624999999996d;
            double actual = m_Tracker.AverageValue;

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual,
                                           0.001);
        }

        [Test]
        public void UpdateAntUpdatesAverageTest()
        {
            m_Pheromones.SetValue(0,
                                  0,
                                  100.0);
            m_Tracker.Update(Substitute.For <IAnt>());

            const double expected = 3.9420624999999996d;
            double actual = m_Tracker.AverageValue;

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual,
                                           0.001);
        }
    }
}