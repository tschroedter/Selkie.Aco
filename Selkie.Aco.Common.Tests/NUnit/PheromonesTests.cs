using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.NUnit.Extensions;

namespace Selkie.Aco.Common.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class PheromonesTests
    {
        [SetUp]
        public void Setup()
        {
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

            m_Pheromones = new Pheromones();

            var information = new InitializeInformation
                              {
                                  NumberOfNodes = NumberOfNodes,
                                  Rho = Rho,
                                  Q = Q,
                                  MinimumValue = MinimumValue,
                                  MaximumValue = MaxmumValue,
                                  InitialValue = InitialValue
                              };

            m_Pheromones.Initialize(information);
        }

        private const int NumberOfNodes = 2;
        private const double InitialValue = 5.0;
        private const double Q = 2.0; // pheromone increase factor
        private const double Rho = 0.005; // pheromone decrease factor
        private const double MinimumValue = 1.0;
        private const double MaxmumValue = 10.0;
        private IAnt m_AntOne;
        private Pheromones m_Pheromones;
        private ITrailBuilder m_TrailBuilderOne;

        [Test]
        public void CaclulateNewValueForZeroZeroAndEdgeInTrailIsFalseTest()
        {
            m_TrailBuilderOne.EdgeInTrail(0,
                                          0).Returns(false);

            const double expected = 4.9749999999999996d;
            double actual = m_Pheromones.CaclulateNewValue(m_AntOne,
                                                           0,
                                                           0);

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual,
                                           0.0001);
        }

        [Test]
        public void CaclulateNewValueForZeroZeroAndEdgeInTrailIsTrueTest()
        {
            m_TrailBuilderOne.EdgeInTrail(0,
                                          0).Returns(true);
            m_TrailBuilderOne.Length.Returns(10.0);

            const double expected = 5.1749999999999998d;
            double actual = m_Pheromones.CaclulateNewValue(m_AntOne,
                                                           0,
                                                           0);

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual,
                                           0.0001);
        }

        [Test]
        public void CalculateAverageValueTest()
        {
            m_Pheromones.SetValue(0,
                                  0,
                                  0.0);
            m_Pheromones.SetValue(0,
                                  1,
                                  1.0);
            m_Pheromones.SetValue(1,
                                  0,
                                  2.0);
            m_Pheromones.SetValue(1,
                                  1,
                                  3.0);

            double actual = m_Pheromones.CalculateAverageValue();

            NUnitHelper.AssertIsEquivalent(1.5,
                                           actual,
                                           0.001);
        }

        [Test]
        public void CalculateNewTrimValueForZeroZeroAndEdgeInTrailIsFalseTest()
        {
            m_TrailBuilderOne.EdgeInTrail(0,
                                          0).Returns(false);
            m_TrailBuilderOne.Length.Returns(10.0);

            const double expected = 4.9749999999999996d;
            double actual = m_Pheromones.CalculateNewTrimValue(m_AntOne,
                                                               0,
                                                               0);

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual,
                                           0.0001);
        }

        [Test]
        public void CalculateNewTrimValueForZeroZeroAndEdgeInTrailIsTrueTest()
        {
            m_TrailBuilderOne.EdgeInTrail(0,
                                          0).Returns(true);
            m_TrailBuilderOne.Length.Returns(10.0);

            const double expected = 5.1749999999999998d;
            double actual = m_Pheromones.CalculateNewTrimValue(m_AntOne,
                                                               0,
                                                               0);

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual,
                                           0.0001);
        }

        [Test]
        public void DefaultValuesTest()
        {
            for ( var from = 0 ; from < NumberOfNodes ; from++ )
            {
                for ( var to = 0 ; to < NumberOfNodes ; to++ )
                {
                    NUnitHelper.AssertIsEquivalent(InitialValue,
                                                   m_Pheromones.GetValue(from,
                                                                         to),
                                                   "[{0}][{1}]".Inject(from,
                                                                       to));
                }
            }
        }

        [Test]
        public void InitialValueTest()
        {
            Assert.AreEqual(InitialValue,
                            m_Pheromones.InitialValue);
        }

        [Test]
        public void NumberOfNodesTest()
        {
            Assert.AreEqual(NumberOfNodes,
                            m_Pheromones.NumberOfNodes);
        }

        [Test]
        public void ToArrayTest()
        {
            m_Pheromones.SetValue(0,
                                  0,
                                  0.0);
            m_Pheromones.SetValue(0,
                                  1,
                                  1.0);
            m_Pheromones.SetValue(1,
                                  0,
                                  2.0);
            m_Pheromones.SetValue(1,
                                  1,
                                  3.0);

            double[][] actual = m_Pheromones.ToArray();

            Assert.AreEqual(NumberOfNodes,
                            actual.GetLength(0),
                            "Length 0");

            NUnitHelper.AssertIsEquivalent(0.0,
                                           actual [ 0 ] [ 0 ],
                                           "[0][0]");
            NUnitHelper.AssertIsEquivalent(1.0,
                                           actual [ 0 ] [ 1 ],
                                           "[0][1]");
            NUnitHelper.AssertIsEquivalent(2.0,
                                           actual [ 1 ] [ 0 ],
                                           "[1][0]");
            NUnitHelper.AssertIsEquivalent(3.0,
                                           actual [ 1 ] [ 1 ],
                                           "[1][1]");
        }

        [Test]
        public void TrimPheromoneValueForAboveMaximumTest()
        {
            double value = m_Pheromones.MaximumValue + 1.0;

            double actual = m_Pheromones.TrimPheromoneValue(value);

            Assert.AreEqual(m_Pheromones.MaximumValue,
                            actual);
        }

        [Test]
        public void TrimPheromoneValueForBelowMinimumTest()
        {
            double value = m_Pheromones.MinimumValue - 1.0;

            double actual = m_Pheromones.TrimPheromoneValue(value);

            Assert.AreEqual(m_Pheromones.MinimumValue,
                            actual);
        }

        [Test]
        public void TrimPheromoneValueForBetweenMinimumMaximumTest()
        {
            double value = ( m_Pheromones.MinimumValue + m_Pheromones.MaximumValue ) / 2.0;

            double actual = m_Pheromones.TrimPheromoneValue(value);

            Assert.AreEqual(value,
                            actual);
        }

        [Test]
        public void UpdateForAntUpdatesPheromonesForIndexOneZeroTest()
        {
            m_TrailBuilderOne.EdgeInTrail(0,
                                          1).Returns(false);
            m_TrailBuilderOne.Length.Returns(10.0);

            m_Pheromones.UpdateForAnt(m_AntOne,
                                      0,
                                      1);

            const double expected = 4.9749999999999996d;
            double actual = m_Pheromones.GetValue(1,
                                                  0);

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual,
                                           0.0001);
        }

        [Test]
        public void UpdateForAntUpdatesPheromonesForIndexZeroOneTest()
        {
            m_TrailBuilderOne.EdgeInTrail(0,
                                          1).Returns(false);
            m_TrailBuilderOne.Length.Returns(10.0);

            m_Pheromones.UpdateForAnt(m_AntOne,
                                      0,
                                      1);

            const double expected = 4.9749999999999996d;
            double actual = m_Pheromones.GetValue(0,
                                                  1);

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual,
                                           0.0001);
        }

        [Test]
        public void ValueRoundtripTest()
        {
            m_Pheromones.SetValue(0,
                                  0,
                                  10.0);

            NUnitHelper.AssertIsEquivalent(10.0,
                                           m_Pheromones.GetValue(0,
                                                                 0));
        }
    }
}