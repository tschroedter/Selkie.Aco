using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core2.Selkie.Aco.Ants.Interfaces;
using Core2.Selkie.Aco.Common;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Common.TypedFactories;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Aco.Trails.Optimizers;
using Core2.Selkie.Common.Interfaces;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;

namespace Core2.Selkie.Aco.Ants.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    // ReSharper disable MaximumChainedReferences
    internal sealed class CandidateListAntTests
    {
        [SetUp]
        // ReSharper disable once MethodTooLong
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();

            m_TrailBuilderFactory = new TestTrailBuilderFactory();
            m_Chromosome = Substitute.For <IChromosome>();
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

            m_Ant = new CandidateListAnt(m_Random,
                                         m_TrailBuilderFactory,
                                         m_Chromosome,
                                         m_Tracker,
                                         m_Graph,
                                         m_Optimizer,
                                         m_AntSettings,
                                         new int[0]);
        }

        private CandidateListAnt m_Ant;
        private IPheromonesTracker m_Tracker;
        private IDistanceGraph m_Graph;
        private IChromosome m_Chromosome;
        private ITrailBuilderFactory m_TrailBuilderFactory;
        private IOptimizer m_Optimizer;
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
        public void Clone_CallsFactory_WhenCalled()
        {
            // Arrange
            var antFactory = Substitute.For <IAntFactory>();
            var chromosomeFactory = Substitute.For <IChromosomeFactory>();

            // Act
            m_Ant.Clone(antFactory,
                        chromosomeFactory);

            // Assert
            antFactory.Received().Create <ICandidateListAnt>(Arg.Any <IChromosome>(),
                                                             Arg.Any <IPheromonesTracker>(),
                                                             Arg.Any <IDistanceGraph>(),
                                                             Arg.Any <IOptimizer>(),
                                                             Arg.Any <IAntSettings>(),
                                                             Arg.Any <IEnumerable <int>>());
        }

        [Test]
        public void TrailBuilderType_ReturnsString_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(typeof( ICandidateListTrailBuilder ).Name,
                            m_Ant.TrailBuilder.Type);
        }

        [Test]
        public void Type_ReturnsString_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(typeof( ICandidateListAnt ).Name,
                            m_Ant.Type);
        }

        [Test]
        public void Update_CalculatesTrail_WhenCalled()
        {
            // Arrange
            // Act
            m_Ant.Update();

            // Assert
            int[] actual = m_Ant.TrailBuilder.Trail.ToArray();

            Assert.AreEqual(4,
                            m_Graph.NumberOfNodes,
                            "Length");
            Assert.AreEqual(2,
                            actual.Length,
                            "Length");
        }

        [Test]
        public void Update_CalculatesTrailWithCorrectLength_WhenCalled()
        {
            // Arrange
            // Act
            m_Ant.Update();

            // Assert
            Assert.AreEqual(m_Graph.NumberOfUniqueNodes,
                            m_Ant.TrailBuilder.Trail.Count());
        }

        [Test]
        public void Update_CreatesTrail_WhenCalled()
        {
            // Arrange
            List <int> oldTrail = m_Ant.TrailBuilder.Trail.ToList();

            // Act
            m_Ant.Update();

            // Assert
            List <int> newTrail = m_Ant.TrailBuilder.Trail.ToList();

            Assert.False(oldTrail.SequenceEqual(newTrail));
        }

        [Test]
        public void Update_ReturnsLength_WhenCalled()
        {
            // Arrange
            // Act
            m_Ant.Update();

            // Assert
            Assert.True(m_Ant.TrailBuilder.Length > 0.0);
        }

        [Test]
        public void UpdateWithFixedStartNode_CallsBuilderWithStartNode_WhenCalled()
        {
            // Arrange
            m_AntSettings.IsFixedStartNode.Returns(true);
            m_AntSettings.FixedStartNode.Returns(3);

            var builder = Substitute.For <ICandidateListTrailBuilder>();
            var factory = Substitute.For <ITrailBuilderFactory>();
            factory.Create <ICandidateListTrailBuilder>(m_Chromosome,
                                                        m_Tracker,
                                                        m_Graph,
                                                        m_Optimizer,
                                                        new int[0]).ReturnsForAnyArgs(builder);

            var ant = new CandidateListAnt(m_Random,
                                           factory,
                                           m_Chromosome,
                                           m_Tracker,
                                           m_Graph,
                                           m_Optimizer,
                                           m_AntSettings,
                                           new int[0]);

            // Act
            ant.Update();

            // Assert
            builder.Received().Build(3);
        }
    }
}