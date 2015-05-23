using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails.Optimizers;
using Selkie.Common;

namespace Selkie.Aco.Trails.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class UnknownTrailBuilderTests
    {
        [SetUp]
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();

            var graph = Substitute.For <IDistanceGraph>();
            graph.NumberOfNodes.Returns(2);
            graph.MinimumDistance.Returns(10.0);

            var tracker = Substitute.For <IPheromonesTracker>();
            var optimizer = new TwoOptSimple
                            {
                                DistanceGraph = graph
                            };

            var unknown = Substitute.For <IChromosome>();
            unknown.IsUnknown.Returns(true);

            m_TrailBuilder = new UnknownTrailBuilder(m_Random,
                                                     unknown,
                                                     tracker,
                                                     graph,
                                                     optimizer);
        }

        private IRandom m_Random;
        private UnknownTrailBuilder m_TrailBuilder;

        [Test]
        public void BuildTrailDoesNothingTest()
        {
            m_TrailBuilder.BuildTrail(0);

            Assert.AreEqual(0,
                            m_TrailBuilder.Trail.Count());
        }

        [Test]
        public void BuildTrailTest()
        {
            m_TrailBuilder.BuildTrail(0);

            Assert.AreEqual(0,
                            m_TrailBuilder.Trail.Count());
        }

        [Test]
        public void CloneReturnsSameInstanceTest()
        {
            var trailBuilderFactory = Substitute.For <ITrailBuilderFactory>();
            var chromosomeFactory = Substitute.For <IChromosomeFactory>();

            ITrailBuilder clone = m_TrailBuilder.Clone(trailBuilderFactory,
                                                       chromosomeFactory);

            Assert.AreEqual(m_TrailBuilder,
                            clone);
        }

        [Test]
        public void CloneTest()
        {
            var trailBuilderFactory = Substitute.For <ITrailBuilderFactory>();
            var chromosomeFactory = Substitute.For <IChromosomeFactory>();

            m_TrailBuilder.Clone(trailBuilderFactory,
                                 chromosomeFactory);

            trailBuilderFactory.DidNotReceive().Create <IUnknownTrailBuilder>(Arg.Any <IChromosome>(),
                                                                              Arg.Any <IPheromonesTracker>(),
                                                                              Arg.Any <IDistanceGraph>(),
                                                                              Arg.Any <IOptimizer>());
        }

        [Test]
        public void IdTest()
        {
            Assert.AreEqual(BaseTrailBuilder <UnknownTrailBuilder>.UnknownId,
                            m_TrailBuilder.Id);
        }

        [Test]
        public void IsUnknownReturnsTrueTest()
        {
            Assert.True(m_TrailBuilder.IsUnknown);
        }
    }
}