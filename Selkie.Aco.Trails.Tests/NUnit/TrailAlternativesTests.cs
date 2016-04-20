using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Common.TypedFactories;

namespace Selkie.Aco.Trails.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class TrailAlternativesTests
    {
        [SetUp]
        public void Setup()
        {
            m_TrailBuilderFactory = Substitute.For <ITrailBuilderFactory>();
            m_ChromosomeFactory = Substitute.For <IChromosomeFactory>();

            m_Alternatives = new TrailAlternatives(m_TrailBuilderFactory,
                                                   m_ChromosomeFactory);
        }

        private TrailAlternatives m_Alternatives;
        private IChromosomeFactory m_ChromosomeFactory;
        private ITrailBuilderFactory m_TrailBuilderFactory;

        [NotNull]
        private static ITrailBuilder CreateTrailOne()
        {
            int[] trailNodes =
            {
                0,
                1,
                2
            };
            var trail = Substitute.For <ITrailBuilder>();
            trail.Trail.Returns(trailNodes);
            trail.Clone(Arg.Any <ITrailBuilderFactory>(),
                        Arg.Any <IChromosomeFactory>()).Returns(trail);

            return trail;
        }

        [NotNull]
        private static ITrailBuilder CreateTrailTwo()
        {
            int[] trailNodes =
            {
                1,
                0,
                2
            };
            var trail = Substitute.For <ITrailBuilder>();
            trail.Trail.Returns(trailNodes);
            trail.Clone(Arg.Any <ITrailBuilderFactory>(),
                        Arg.Any <IChromosomeFactory>()).Returns(trail);

            return trail;
        }

        [Test]
        public void AddTwoUnknownTrailsCountTest()
        {
            ITrailBuilder trail1 = CreateTrailOne();

            int[] trailNodes2 =
            {
                1,
                0,
                2
            };
            var trail2 = Substitute.For <ITrailBuilder>();
            trail2.Trail.Returns(trailNodes2);
            trail2.Clone(Arg.Any <ITrailBuilderFactory>(),
                         Arg.Any <IChromosomeFactory>()).Returns(trail2);

            m_Alternatives.AddAlternative(1,
                                          trail1);
            m_Alternatives.AddAlternative(2,
                                          trail2);

            Assert.AreEqual(2,
                            m_Alternatives.Count);
        }

        [Test]
        public void AddTwoUnknownTrailsNodesTest()
        {
            ITrailBuilder trail1 = CreateTrailOne();
            ITrailBuilder trail2 = CreateTrailOne();

            m_Alternatives.AddAlternative(1,
                                          trail1);
            m_Alternatives.AddAlternative(2,
                                          trail2);

            ITrailBuilder actual1 = m_Alternatives.Trails.First();
            Assert.True(trail1.Trail.SequenceEqual(actual1.Trail),
                        "Trail1");

            ITrailBuilder actual2 = m_Alternatives.Trails.Last();
            Assert.True(trail2.Trail.SequenceEqual(actual2.Trail),
                        "Trail2");
        }

        [Test]
        public void AddUnknownTrailCountTest()
        {
            int[] trailNodes =
            {
                0,
                1,
                2
            };
            var trail = Substitute.For <ITrailBuilder>();
            trail.Trail.Returns(trailNodes);

            m_Alternatives.AddAlternative(1,
                                          trail);

            Assert.AreEqual(1,
                            m_Alternatives.Count);
        }

        [Test]
        public void AddUnknownTrailNodesTest()
        {
            ITrailBuilder trail1 = CreateTrailOne();

            m_Alternatives.AddAlternative(1,
                                          trail1);

            ITrailBuilder actual = m_Alternatives.Trails.First();
            Assert.True(trail1.Trail.SequenceEqual(actual.Trail));
        }

        [Test]
        public void ClearCallsReleaseTest()
        {
            int[] trailNodes =
            {
                0,
                1,
                2
            };
            var trail = Substitute.For <ITrailBuilder>();
            trail.Trail.Returns(trailNodes);
            trail.Clone(Arg.Any <ITrailBuilderFactory>(),
                        Arg.Any <IChromosomeFactory>()).Returns(trail);

            m_Alternatives.AddAlternative(1,
                                          trail);

            m_Alternatives.Clear();

            m_TrailBuilderFactory.Received().Release(Arg.Any <ITrailBuilder>());
        }

        [Test]
        public void ClearTest()
        {
            int[] trailNodes =
            {
                0,
                1,
                2
            };
            var trail = Substitute.For <ITrailBuilder>();
            trail.Trail.Returns(trailNodes);
            trail.Clone(Arg.Any <ITrailBuilderFactory>(),
                        Arg.Any <IChromosomeFactory>()).Returns(trail);

            m_Alternatives.AddAlternative(1,
                                          trail);

            m_Alternatives.Clear();

            Assert.AreEqual(0,
                            m_Alternatives.Count);
        }

        [Test]
        public void ConvertValuesToListTest()
        {
            ITrailBuilder trail1 = CreateTrailOne();
            ITrailBuilder trail2 = CreateTrailTwo();

            m_Alternatives.AddAlternative(1,
                                          trail1);
            m_Alternatives.AddAlternative(2,
                                          trail2);

            ITrailBuilder[] actual = m_Alternatives.ConvertValuesToList().ToArray();

            IEnumerable <int> trailOne = actual.First().Trail;
            IEnumerable <int> trailTwo = actual.Last().Trail;

            List <int> one = trailOne.ToList();
            List <int> two = trailTwo.ToList();

            Assert.True(one.SequenceEqual(trail1.Trail),
                        "trail1");
            Assert.True(two.SequenceEqual(trail2.Trail),
                        "trail2");
        }

        [Test]
        public void IsKnownAlternativeReturnsFalseForEmptyValuesTest()
        {
            Assert.False(m_Alternatives.IsKnownAlternative(Substitute.For <ITrailBuilder>()));
        }

        [Test]
        public void IsKnownAlternativeReturnsFalseForTrailIsEmptyTest()
        {
            ITrailBuilder trail1 = CreateTrailOne();

            m_Alternatives.AddAlternative(1,
                                          trail1);

            var trailBuilder = Substitute.For <ITrailBuilder>();
            trailBuilder.Trail.Returns(new int[0]);

            Assert.False(m_Alternatives.IsKnownAlternative(trailBuilder));
        }

        [Test]
        public void IsKnownAlternativeReturnsFalseForUnknownTest()
        {
            ITrailBuilder trail1 = CreateTrailOne();

            m_Alternatives.AddAlternative(1,
                                          trail1);

            int[] trailNodes =
            {
                1,
                0,
                2
            };
            var trailBuilder = Substitute.For <ITrailBuilder>();
            trailBuilder.Trail.Returns(trailNodes);

            Assert.False(m_Alternatives.IsKnownAlternative(trailBuilder));
        }

        [Test]
        public void IsKnownAlternativeReturnsFalseForUnknownTrailTest()
        {
            int[] trailNodes =
            {
                0,
                1,
                2
            };
            var trail = Substitute.For <ITrailBuilder>();
            trail.Trail.Returns(trailNodes);

            Assert.False(m_Alternatives.IsKnownAlternative(trail));
        }

        [Test]
        public void IsKnownAlternativeReturnsTrueForKnownTest()
        {
            ITrailBuilder trail1 = CreateTrailOne();

            m_Alternatives.AddAlternative(1,
                                          trail1);

            int[] trailNodes =
            {
                0,
                1,
                2
            };
            var trailBuilder = Substitute.For <ITrailBuilder>();
            trailBuilder.Trail.Returns(trailNodes);

            Assert.True(m_Alternatives.IsKnownAlternative(trailBuilder));
        }

        [Test]
        public void IsKnownAlternativeReturnsTrueForKnownTrailTest()
        {
            int[] trailNodes =
            {
                0,
                1,
                2
            };
            var trail = Substitute.For <ITrailBuilder>();
            trail.Trail.Returns(trailNodes);
            trail.Clone(Arg.Any <ITrailBuilderFactory>(),
                        Arg.Any <IChromosomeFactory>()).Returns(trail);

            m_Alternatives.AddAlternative(1,
                                          trail);

            Assert.True(m_Alternatives.IsKnownAlternative(trail));
        }

        [Test]
        public void ToStringTest()
        {
            ITrailBuilder trail1 = CreateTrailOne();
            ITrailBuilder trail2 = CreateTrailTwo();

            m_Alternatives.AddAlternative(1,
                                          trail1);
            m_Alternatives.AddAlternative(2,
                                          trail2);

            const string expected =
                "Ant 1: \r\nCastle.Proxies.ITrailBuilderProxy\r\nAnt 2: \r\nCastle.Proxies.ITrailBuilderProxy\r\n";
            string actual = m_Alternatives.ToString();

            Assert.AreEqual(expected,
                            actual);
        }
    }
}