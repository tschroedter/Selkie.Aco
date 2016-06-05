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
    internal sealed class UnknownAntTests
    {
        [SetUp]
        // ReSharper disable once MethodTooLong
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();
            m_TrailBuilderFactory = new TestTrailBuilderFactory();
            m_Chromosome = Chromosome.Unknown;
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

            m_Optimizer = new TwoOptSimple
                          {
                              DistanceGraph = m_Graph
                          };

            m_Random = Substitute.For <IRandom>();

            m_AntSettings = Substitute.For <IAntSettings>();

            m_Ant = new UnknownAnt(m_Random,
                                   m_TrailBuilderFactory,
                                   m_Chromosome,
                                   m_Tracker,
                                   m_Graph,
                                   m_Optimizer,
                                   m_AntSettings,
                                   new int[0]);
        }

        private UnknownAnt m_Ant;
        private IPheromonesTracker m_Tracker;
        private IDistanceGraph m_Graph;
        private IChromosome m_Chromosome;
        private ITrailBuilderFactory m_TrailBuilderFactory;
        private TwoOptSimple m_Optimizer;
        private IRandom m_Random;
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
        public void ChromosomeTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Chromosome,
                            m_Ant.Chromosome);
        }

        [Test]
        public void Clone_ReturnsItSelf_WhenCalled()
        {
            // Arrange
            var factory = Substitute.For <IAntFactory>();

            // Act
            IAnt clone = m_Ant.Clone(factory,
                                     Substitute.For <IChromosomeFactory>());

            // Assert
            Assert.AreEqual(m_Ant,
                            clone);
        }

        [Test]
        public void CloneCallsFactory_WhenCalled()
        {
            // Arrange
            var factory = Substitute.For <IAntFactory>();

            // Act
            m_Ant.Clone(factory,
                        Substitute.For <IChromosomeFactory>());

            // Assert
            factory.DidNotReceiveWithAnyArgs().Create <IUnknownAnt>(Arg.Any <IChromosome>(),
                                                                    Arg.Any <IPheromonesTracker>(),
                                                                    Arg.Any <IDistanceGraph>(),
                                                                    Arg.Any <IOptimizer>(),
                                                                    Arg.Any <IAntSettings>(),
                                                                    Arg.Any <IEnumerable <int>>());
        }

        [Test]
        public void Constuctor_ReturnsInstance_WhenCalled()
        {
            // Arrange
            // Act
            var ant = new UnknownAnt(m_Random,
                                     m_TrailBuilderFactory,
                                     m_Chromosome,
                                     m_Tracker,
                                     m_Graph,
                                     m_Optimizer,
                                     m_AntSettings,
                                     new int[0]);

            // Assert
            Assert.AreEqual(typeof( IUnknownAnt ).Name,
                            ant.Type,
                            "Type");
            Assert.AreEqual(m_Chromosome,
                            ant.Chromosome,
                            "Chromosome");
            Assert.True(ant.TrailBuilder.IsUnknown,
                        "IsUnknown");
        }

        [Test]
        public void Trail_ReturnsTrailWithLengthMaxValue_WhenCalled()
        {
            // Arrange
            // Act
            m_Ant.Update();

            // Assert
            Assert.AreEqual(double.MaxValue,
                            m_Ant.TrailBuilder.Length);
        }

        [Test]
        public void TrailBuilderType_ReturnsString_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(typeof( IUnknownTrailBuilder ).Name,
                            m_Ant.TrailBuilder.Type);
        }

        [Test]
        public void Type_ReturnsString_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(typeof( IUnknownAnt ).Name,
                            m_Ant.Type);
        }

        [Test]
        public void Update_ReturnsEmptyTrail_WhenCalled()
        {
            // Arrange
            // Act
            m_Ant.Update();

            // Assert
            IEnumerable <int> trail = m_Ant.TrailBuilder.Trail;

            Assert.AreEqual(0,
                            trail.Count());
        }

        [Test]
        public void UpdateWithFixedStart_DoesNothing_WhenCalled()
        {
            // Arrange
            // Act
            m_Ant.Update();

            // Assert
            IEnumerable <int> trail = m_Ant.TrailBuilder.Trail;

            Assert.AreEqual(0,
                            trail.Count());
        }
    }
}