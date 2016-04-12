using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NUnit.Framework;
using Selkie.Aco.Common;

namespace Selkie.Aco.Ants.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class NearestNeighboursTests
    {
        [SetUp]
        public void Setup()
        {
            m_Costmatrix = new[]
                           {
                               new[]
                               {
                                   3,
                                   2,
                                   1,
                                   0
                               },
                               new[]
                               {
                                   2,
                                   4,
                                   6,
                                   5
                               },
                               new[]
                               {
                                   9,
                                   6,
                                   7,
                                   8
                               },
                               new[]
                               {
                                   1,
                                   2,
                                   3,
                                   5
                               }
                           };

            m_Neighbours = new NearestNeighbours();
            m_Neighbours.Initialize(m_Costmatrix);
        }

        private int[][] m_Costmatrix;
        private NearestNeighbours m_Neighbours;

        [Test]
        public void CalculateNearestNeighboursReturnsArrayLengthTest()
        {
            int[][] actual = NearestNeighbours.CalculateNearestNeighbours(m_Costmatrix);

            Assert.AreEqual(4,
                            actual.GetLength(0));
        }

        [Test]
        public void CalculateNearestNeighboursReturnsArrayValuesForOneTest()
        {
            int[] expected =
            {
                0,
                3,
                2
            };
            int[][] all = NearestNeighbours.CalculateNearestNeighbours(m_Costmatrix);
            int[] actual = all [ 1 ];

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void CalculateNearestNeighboursReturnsArrayValuesForThreeTest()
        {
            int[] expected =
            {
                0,
                1,
                2
            };
            int[][] all = NearestNeighbours.CalculateNearestNeighbours(m_Costmatrix);
            int[] actual = all [ 3 ];

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void CalculateNearestNeighboursReturnsArrayValuesForTwoTest()
        {
            int[] expected =
            {
                1,
                3,
                0
            };
            int[][] all = NearestNeighbours.CalculateNearestNeighbours(m_Costmatrix);
            int[] actual = all [ 2 ];

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void CalculateNearestNeighboursReturnsArrayValuesForZeroTest()
        {
            int[] expected =
            {
                3,
                2,
                1
            };
            int[][] all = NearestNeighbours.CalculateNearestNeighbours(m_Costmatrix);
            int[] actual = all [ 0 ];

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void GetNeighboursForOneTest()
        {
            int[] expected =
            {
                0,
                3,
                2
            };
            IEnumerable <int> actual = m_Neighbours.GetNeighbours(1);

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void GetNeighboursForThreeTest()
        {
            int[] expected =
            {
                0,
                1,
                2
            };
            IEnumerable <int> actual = m_Neighbours.GetNeighbours(3);

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void GetNeighboursForTwoTest()
        {
            int[] expected =
            {
                1,
                3,
                0
            };
            IEnumerable <int> actual = m_Neighbours.GetNeighbours(2);

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void GetNeighboursForZeroTest()
        {
            int[] expected =
            {
                3,
                2,
                1
            };
            IEnumerable <int> actual = m_Neighbours.GetNeighbours(0);

            Assert.True(expected.SequenceEqual(actual));
        }

        [Test]
        public void IsUnknownReturnsFalseForKnownTest()
        {
            Assert.False(m_Neighbours.IsUnknown);
        }

        [Test]
        public void IsUnknownReturnsTrueForUnknownTest()
        {
            INearestNeighbours neighbhours = NearestNeighbours.Unknown;

            Assert.True(neighbhours.IsUnknown);
        }

        [Test]
        public void ToStringReturnsStringTest()
        {
            const string expected = "[0] 3, 2, 1\r\n" +
                                    "[1] 0, 3, 2\r\n" +
                                    "[2] 1, 3, 0\r\n" +
                                    "[3] 0, 1, 2\r\n";

            string actual = m_Neighbours.ToString();

            Assert.True(string.Compare(expected,
                                       actual,
                                       StringComparison.InvariantCulture) == 0);
        }

        [Test]
        public void UnknownGetNeighboursReturnsEmptyTest()
        {
            INearestNeighbours neighbhours = NearestNeighbours.Unknown;

            IEnumerable <int> actual = neighbhours.GetNeighbours(1);

            Assert.AreEqual(0,
                            actual.Count());
        }

        [Test]
        public void UnknownReturnsTrueForUnknownTest()
        {
            INearestNeighbours neighbhours = NearestNeighbours.Unknown;

            Assert.True(neighbhours.IsUnknown);
        }
    }
}