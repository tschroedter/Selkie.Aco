using System;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Common;
using Selkie.NUnit.Extensions;
using Selkie.Windsor;

namespace Selkie.Aco.Ants.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class CrossoverTests
    {
        [SetUp]
        public void Setup()
        {
            m_Disposer = Substitute.For <IDisposer>();

            m_Father = Substitute.For <IChromosome>();
            m_Father.Alpha.Returns(1.0);
            m_Father.Beta.Returns(2.0);
            m_Father.Gamma.Returns(3.0);

            m_Mother = Substitute.For <IChromosome>();
            m_Mother.Alpha.Returns(-1.0);
            m_Mother.Beta.Returns(-2.0);
            m_Mother.Gamma.Returns(-3.0);

            m_Logger = Substitute.For <ISelkieLogger>();
            m_Random = Substitute.For <IRandom>();
            m_Random.NextDouble().Returns(1.0,
                                          2.0,
                                          3.0);

            m_Factory = new TestChromosomeFactory();

            m_Crossover = new Crossover(m_Disposer,
                                        m_Logger,
                                        m_Random,
                                        m_Factory)
                          {
                              Disposer = m_Disposer
                          };
        }

        private class TestChromosomeFactory : IChromosomeFactory
        {
            private readonly IRandom m_Random = new SelkieRandom();

            public IChromosome Create(double alpha,
                                      double beta,
                                      double gamma)
            {
                return new Chromosome(m_Random,
                                      alpha,
                                      beta,
                                      gamma);
            }

            // ReSharper disable once TooManyArguments
            public IChromosome Create(double alpha,
                                      double beta,
                                      double gamma,
                                      double alphaMinValue,
                                      double alphaMaxValue,
                                      double betaMinValue,
                                      double betaMaxValue,
                                      double gammaMinValue,
                                      double gammaMaxValue)
            {
                return new Chromosome(m_Random,
                                      alpha,
                                      beta,
                                      gamma,
                                      alphaMinValue,
                                      alphaMaxValue,
                                      betaMinValue,
                                      betaMaxValue,
                                      gammaMinValue,
                                      gammaMaxValue);
            }

            public void Release(IChromosome chromosome)
            {
            }
        }

        private Crossover m_Crossover;
        private IChromosome m_Father;
        private IChromosome m_Mother;
        private IRandom m_Random;
        private ISelkieLogger m_Logger;
        private TestChromosomeFactory m_Factory;
        private IDisposer m_Disposer;

        [Test]
        public void CreateGenesTest()
        {
            bool[] actual = m_Crossover.CreateGenes();

            Assert.AreEqual(Crossover.NumberOfGenes,
                            actual.Length);
        }

        [Test]
        public void DisposeCallsDisposerTest()
        {
            var crossover = new Crossover(m_Disposer,
                                          m_Logger,
                                          m_Random,
                                          m_Factory);

            crossover.Dispose();

            m_Disposer.Received().Dispose();
        }

        [Test]
        public void MutationAddsToDisposerTest()
        {
            m_Crossover.Mutation(m_Father,
                                 Crossover.Gene.Alpha);

            m_Disposer.Received().AddResource(Arg.Any <Action>());
        }

        [Test]
        public void MutationForAlphaTest()
        {
            IChromosome actual = m_Crossover.Mutation(m_Father,
                                                      Crossover.Gene.Alpha);

            // Alpha was randomized
            Assert.AreEqual(2.0,
                            actual.Beta,
                            "Beta");
            Assert.AreEqual(3.0,
                            actual.Gamma,
                            "Gamma");
        }

        [Test]
        public void MutationForBetaTest()
        {
            IChromosome actual = m_Crossover.Mutation(m_Father,
                                                      Crossover.Gene.Beta);

            Assert.AreEqual(1.0,
                            actual.Alpha,
                            "Alpha");
            // Beta was randomized
            Assert.AreEqual(3.0,
                            actual.Gamma,
                            "Gamma");
        }

        [Test]
        public void MutationForGammaTest()
        {
            IChromosome actual = m_Crossover.Mutation(m_Father,
                                                      Crossover.Gene.Gamma);

            Assert.AreEqual(1.0,
                            actual.Alpha,
                            "Alpha");
            Assert.AreEqual(2.0,
                            actual.Beta,
                            "Beta");
            // Gamma was randomized
        }

        [Test]
        public void MutationTest()
        {
            IChromosome actual = m_Crossover.Mutation(m_Father);

            Assert.NotNull(actual);
        }

        [Test]
        public void OffspringAddsToDisposerTest()
        {
            m_Crossover.Offspring(m_Father,
                                  m_Mother);

            m_Disposer.Received().AddResource(Arg.Any <Action>());
        }

        [Test]
        public void OffspringForAlphaTest()
        {
            bool[] genes =
            {
                Crossover.FromFather,
                Crossover.FromMother,
                Crossover.FromMother
            };

            IChromosome actual = m_Crossover.Offspring(m_Father,
                                                       m_Mother,
                                                       genes);

            NUnitHelper.AssertIsEquivalent(2.0,
                                           actual.Alpha,
                                           "Alpha");
            NUnitHelper.AssertIsEquivalent(-8.0,
                                           actual.Beta,
                                           "Alpha");
            NUnitHelper.AssertIsEquivalent(-18.0,
                                           actual.Gamma,
                                           "Alpha");
        }

        [Test]
        public void OffspringForBetaTest()
        {
            bool[] genes =
            {
                Crossover.FromMother,
                Crossover.FromFather,
                Crossover.FromMother
            };

            IChromosome actual = m_Crossover.Offspring(m_Father,
                                                       m_Mother,
                                                       genes);

            NUnitHelper.AssertIsEquivalent(-2.0,
                                           actual.Alpha,
                                           "Alpha");
            NUnitHelper.AssertIsEquivalent(8.0,
                                           actual.Beta,
                                           "Alpha");
            NUnitHelper.AssertIsEquivalent(-18.0,
                                           actual.Gamma,
                                           "Alpha");
        }

        [Test]
        public void OffspringForGammaTest()
        {
            bool[] genes =
            {
                Crossover.FromMother,
                Crossover.FromMother,
                Crossover.FromFather
            };

            IChromosome actual = m_Crossover.Offspring(m_Father,
                                                       m_Mother,
                                                       genes);

            NUnitHelper.AssertIsEquivalent(-2.0,
                                           actual.Alpha,
                                           "Alpha");
            NUnitHelper.AssertIsEquivalent(-8.0,
                                           actual.Beta,
                                           "Alpha");
            NUnitHelper.AssertIsEquivalent(18.0,
                                           actual.Gamma,
                                           "Alpha");
        }

        [Test]
        public void OffspringTest()
        {
            IChromosome actual = m_Crossover.Offspring(m_Father,
                                                       m_Mother);

            Assert.NotNull(actual);
        }
    }
}