using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails.Interfaces;
using Selkie.Aco.Trails.Optimizers;
using Selkie.Common.Interfaces;

namespace Selkie.Aco.Trails.Tests
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
        public void BuildTrail_ReturnsTrailStaringWithFixedNode_ForGivenStartNode()
        {
            // Arrange
            // Act
            m_TrailBuilder.BuildTrail(2);

            // Assert
            int[] actual = m_TrailBuilder.Trail.ToArray();

            Assert.AreEqual(2,
                            actual [ 0 ],
                            "Node 2 expected at index 0!");
        }

        [Test]
        public void BuildTrail_ReturnsTrailWithCorrectLength_ForGivenStartNode()
        {
            // Arrange
            m_TrailBuilder.BuildTrail(2);

            // Act
            int[] actual = m_TrailBuilder.Trail.ToArray();

            // Assert
            Assert.AreEqual(2,
                            actual.Length);
        }

        [Test]
        public void Clone_ReturnsInstance_WhenCalled()
        {
            // Arrange
            var trailBuilderFactory = Substitute.For <ITrailBuilderFactory>();
            var chromosomeFactory = Substitute.For <IChromosomeFactory>();

            // Act
            m_TrailBuilder.Clone(trailBuilderFactory,
                                 chromosomeFactory);

            // Assert
            trailBuilderFactory.ReceivedWithAnyArgs().Create <IRandomTrailBuilder>(Arg.Any <IChromosome>(),
                                                                                   Arg.Any <IPheromonesTracker>(),
                                                                                   Arg.Any <IDistanceGraph>(),
                                                                                   Arg.Any <IOptimizer>(),
                                                                                   Arg.Any <IEnumerable <int>>());
        }

        [Test]
        public void Create_ReturnsRandomTrail_WhenCalled()
        {
            // Arrange
            // Act
            int[] actual = RandomTrailBuilder.Create(3);

            // Assert
            Assert.AreEqual(3,
                            actual.Length,
                            "Length");
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
        public void IndexOfNode_ReturnsIndex_ForGivenNode()
        {
            // Arrange
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

            // Act
            int actual = builder.IndexOfTarget(0);

            // Assert
            Assert.AreEqual(2,
                            actual);
        }

        [Test]
        public void IsUnknown_ReturnsFalse_ForKnownInstance()
        {
            // Arrange
            // Act
            // Assert
            Assert.False(m_TrailBuilder.IsUnknown);
        }

        [Test]
        public void Length_CallsGraphLength_WhenCalled()
        {
            // Arrange
            var trail = new int[0];

            // Act
            m_TrailBuilder.CalculateLength(trail);

            // Assert
            m_Graph.Received().Length(trail);
        }

        [Test]
        public void MoveNodeToStartPosition_MoveNodeToStartPosition_ForGivenNode()
        {
            // Arrange
            int[] trail =
            {
                0,
                1,
                2
            };

            // Act
            int[] actual = m_TrailBuilder.MoveNodeToStartPosition(trail,
                                                                  2);

            // Assert
            Assert.AreEqual(2,
                            actual [ 0 ],
                            "Node 2 expected at index 0!");
        }

        [Test]
        public void Randomize_RandomizeTrail_ForGivenTrail()
        {
            // Arrange
            int[] trail =
            {
                0,
                1,
                2
            };

            // Act
            int[] actual = m_TrailBuilder.Randomize(trail,
                                                    3);

            // Assert
            Assert.True(actual.Any(x => x == 0),
                        "0");
            Assert.True(actual.Any(x => x == 1),
                        "1");
            Assert.True(actual.Any(x => x == 2),
                        "2");
        }

        [Test]
        public void RemoveReverseNodes_RemovesNodes_ForGivenTrail()
        {
            // Arrange
            int[] trail =
            {
                0,
                1,
                2,
                3
            };

            // Act
            int[] actual = RandomTrailBuilder.RemoveReverseNodes(trail,
                                                                 2);

            // Assert
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
        public void RemoveReverseNodes_RemovesNodes_ForStartNodeAfterReverset()
        {
            // Arrange
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

            // Act
            int[] actual = RandomTrailBuilder.RemoveReverseNodes(trail,
                                                                 2);

            // Assert
            Assert.AreEqual(4,
                            actual.Length,
                            "Length");
            Assert.True(excpected.SequenceEqual(actual));
        }

        [Test]
        public void RemoveReverseNodes_RemovesNodes_ForStartNodeBeforeReverse()
        {
            // Arrange
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
            // Act
            int[] actual = RandomTrailBuilder.RemoveReverseNodes(trail,
                                                                 2);

            // Assert
            Assert.AreEqual(4,
                            actual.Length,
                            "Length");
            Assert.True(excpected.SequenceEqual(actual));
        }

        [Test]
        public void RemoveReverseNodes_RemovesNodes_ForTrailOfTwo()
        {
            // Arrange
            int[] trail =
            {
                0,
                1
            };

            // Act
            int[] actual = RandomTrailBuilder.RemoveReverseNodes(trail,
                                                                 0);

            // Assert
            Assert.AreEqual(1,
                            actual.Length,
                            "Length");
            Assert.AreEqual(0,
                            actual [ 0 ],
                            "actual[0]");
        }

        [Test]
        public void Type_ReturnsString_WhenCalled()
        {
            // Arrange
            string expected = typeof( IRandomTrailBuilder ).Name;

            // Act
            string actual = m_TrailBuilder.Type;

            // Assert
            Assert.AreEqual(expected,
                            actual);
        }
    }
}