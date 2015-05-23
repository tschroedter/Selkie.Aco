using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails.Optimizers;
using Selkie.Common;

namespace Selkie.Aco.Trails.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    // ReSharper disable once ClassTooBig
    internal sealed class RandomTrailBuilderTests
    {
        [SetUp]
        // ReSharper disable once MethodTooLong
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();
            m_CostMatrix = CreateCostMatrix();

            m_Graph = Substitute.For <IDistanceGraph>();
            m_Graph.GetCost(Arg.Any <int>(),
                            Arg.Any <int>()).ReturnsForAnyArgs(GetCost());

            m_Graph.NumberOfNodes.Returns(4);
            m_Graph.GetCost(0,
                            1).Returns(2);
            m_Graph.GetCost(1,
                            2).Returns(3);
            m_Graph.MinimumDistance.Returns(1.0);

            m_Tracker = Substitute.For <IPheromonesTracker>();

            m_Optimizer = new TwoOptSimple
                          {
                              DistanceGraph = m_Graph
                          };

            var unknown = Substitute.For <IChromosome>();
            unknown.IsUnknown.Returns(true);

            m_TrailBuilder = new RandomTrailBuilder(m_Random,
                                                    unknown,
                                                    m_Tracker,
                                                    m_Graph,
                                                    m_Optimizer);
        }

        private Func <CallInfo, int> GetCost()
        {
            return x =>
                   {
                       int value = m_CostMatrix [ ( int ) x [ 0 ] ] [ ( int ) x [ 1 ] ];

                       return value;
                   };
        }

        [NotNull]
        private int[][] CreateCostMatrix()
        {
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

            return costMatrix;
        }

        private RandomTrailBuilder m_TrailBuilder;
        private int[][] m_CostMatrix;
        private IDistanceGraph m_Graph;
        private IPheromonesTracker m_Tracker;
        private IOptimizer m_Optimizer;
        private IRandom m_Random;

        [Test]
        public void BuildTrailTest()
        {
            m_TrailBuilder.BuildTrail(1);

            int[] actual = m_TrailBuilder.Trail.ToArray();

            Assert.True(actual [ 0 ] == 1,
                        "1");
            Assert.AreEqual(2,
                            actual.Length);
        }

        [Test]
        public void BuildTrailThrowsForStartBiggerNumberOfNodesTest()
        {
            Assert.Throws <ArgumentException>(() => m_TrailBuilder.BuildTrail(1000));
        }

        [Test]
        public void BuildTrailThrowsForStartLessZeroTest()
        {
            Assert.Throws <ArgumentException>(() => m_TrailBuilder.BuildTrail(-1));
        }

        [Test]
        public void CloneTest()
        {
            var trailBuilderFactory = Substitute.For <ITrailBuilderFactory>();
            var chromosomeFactory = Substitute.For <IChromosomeFactory>();

            m_TrailBuilder.Clone(trailBuilderFactory,
                                 chromosomeFactory);

            trailBuilderFactory.ReceivedWithAnyArgs().Create <IRandomTrailBuilder>(Arg.Any <IChromosome>(),
                                                                                   Arg.Any <IPheromonesTracker>(),
                                                                                   Arg.Any <IDistanceGraph>(),
                                                                                   Arg.Any <IOptimizer>(),
                                                                                   Arg.Any <IEnumerable <int>>());
        }

        [Test]
        public void CreateTest()
        {
            int[] actual = RandomTrailBuilder.Create(3);

            Assert.AreEqual(0,
                            actual [ 0 ],
                            "[0]");
            Assert.AreEqual(1,
                            actual [ 1 ],
                            "[1]");
            Assert.AreEqual(2,
                            actual [ 2 ],
                            "[2]");
        }

        [Test]
        public void CreateTrailForNumberOfNodesIsOddTest()
        {
            Assert.Throws <ArgumentException>(() => m_TrailBuilder.CreateTrail(1,
                                                                               3));
        }

        [Test]
        public void CreateTrailForNumberOfNodesIsOneTest()
        {
            Assert.Throws <ArgumentException>(() => m_TrailBuilder.CreateTrail(1,
                                                                               1));
        }

        [Test]
        public void CreateTrailForNumberOfNodesIsZeroTest()
        {
            Assert.Throws <ArgumentException>(() => m_TrailBuilder.CreateTrail(1,
                                                                               0));
        }

        [Test]
        public void CreateTrailTest()
        {
            int[] actual = m_TrailBuilder.CreateTrail(1,
                                                      4);

            Assert.True(actual [ 0 ] == 1,
                        "1");
            Assert.AreEqual(2,
                            actual.Length);
        }

        [Test]
        public void IndexOfNodeTest()
        {
            var unknown = Substitute.For <IChromosome>();
            unknown.IsUnknown.Returns(true);

            int[] trail =
            {
                1,
                2,
                0
            };

            var builder = new RandomTrailBuilder(m_Random,
                                                 unknown,
                                                 m_Tracker,
                                                 m_Graph,
                                                 m_Optimizer,
                                                 trail);

            int actual = builder.IndexOfTarget(0);

            Assert.AreEqual(2,
                            actual);
        }

        [Test]
        public void IsUnknownReturnsFalseTest()
        {
            Assert.False(m_TrailBuilder.IsUnknown);
        }

        [Test]
        public void LengthCallsGraphLengthTest()
        {
            var trail = new int[0];

            m_TrailBuilder.CalculateLength(trail);

            m_Graph.Received().Length(trail);
        }

        [Test]
        public void MoveNodeToStartForNodeZeroTest()
        {
            var unknown = Substitute.For <IChromosome>();
            unknown.IsUnknown.Returns(true);

            int[] trail =
            {
                1,
                2,
                0
            };

            var builder = new RandomTrailBuilder(m_Random,
                                                 unknown,
                                                 m_Tracker,
                                                 m_Graph,
                                                 m_Optimizer,
                                                 trail);

            builder.MoveNodeToStart(trail,
                                    0);

            Assert.AreEqual(0,
                            trail [ 0 ],
                            "Node 0 expected at index 0!");
        }

        [Test]
        public void RandomizeTest()
        {
            int[] trail =
            {
                0,
                1,
                2
            };

            m_TrailBuilder.Randomize(trail,
                                     3);

            Assert.True(trail.Any(x => x == 0),
                        "0");
            Assert.True(trail.Any(x => x == 1),
                        "1");
            Assert.True(trail.Any(x => x == 2),
                        "2");
        }

        [Test]
        public void RemoveReverseNodesForFourTest()
        {
            int[] trail =
            {
                0,
                1,
                2,
                3
            };

            int[] actual = RandomTrailBuilder.RemoveReverseNodes(trail,
                                                                 2);

            Assert.AreEqual(2,
                            actual.Length,
                            "Length");
            Assert.AreEqual(0,
                            actual [ 0 ],
                            "actual[0]");
            Assert.AreEqual(2,
                            actual [ 1 ],
                            "actual[1]");
        }

        [Test]
        public void RemoveReverseNodesForStartNodeAfterReverseTest()
        {
            int[] trail =
            {
                5,
                6,
                1,
                0,
                4,
                3,
                7,
                2
            };

            var excpected = new List <int>
                            {
                                5,
                                6,
                                1,
                                2
                            };
            int[] actual = RandomTrailBuilder.RemoveReverseNodes(trail,
                                                                 2);

            Assert.AreEqual(4,
                            actual.Length,
                            "Length");
            Assert.True(excpected.SequenceEqual(actual));
        }

        [Test]
        public void RemoveReverseNodesForStartNodeBeforeReverseTest()
        {
            int[] trail =
            {
                5,
                6,
                1,
                0,
                4,
                2,
                7,
                3
            };

            var excpected = new List <int>
                            {
                                5,
                                6,
                                1,
                                2
                            };
            int[] actual = RandomTrailBuilder.RemoveReverseNodes(trail,
                                                                 2);

            Assert.AreEqual(4,
                            actual.Length,
                            "Length");
            Assert.True(excpected.SequenceEqual(actual));
        }

        [Test]
        public void RemoveReverseNodesForTwoTest()
        {
            int[] trail =
            {
                0,
                1
            };

            int[] actual = RandomTrailBuilder.RemoveReverseNodes(trail,
                                                                 0);

            Assert.AreEqual(1,
                            actual.Length,
                            "Length");
            Assert.AreEqual(0,
                            actual [ 0 ],
                            "actual[0]");
        }

        [Test]
        public void TypeTest()
        {
            Assert.AreEqual(typeof ( IRandomTrailBuilder ).Name,
                            m_TrailBuilder.Type);
        }
    }
}