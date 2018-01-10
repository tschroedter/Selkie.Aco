using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Aco.Trails.Optimizers;
using Core2.Selkie.Common.Interfaces;

namespace Core2.Selkie.Aco.Trails.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    // ReSharper disable once ClassTooBig
    internal sealed class BaseTrailBuilderTests
    {
        [SetUp]
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();
            m_Tracker = Substitute.For <IPheromonesTracker>();
            m_Graph = Substitute.For <IDistanceGraph>();

            m_Chromosome = Substitute.For <IChromosome>();
            m_Chromosome.Alpha.Returns(3.0);
            m_Chromosome.Beta.Returns(2.0);
            m_Chromosome.Gamma.Returns(1.0);

            m_Optimizer = new TwoOptSimple
                          {
                              DistanceGraph = m_Graph
                          };

            m_Graph.NumberOfNodes.Returns(10);
            m_Graph.MinimumDistance.Returns(1);

            m_Sut = new TestBaseTrailBuilder(m_Random,
                                             m_Chromosome,
                                             m_Tracker,
                                             m_Graph,
                                             m_Optimizer);
        }

        private TestBaseTrailBuilder m_Sut;
        private IPheromonesTracker m_Tracker;
        private IDistanceGraph m_Graph;
        private IChromosome m_Chromosome;
        private IOptimizer m_Optimizer;
        private IRandom m_Random;

        private class TestBaseTrailBuilder : BaseTrailBuilder <IUnknownTrailBuilder>
        {
            public TestBaseTrailBuilder([NotNull] IRandom random,
                                        [NotNull] IChromosome chromosome,
                                        [NotNull] IPheromonesTracker tracker,
                                        [NotNull] IDistanceGraph graph,
                                        [NotNull] IOptimizer optimizer)
                : base(random,
                       chromosome,
                       tracker,
                       graph,
                       optimizer)
            {
                Trail = new[]
                        {
                            0,
                            1,
                            2,
                            3
                        };

                Length = 123;
            }

            public bool BuildTrailWasCalled { get; private set; }

            internal override void BuildTrail(int startNode)
            {
                BuildTrailWasCalled = true;
            }
        }

        [Test]
        public void Build_CallsBuildTrailWasCalled_UnderCondition()
        {
            // Arrange
            // Act
            m_Sut.Build(0);

            // Assert
            Assert.True(m_Sut.BuildTrailWasCalled);
        }

        [Test]
        public void BuildTrailThrowsForStartBiggerNumberOfNodesTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws <ArgumentException>(() => m_Sut.Build(1000));
        }

        [Test]
        public void BuildTrailThrowsForStartEqualNumberOfGraphNodesTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws <ArgumentException>(() => m_Sut.Build(m_Graph.NumberOfNodes));
        }

        [Test]
        public void BuildTrailThrowsForStartGreaterNumberOfGraphNodesTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws <ArgumentException>(() => m_Sut.Build(m_Graph.NumberOfNodes + 1));
        }

        [Test]
        public void BuildTrailThrowsForStartLessZeroTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws <ArgumentException>(() => m_Sut.Build(-1));
        }

        [Test]
        public void ToString_ReturnsString_WhenCalled()
        {
            // Arrange
            const string expected = "Length: 0123 [0 1 2 3]";

            // Act
            string actual = m_Sut.ToString();

            // Assert
            Assert.AreEqual(expected,
                            actual);
        }
    }
}