using System;
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
    internal sealed class BaseAntTests
    {
        [SetUp]
        // ReSharper disable MethodTooLong
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();
            m_TrailBuilderFactory = new TestTrailBuilderFactory();
            m_Chromosome = new Chromosome(m_Random,
                                          0.5,
                                          0.5,
                                          1.0);

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
                                              m_Pheromones,
                                              m_Graph);

            m_Optimizer = new TwoOptSimple
                          {
                              DistanceGraph = m_Graph
                          };

            m_Random = Substitute.For <IRandom>();

            m_Ant = new TestBaseAnt(m_Random,
                                    m_TrailBuilderFactory,
                                    m_Chromosome,
                                    m_Tracker,
                                    m_Graph,
                                    m_Optimizer);
        }

        // ReSharper restore MethodTooLong
        private TestBaseAnt m_Ant;
        private IChromosome m_Chromosome;
        private IDistanceGraph m_Graph;
        private IOptimizer m_Optimizer;
        private IPheromones m_Pheromones;
        private IRandom m_Random;
        private IPheromonesTracker m_Tracker;
        private ITrailBuilderFactory m_TrailBuilderFactory;

        internal sealed class TestBaseAnt : BaseAnt <TestBaseAnt, IRandomTrailBuilder>
        {
            // ReSharper disable TooManyDependencies
            public TestBaseAnt([NotNull] IRandom random,
                               [NotNull] ITrailBuilderFactory trailBuilderFactory,
                               [NotNull] IChromosome chromosome,
                               [NotNull] IPheromonesTracker tracker,
                               [NotNull] IDistanceGraph graph,
                               [NotNull] IOptimizer optimizer)
                : base(random,
                       trailBuilderFactory,
                       chromosome,
                       tracker,
                       graph,
                       optimizer,
                       new int[0])
            {
                int startNode = Random.Next(0,
                                            DistanceGraph.NumberOfNodes);

                var builder = new RandomTrailBuilder(random,
                                                     chromosome,
                                                     tracker,
                                                     graph,
                                                     optimizer);
                builder.BuildTrail(startNode);

                TrailBuilder = builder;
            }

            // ReSharper restore TooManyDependencies
            public override void Update()
            {
            }
        }

        [Test]
        public void AntTypeTest()
        {
            Assert.AreEqual("TestBaseAnt",
                            m_Ant.Type);
        }

        [Test]
        public void ChromosomeTest()
        {
            var newChromosome = Substitute.For <IChromosome>();

            m_Ant.Chromosome = newChromosome;

            Assert.AreEqual(newChromosome,
                            m_Ant.Chromosome);
        }

        [Test]
        public void CloneCallsAntFactoryTest()
        {
            var factory = Substitute.For <IAntFactory>();

            m_Ant.Clone(factory,
                        Substitute.For <IChromosomeFactory>());

            factory.ReceivedWithAnyArgs().Create <TestBaseAnt>(Arg.Any <IChromosome>(),
                                                               Arg.Any <IPheromonesTracker>(),
                                                               Arg.Any <IDistanceGraph>(),
                                                               Arg.Any <IOptimizer>(),
                                                               Arg.Any <IEnumerable <int>>());
        }

        [Test]
        public void CloneReturnsAntTest()
        {
            var factory = Substitute.For <IAntFactory>();

            factory.Create <TestBaseAnt>(Arg.Any <IChromosome>(),
                                         Arg.Any <IPheromonesTracker>(),
                                         Arg.Any <IDistanceGraph>(),
                                         Arg.Any <IOptimizer>(),
                                         Arg.Any <IEnumerable <int>>()).Returns(m_Ant);

            IAnt actual = m_Ant.Clone(factory,
                                      Substitute.For <IChromosomeFactory>());

            Assert.NotNull(actual);
        }

        [Test]
        public void DefaultAlfaTest()
        {
            Assert.AreEqual(m_Chromosome.Alpha,
                            m_Ant.Alpha);
        }

        [Test]
        public void DefaultBetaTest()
        {
            Assert.AreEqual(m_Chromosome.Beta,
                            m_Ant.Beta);
        }

        [Test]
        public void DefaultChromosomeTest()
        {
            Assert.AreEqual(m_Chromosome,
                            m_Ant.Chromosome);
        }

        [Test]
        public void DefaultGammaTest()
        {
            Assert.AreEqual(m_Chromosome.Gamma,
                            m_Ant.Gamma);
        }

        [Test]
        public void IdTest()
        {
            Assert.True(m_Ant.Id >= 0);
        }

        [Test]
        public void RandomizeChromosomeTest()
        {
            m_Random.NextDouble().Returns(1.0,
                                          2.0,
                                          3.0);

            double oldAlpha = m_Ant.Alpha;
            double oldBeta = m_Ant.Beta;
            double oldGamma = m_Ant.Gamma;

            m_Ant.RandomizeChromosome();

            IChromosome actual = m_Ant.Chromosome;

            Assert.True(Math.Abs(oldAlpha - actual.Alpha) > 0.001,
                        "Alpha");
            Assert.True(Math.Abs(oldBeta - actual.Beta) > 0.001,
                        "Alpha");
            Assert.True(Math.Abs(oldGamma - actual.Gamma) > 0.001,
                        "Alpha");
        }

        [Test]
        public void TrailTest()
        {
            IEnumerable <int> trail = m_Ant.TrailBuilder.Trail;

            Assert.AreEqual(4,
                            m_Graph.NumberOfNodes,
                            "Length");
            Assert.AreEqual(2,
                            trail.Count(),
                            "Count");
        }
    }
}