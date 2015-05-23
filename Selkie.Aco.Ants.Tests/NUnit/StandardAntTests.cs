using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails;
using Selkie.Aco.Trails.Optimizers;
using Selkie.Common;

namespace Selkie.Aco.Ants.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class StandardAntTests
    {
        [SetUp]
        // ReSharper disable once MethodTooLong
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();
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

            m_Optimizer = new TwoOptSimple
                          {
                              DistanceGraph = m_Graph
                          };

            m_Random = Substitute.For <IRandom>();

            m_Ant = new StandardAnt(m_Random,
                                    m_TrailBuilderFactory,
                                    m_Chromosome,
                                    m_Tracker,
                                    m_Graph,
                                    m_Optimizer,
                                    new int[0]);
        }

        private StandardAnt m_Ant;
        private IPheromonesTracker m_Tracker;
        private IDistanceGraph m_Graph;
        private IChromosome m_Chromosome;
        private ITrailBuilderFactory m_TrailBuilderFactory;
        private IOptimizer m_Optimizer;
        private IRandom m_Random;

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
        public void CloneTest()
        {
            var factory = Substitute.For <IAntFactory>();

            m_Ant.Clone(factory,
                        Substitute.For <IChromosomeFactory>());

            factory.Received().Create <IStandardAnt>(Arg.Any <IChromosome>(),
                                                     Arg.Any <IPheromonesTracker>(),
                                                     Arg.Any <IDistanceGraph>(),
                                                     Arg.Any <IOptimizer>(),
                                                     Arg.Any <IEnumerable <int>>());
        }

        [Test]
        public void TrailBuilderLengthTest()
        {
            m_Ant.Update();

            Assert.True(m_Ant.TrailBuilder.Length > 0.0);
        }

        [Test]
        public void TrailBuilderTypeTest()
        {
            Assert.AreEqual(typeof ( IStandardTrailBuilder ).Name,
                            m_Ant.TrailBuilder.Type);
        }

        [Test]
        public void TrailLengthTest()
        {
            m_Ant.Update();

            IEnumerable <int> trail = m_Ant.TrailBuilder.Trail;

            Assert.AreEqual(m_Graph.NumberOfUniqueNodes,
                            trail.Count());
        }

        [Test]
        public void TrailTest()
        {
            m_Ant.Update();

            IEnumerable <int> trail = m_Ant.TrailBuilder.Trail;
            int[] actual = trail.ToArray();

            Assert.AreEqual(4,
                            m_Graph.NumberOfNodes,
                            "Length");
            Assert.AreEqual(2,
                            actual.Length,
                            "Length");
        }

        [Test]
        public void TypeTest()
        {
            Assert.AreEqual(typeof ( IStandardAnt ).Name,
                            m_Ant.Type);
        }

        [Test]
        public void UpdateCreatesTrailTest()
        {
            ITrailBuilder trailBuilder = m_Ant.TrailBuilder;

            List <int> oldTrail = trailBuilder.Trail.ToList();

            m_Ant.Update();

            List <int> newTrail = trailBuilder.Trail.ToList();

            Assert.False(oldTrail.SequenceEqual(newTrail));
        }
    }
}