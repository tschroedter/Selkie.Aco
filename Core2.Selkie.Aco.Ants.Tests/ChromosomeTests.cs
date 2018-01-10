using System;
using System.Diagnostics.CodeAnalysis;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Common.TypedFactories;
using Core2.Selkie.Common.Interfaces;
using Core2.Selkie.NUnit.Extensions;
using NSubstitute;
using NUnit.Framework;

namespace Core2.Selkie.Aco.Ants.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ChromosomeTests
    {
        [SetUp]
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();
            m_Random.NextDouble().Returns(0.1,
                                          0.2,
                                          0.3);
        }

        private IRandom m_Random;

        [Test]
        public void CloneMinMaxTest()
        {
            var factory = Substitute.For <IChromosomeFactory>();

            var chromosome = new Chromosome(m_Random,
                                            1.0,
                                            2.0,
                                            3.0,
                                            4.0,
                                            5.0,
                                            6.0);

            chromosome.Clone(factory);

            factory.Received().Create(chromosome.Alpha,
                                      chromosome.Beta,
                                      chromosome.Gamma,
                                      chromosome.AlphaMinValue,
                                      chromosome.AlphaMaxValue,
                                      chromosome.BetaMinValue,
                                      chromosome.BetaMaxValue,
                                      chromosome.GammaMinValue,
                                      chromosome.GammaMaxValue);
        }

        [Test]
        public void ConstructorAlphaBetaGammaTest()
        {
            var chromosome = new Chromosome(m_Random,
                                            1.0,
                                            2.0,
                                            3.0);

            Assert.AreEqual(1.0,
                            chromosome.Alpha,
                            "Alpha");
            Assert.AreEqual(2.0,
                            chromosome.Beta,
                            "Beta");
            Assert.AreEqual(3.0,
                            chromosome.Gamma,
                            "Gamma");
        }

        [Test]
        public void ConstructorMinMaxTest()
        {
            var chromosome = new Chromosome(m_Random,
                                            1.0,
                                            2.0,
                                            3.0,
                                            4.0,
                                            5.0,
                                            6.0);

            Assert.AreEqual(1.0,
                            chromosome.AlphaMinValue,
                            "AlphaMinValue");
            Assert.AreEqual(2.0,
                            chromosome.AlphaMaxValue,
                            "AlphaMaxValue");
            Assert.AreEqual(3.0,
                            chromosome.BetaMinValue,
                            "BetaMinValue");
            Assert.AreEqual(4.0,
                            chromosome.BetaMaxValue,
                            "BetaMaxValue");
            Assert.AreEqual(5.0,
                            chromosome.GammaMinValue,
                            "GammaMinValue");
            Assert.AreEqual(6.0,
                            chromosome.GammaMaxValue,
                            "GammaMaxValue");
        }

        [Test]
        public void ConstructorMinMaxValueTest()
        {
            var chromosome = new Chromosome(m_Random,
                                            1.0,
                                            2.0,
                                            3.0,
                                            4.0,
                                            5.0,
                                            6.0);

            Assert.True(chromosome.Alpha >= 1.0 && chromosome.Alpha <= 2,
                        "Alpha");
            Assert.True(chromosome.Beta >= 3.0 && chromosome.Beta <= 4,
                        "Beta");
            Assert.True(chromosome.Gamma >= 5.0 && chromosome.Gamma <= 6.0,
                        "Gamma");
        }

        [Test]
        public void ConstructorRandomTest()
        {
            var chromosome = new Chromosome(m_Random);

            Assert.True(chromosome.Alpha >= chromosome.AlphaMinValue && chromosome.Alpha <= chromosome.AlphaMaxValue,
                        "Alpha");
            Assert.True(chromosome.Beta >= chromosome.BetaMinValue && chromosome.Beta <= chromosome.BetaMaxValue,
                        "Beta");
            Assert.True(chromosome.Gamma >= chromosome.GammaMinValue && chromosome.Gamma <= chromosome.GammaMaxValue,
                        "Gamma");
        }

        [Test]
        public void EqualsForOtherIsNullTest()
        {
            var chromosome = new Chromosome(m_Random,
                                            1.0,
                                            2.0,
                                            3.0);

            Assert.False(chromosome.Equals(null));
        }

        [Test]
        public void EqualsForSameTest()
        {
            var chromosome = new Chromosome(m_Random,
                                            1.0,
                                            2.0,
                                            3.0);

            Assert.True(chromosome.Equals(chromosome));
        }

        [Test]
        public void EqualsForSameValuesTest()
        {
            var chromosome = new Chromosome(m_Random,
                                            1.0,
                                            2.0,
                                            3.0);

            var other = new Chromosome(m_Random,
                                       1.0,
                                       2.0,
                                       3.0);

            Assert.True(chromosome.Equals(other));
        }

        [Test]
        public void IsUnknownReturnsFalseTest()
        {
            var chromosome = new Chromosome(m_Random,
                                            1.0,
                                            2.0,
                                            3.0);

            Assert.False(chromosome.IsUnknown);
        }

        [Test]
        public void IsUnknownReturnsTrueTest()
        {
            IChromosome chromosome = Chromosome.Unknown;

            Assert.True(chromosome.IsUnknown);
        }

        [Test]
        public void RandomizeAlphaTest()
        {
            m_Random.NextDouble().Returns(1.0);

            var chromosome = new Chromosome(m_Random,
                                            1.0,
                                            2.0,
                                            3.0,
                                            5.0,
                                            5.0,
                                            8.0);

            const double expected = 2.0;
            double actual = chromosome.RandomizeAlpha();

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual);
        }

        [Test]
        public void RandomizeBetaTest()
        {
            m_Random.NextDouble().Returns(1.0);

            var chromosome = new Chromosome(m_Random,
                                            1.0,
                                            2.0,
                                            3.0,
                                            5.0,
                                            5.0,
                                            8.0);

            const double expected = 5.0;
            double actual = chromosome.RandomizeBeta();

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual);
        }

        [Test]
        public void RandomizeChromosomeTest()
        {
            var random = Substitute.For <IRandom>();
            random.NextDouble().Returns(10.0,
                                        20.0,
                                        30.0);

            var chromosome = new Chromosome(m_Random,
                                            0.1,
                                            0.2,
                                            0.3,
                                            1.0,
                                            2.0,
                                            3.0,
                                            5.0,
                                            5.0,
                                            8.0);

            IChromosome actual = chromosome.Randomize();

            Assert.True(Math.Abs(chromosome.Alpha - actual.Alpha) > 0.001,
                        "Alpha");
            Assert.True(Math.Abs(chromosome.Beta - actual.Beta) > 0.001,
                        "Alpha");
            Assert.True(Math.Abs(chromosome.Gamma - actual.Gamma) > 0.001,
                        "Alpha");
        }

        [Test]
        public void RandomizeGammaTest()
        {
            m_Random.NextDouble().Returns(1.0);

            var chromosome = new Chromosome(m_Random,
                                            1.0,
                                            2.0,
                                            3.0,
                                            5.0,
                                            5.0,
                                            8.0);

            const double expected = 8.0;
            double actual = chromosome.RandomizeGamma();

            NUnitHelper.AssertIsEquivalent(expected,
                                           actual);
        }

        [Test]
        public void RangeTest()
        {
            var chromosome = new Chromosome(m_Random,
                                            1.0,
                                            2.0,
                                            3.0,
                                            5.0,
                                            5.0,
                                            8.0);

            Assert.AreEqual(1.0,
                            chromosome.AlphaRange,
                            "AlphaRange");
            Assert.AreEqual(2.0,
                            chromosome.BetaRange,
                            "BetaRange");
            Assert.AreEqual(3.0,
                            chromosome.GammaRange,
                            "GammaRange");
        }

        [Test]
        public void ToStringTest()
        {
            var chromosome = new Chromosome(m_Random,
                                            1.0,
                                            2.0,
                                            3.0);

            const string expected = "Alpha: 1.0000 Beta: 2.0000 Gamma: 3.0000";
            string actual = chromosome.ToString();

            Assert.AreEqual(expected,
                            actual);
        }
    }
}