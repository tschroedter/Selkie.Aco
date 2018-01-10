using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core2.Selkie.Aco.Common;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Optimizers;
using Core2.Selkie.Common.Interfaces;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;

namespace Core2.Selkie.Aco.Trails.Tests.Optimizers
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class TwoOptSimpleTests
    {
        [SetUp]
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();
            m_Tracker = Substitute.For <IPheromonesTracker>();

            int[][] costMatrix =
            {
                new[]
                {
                    1,
                    1,
                    9,
                    9
                },
                new[]
                {
                    1,
                    9,
                    1,
                    9
                },
                new[]
                {
                    9,
                    1,
                    9,
                    1
                },
                new[]
                {
                    9,
                    9,
                    1,
                    1
                }
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
        }

        private IDistanceGraph m_Graph;
        private TwoOptSimple m_Optimizer;
        private IRandom m_Random;
        private IPheromonesTracker m_Tracker;

        [NotNull]
        private FixedTrailBuilder CreateFixedTrailBuilder([NotNull] IEnumerable <int> trail)
        {
            var unknown = Substitute.For <IChromosome>();
            unknown.IsUnknown.Returns(true);

            var original = new FixedTrailBuilder(m_Random,
                                                 unknown,
                                                 m_Tracker,
                                                 m_Graph,
                                                 m_Optimizer,
                                                 trail);
            return original;
        }

        [Test]
        public void SwapTest()
        {
            int[] trail =
            {
                0,
                1,
                2,
                3
            };

            int[] expected =
            {
                1,
                0,
                2,
                3
            };

            TwoOptSimple.Swap(trail,
                              0,
                              1);

            Assert.True(expected.SequenceEqual(trail));
        }

        [Test]
        public void TwoOptForAlreadyBestLengthTest()
        {
            IEnumerable <int> trail = new[]
                                      {
                                          0,
                                          1,
                                          2,
                                          3
                                      };

            FixedTrailBuilder original = CreateFixedTrailBuilder(trail);

            trail = m_Optimizer.Optimize(trail);

            FixedTrailBuilder actual = CreateFixedTrailBuilder(trail);

            Assert.AreEqual(original.Length,
                            actual.Length);
        }

        [Test]
        public void TwoOptForAlreadyBestTest()
        {
            int[] trail =
            {
                0,
                1,
                2,
                3
            };

            int[] expected =
            {
                0,
                1,
                2,
                3
            };
            IEnumerable <int> actual = m_Optimizer.Optimize(trail);

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void TwoOptForCaseEmptyTest()
        {
            var trail = new int[0];

            var expected = new int[0];
            IEnumerable <int> actual = m_Optimizer.Optimize(trail);

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void TwoOptForCaseOneLengthTest()
        {
            IEnumerable <int> trail = new[]
                                      {
                                          0,
                                          3,
                                          1,
                                          2
                                      };
            FixedTrailBuilder original = CreateFixedTrailBuilder(trail);

            trail = m_Optimizer.Optimize(trail);

            FixedTrailBuilder actual = CreateFixedTrailBuilder(trail);

            Assert.True(actual.Length < original.Length,
                        "Expected: " + actual.Length + " < " + original.Length);
        }

        [Test]
        public void TwoOptForCaseOneTest()
        {
            int[] trail =
            {
                0,
                3,
                1,
                2
            };

            int[] expected =
            {
                0,
                1,
                2,
                3
            };
            IEnumerable <int> actual = m_Optimizer.Optimize(trail);

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void TwoOptForCaseTwoLengthTest()
        {
            IEnumerable <int> trail = new[]
                                      {
                                          3,
                                          0,
                                          2,
                                          1
                                      };

            FixedTrailBuilder original = CreateFixedTrailBuilder(trail);

            trail = m_Optimizer.Optimize(trail);

            FixedTrailBuilder actual = CreateFixedTrailBuilder(trail);

            Assert.True(actual.Length < original.Length,
                        "Expected: " + actual.Length + " < " + original.Length);
        }

        [Test]
        public void TwoOptForCaseTwoTest()
        {
            int[] trail =
            {
                3,
                0,
                2,
                1
            };

            int[] expected =
            {
                3,
                2,
                1,
                0
            };
            IEnumerable <int> actual = m_Optimizer.Optimize(trail);

            Assert.True(expected.SequenceEqual(actual));
        }
    }
}