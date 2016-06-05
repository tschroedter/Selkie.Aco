using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Ants.Interfaces;
using Selkie.Aco.Common;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails.Interfaces;
using Selkie.Aco.Trails.Optimizers;
using Selkie.Common.Interfaces;

namespace Selkie.Aco.Ants.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    // ReSharper disable MaximumChainedReferences
    internal sealed class FixedAntTests
    {
        [SetUp]
        // ReSharper disable MethodTooLong
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();
            m_Random.NextDouble().Returns(0.1,
                                          0.2,
                                          0.3);

            m_TrailBuilderFactory = new TestTrailBuilderFactory();
            m_Chromosome = new Chromosome(m_Random,
                                          1.0,
                                          2.0,
                                          3.0);

            m_Tracker = CreatePheromonesTracker();

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

            m_Trail = new[]
                      {
                          0,
                          1,
                          2,
                          3
                      };

            m_Optimizer = new TwoOptSimple
                          {
                              DistanceGraph = m_Graph
                          };

            m_Random = Substitute.For <IRandom>();

            m_AntSettings = Substitute.For <IAntSettings>();

            m_Ant = new FixedAnt(m_Random,
                                 m_TrailBuilderFactory,
                                 m_Chromosome,
                                 m_Tracker,
                                 m_Graph,
                                 m_Optimizer,
                                 m_AntSettings,
                                 m_Trail);
        }

        // ReSharper restore MethodTooLong
        private FixedAnt m_Ant;
        private IChromosome m_Chromosome;
        private IDistanceGraph m_Graph;
        private TwoOptSimple m_Optimizer;
        private IRandom m_Random;
        private IPheromonesTracker m_Tracker;
        private int[] m_Trail;
        private ITrailBuilderFactory m_TrailBuilderFactory;
        private IAntSettings m_AntSettings;

        [NotNull]
        // ReSharper disable once MethodTooLong
        private static IPheromonesTracker CreatePheromonesTracker()
        {
            var tracker = Substitute.For <IPheromonesTracker>();

            tracker.GetValue(0,
                             0).Returns(1.0);
            tracker.GetValue(0,
                             1).Returns(2.0);
            tracker.GetValue(0,
                             2).Returns(3.0);
            tracker.GetValue(0,
                             3).Returns(4.0);

            tracker.GetValue(1,
                             0).Returns(5.0);
            tracker.GetValue(1,
                             1).Returns(6.0);
            tracker.GetValue(1,
                             2).Returns(7.0);
            tracker.GetValue(1,
                             3).Returns(8.0);

            tracker.GetValue(2,
                             0).Returns(9.0);
            tracker.GetValue(2,
                             1).Returns(10.0);
            tracker.GetValue(2,
                             2).Returns(11.0);
            tracker.GetValue(2,
                             3).Returns(12.0);

            tracker.GetValue(3,
                             0).Returns(13.0);
            tracker.GetValue(3,
                             1).Returns(14.0);
            tracker.GetValue(3,
                             2).Returns(15.0);
            tracker.GetValue(3,
                             3).Returns(16.0);

            return tracker;
        }

        [Test]
        public void Chromosome_ReturnsChromosome_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Chromosome,
                            m_Ant.Chromosome);
        }

        [Test]
        public void Clone_CallsFactory_WhenCalled()
        {
            // Arrange
            var antFactory = Substitute.For <IAntFactory>();
            var chromosomeFactory = Substitute.For <IChromosomeFactory>();

            // Act
            m_Ant.Clone(antFactory,
                        chromosomeFactory);

            // Assert
            antFactory.Received().Create <IFixedAnt>(Arg.Any <IChromosome>(),
                                                     Arg.Any <IPheromonesTracker>(),
                                                     Arg.Any <IDistanceGraph>(),
                                                     Arg.Any <IOptimizer>(),
                                                     Arg.Any <IAntSettings>(),
                                                     Arg.Any <IEnumerable <int>>());
        }

        [Test]
        public void Constructor_ReturnsInstance_ForEmptyTrail()
        {
            // Arrange
            // Act
            var ant = new FixedAnt(m_Random,
                                   m_TrailBuilderFactory,
                                   m_Chromosome,
                                   m_Tracker,
                                   m_Graph,
                                   m_Optimizer,
                                   m_AntSettings,
                                   new int[0]);

            // Assert
            Assert.AreEqual(typeof( IFixedAnt ).Name,
                            ant.Type,
                            "Type");
            Assert.AreEqual(m_Ant.Chromosome,
                            ant.Chromosome,
                            "Chromosome");
            Assert.AreEqual(typeof( IFixedTrailBuilder ).Name,
                            ant.TrailBuilder.Type,
                            "TrailBuilder.Type");
            Assert.AreEqual(0,
                            ant.TrailBuilder.Trail.Count(),
                            "Trail");
        }

        [Test]
        public void NumberOfNodes_ReturnsCount_WhenCalled()
        {
            // Arrange
            // Act
            m_Ant.Update();

            // Assert
            Assert.AreEqual(4,
                            m_Graph.NumberOfNodes);
        }

        [Test]
        public void TrailBuilderType_ReturnsString_WhenCalledt()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(typeof( IFixedTrailBuilder ).Name,
                            m_Ant.TrailBuilder.Type);
        }

        [Test]
        public void Type_ReturnsString_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(typeof( IFixedAnt ).Name,
                            m_Ant.Type);
        }

        [Test]
        public void UpdateWithFixedStartNode_DoesNotCreateNewTrail_WhenCalled()
        {
            // Arrange
            m_AntSettings.IsFixedStartNode.Returns(true);
            m_AntSettings.FixedStartNode.Returns(10);

            // Act
            m_Ant.Update();

            // Assert
            List <int> newTrail = m_Ant.TrailBuilder.Trail.ToList();

            Assert.True(m_Trail.SequenceEqual(newTrail));
        }

        [Test]
        public void UpdateWithRandomStartNode_DoesNotCreateNewTrail_WhenCalled()
        {
            // Arrange
            // Act
            m_Ant.Update();

            // Assert
            List <int> newTrail = m_Ant.TrailBuilder.Trail.ToList();

            Assert.True(m_Trail.SequenceEqual(newTrail));
        }
    }
}