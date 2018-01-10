using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Common.TypedFactories;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Aco.Trails.Optimizers;
using Core2.Selkie.Common.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Core2.Selkie.Aco.Trails.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class FixedTrailBuilderTests
    {
        [SetUp]
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();
            m_Chromosome = Substitute.For <IChromosome>();
            m_Tracker = Substitute.For <IPheromonesTracker>();
            m_Graph = Substitute.For <IDistanceGraph>();
            m_Graph.NumberOfNodes.Returns(4);
            m_Graph.NumberOfUniqueNodes.Returns(2);
            m_Graph.MinimumDistance.Returns(10.0);

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

            m_TrailBuilder = new FixedTrailBuilder(m_Random,
                                                   m_Chromosome,
                                                   m_Tracker,
                                                   m_Graph,
                                                   m_Optimizer,
                                                   m_Trail);
        }

        private IChromosome m_Chromosome;
        private IDistanceGraph m_Graph;
        private IOptimizer m_Optimizer;
        private IRandom m_Random;
        private IPheromonesTracker m_Tracker;
        private int[] m_Trail;
        private FixedTrailBuilder m_TrailBuilder;

        [Test]
        public void BuildTrailTest()
        {
            m_TrailBuilder.BuildTrail(0);

            Assert.AreEqual(m_Trail,
                            m_TrailBuilder.Trail);
        }

        [Test]
        public void CloneTest()
        {
            var trailBuilderFactory = Substitute.For <ITrailBuilderFactory>();
            var chromosomeFactory = Substitute.For <IChromosomeFactory>();

            m_TrailBuilder.Clone(trailBuilderFactory,
                                 chromosomeFactory);

            trailBuilderFactory.DidNotReceiveWithAnyArgs().Create <IFixedTrailBuilder>(Arg.Any <IChromosome>(),
                                                                                       Arg.Any <IPheromonesTracker>(),
                                                                                       Arg.Any <IDistanceGraph>(),
                                                                                       Arg.Any <IOptimizer>());
        }

        [Test]
        public void ConstructorWithoutTrailTest()
        {
            var trailBuilder = new FixedTrailBuilder(m_Random,
                                                     m_Chromosome,
                                                     m_Tracker,
                                                     m_Graph,
                                                     m_Optimizer);

            Assert.AreEqual(typeof( IFixedTrailBuilder ).Name,
                            trailBuilder.Type,
                            "Type");
            Assert.AreEqual(m_Chromosome,
                            trailBuilder.Chromosome,
                            "Chromosome");
            Assert.AreEqual(0,
                            trailBuilder.Trail.Count(),
                            "Trail.Length");
        }

        [Test]
        public void IsUnknownReturnsFalseTest()
        {
            Assert.False(m_TrailBuilder.IsUnknown);
        }

        [Test]
        public void TypeTest()
        {
            Assert.AreEqual(typeof( IFixedTrailBuilder ).Name,
                            m_TrailBuilder.Type);
        }
    }
}