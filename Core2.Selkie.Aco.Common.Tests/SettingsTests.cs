using System.Diagnostics.CodeAnalysis;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.NUnit.Extensions;
using NSubstitute;
using NUnit.Framework;

namespace Core2.Selkie.Aco.Common.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class SettingsTests
    {
        [SetUp]
        public void Setup()
        {
            m_Chromosome = Substitute.For <IChromosome>();
            m_Chromosome.Alpha.Returns(1.0);
            m_Chromosome.Beta.Returns(2.0);
            m_Chromosome.Gamma.Returns(3.0);

            m_Tracker = Substitute.For <IPheromonesTracker>();
            m_Tracker.Rho.Returns(4.0);
            m_Tracker.Q.Returns(5.0);

            m_Ant = Substitute.For <IAnt>();
            m_Ant.Type.Returns("Unknown");
            m_Ant.Chromosome.Returns(m_Chromosome);

            m_Settings = new Settings(m_Ant,
                                      m_Tracker);
        }

        private const double Epsilon = 0.01;
        private IAnt m_Ant;
        private IChromosome m_Chromosome;
        private Settings m_Settings;
        private IPheromonesTracker m_Tracker;

        [Test]
        public void AlphaTest()
        {
            NUnitHelper.AssertIsEquivalent(m_Chromosome.Alpha,
                                           m_Settings.Alpha,
                                           Epsilon);
        }

        [Test]
        public void BetaTest()
        {
            NUnitHelper.AssertIsEquivalent(m_Chromosome.Beta,
                                           m_Settings.Beta,
                                           Epsilon);
        }

        [Test]
        public void GammaTest()
        {
            NUnitHelper.AssertIsEquivalent(m_Chromosome.Gamma,
                                           m_Settings.Gamma,
                                           Epsilon);
        }

        [Test]
        public void QTest()
        {
            NUnitHelper.AssertIsEquivalent(m_Tracker.Q,
                                           m_Settings.Q,
                                           Epsilon);
        }

        [Test]
        public void RhoTest()
        {
            NUnitHelper.AssertIsEquivalent(m_Tracker.Rho,
                                           m_Settings.Rho,
                                           Epsilon);
        }

        [Test]
        public void ToStringTest()
        {
            const string expected =
                "AntType = Unknown " + "Alpha = 1.0000 " + "Betta = 2.0000 " + "Gamma = 3.0000 " + "Rho = 4.0000 " +
                "Q = 5.0000";

            string actual = m_Settings.ToString();

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void TypeTest()
        {
            Assert.AreEqual(m_Ant.Type,
                            m_Settings.AntType);
        }
    }
}