using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Core2.Selkie.Aco.Trails.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class TrailHistoryTests
    {
        [SetUp]
        public void Setup()
        {
            m_BuilderOne = Substitute.For <ITrailBuilder>();
            m_BuilderOne.Trail.Returns(new[]
                                       {
                                           1,
                                           2,
                                           3
                                       });
            m_BuilderOne.Length.Returns(6);

            m_BuilderTwo = Substitute.For <ITrailBuilder>();
            m_BuilderTwo.Length.Returns(10);
            m_BuilderTwo.Trail.Returns(new[]
                                       {
                                           4,
                                           5,
                                           6
                                       });

            m_History = new TrailHistory();
        }

        private ITrailBuilder m_BuilderOne;
        private ITrailBuilder m_BuilderTwo;
        private TrailHistory m_History;

        [Test]
        public void AddOneAddsInformationTest()
        {
            var information1 = Substitute.For <ITrailInformation>();
            information1.TrailBuilder.Returns(m_BuilderOne);

            m_History.AddTrailInformation(information1);

            IEnumerable <ITrailInformation> actual = m_History [ 6 ];

            Assert.AreEqual(information1,
                            actual.First());
        }

        [Test]
        public void AddOneUpdatesCountTest()
        {
            var information1 = Substitute.For <ITrailInformation>();
            information1.TrailBuilder.Returns(m_BuilderOne);

            m_History.AddTrailInformation(information1);

            Assert.AreEqual(1,
                            m_History.Count,
                            "Count");
        }

        [Test]
        public void AddOneUpdatesInformationsCountTest()
        {
            var information1 = Substitute.For <ITrailInformation>();
            information1.TrailBuilder.Returns(m_BuilderOne);

            m_History.AddTrailInformation(information1);

            Assert.AreEqual(1,
                            m_History.Information.Count(),
                            "Information.Count");
        }

        [Test]
        public void AddOneUpdatesLengthsTest()
        {
            var information1 = Substitute.For <ITrailInformation>();
            information1.TrailBuilder.Returns(m_BuilderOne);

            m_History.AddTrailInformation(information1);

            Assert.AreEqual(1,
                            m_History.Lengths.Count());
        }

        [Test]
        public void AddTwoDifferentLengthUpdatesCountTest()
        {
            var information1 = Substitute.For <ITrailInformation>();
            information1.TrailBuilder.Returns(m_BuilderOne);

            var information2 = Substitute.For <ITrailInformation>();
            information2.TrailBuilder.Returns(m_BuilderTwo);

            m_History.AddTrailInformation(information1);
            m_History.AddTrailInformation(information2);

            Assert.AreEqual(2,
                            m_History.Count,
                            "Count");
            Assert.AreEqual(information1,
                            m_History [ 6 ].First(),
                            "[1]");
            Assert.AreEqual(information2,
                            m_History [ 10 ].First(),
                            "[2]");
        }

        [Test]
        public void AddTwoSameLengthIsIgnoredTest()
        {
            var information1 = Substitute.For <ITrailInformation>();
            information1.TrailBuilder.Returns(m_BuilderOne);

            var trailBuilder2 = Substitute.For <ITrailBuilder>();
            trailBuilder2.Trail.Returns(new[]
                                        {
                                            2,
                                            1,
                                            3
                                        });
            trailBuilder2.Length.Returns(6);

            var information2 = Substitute.For <ITrailInformation>();
            information2.TrailBuilder.Returns(trailBuilder2);

            m_History.AddTrailInformation(information1);
            m_History.AddTrailInformation(information2);

            Assert.AreEqual(1,
                            m_History.Count,
                            "Count");
            Assert.AreEqual(1,
                            m_History.Information.Count(),
                            "Information.Count");
        }

        [Test]
        public void AddUnknownIsIgnoredTest()
        {
            var unknown = Substitute.For <IUnknownTrailBuilder>();
            unknown.IsUnknown.Returns(true);

            var information1 = Substitute.For <ITrailInformation>();
            information1.TrailBuilder.Returns(unknown);

            m_History.AddTrailInformation(information1);

            Assert.AreEqual(0,
                            m_History.Count);
        }

        [Test]
        public void ClearTest()
        {
            var information1 = Substitute.For <ITrailInformation>();
            information1.TrailBuilder.Returns(m_BuilderOne);

            var information2 = Substitute.For <ITrailInformation>();
            information2.TrailBuilder.Returns(m_BuilderTwo);

            m_History.AddTrailInformation(information1);
            m_History.AddTrailInformation(information2);

            m_History.Clear();

            Assert.AreEqual(0,
                            m_History.Count,
                            "Count");
            Assert.AreEqual(0,
                            m_History.Information.Count(),
                            "Information.Count");
        }

        [Test]
        public void ConvertValuesToListTest()
        {
            var dictionary = new Dictionary <int, List <ITrailInformation>>();

            var information1 = Substitute.For <ITrailInformation>();
            var information2 = Substitute.For <ITrailInformation>();

            var list1 = new List <ITrailInformation>
                        {
                            information1
                        };
            var list2 = new List <ITrailInformation>
                        {
                            information2
                        };

            dictionary.Add(1,
                           list1);
            dictionary.Add(2,
                           list2);

            IEnumerable <ITrailInformation> trailInformations = TrailHistory.ConvertValuesToList(dictionary.Values);
            ITrailInformation[] actual = trailInformations.ToArray();

            Assert.AreEqual(2,
                            actual.Length,
                            "Length");
            Assert.AreEqual(information1,
                            actual [ 0 ],
                            "[0]");
            Assert.AreEqual(information2,
                            actual [ 1 ],
                            "[1]");
        }

        [Test]
        public void DefaultCountTest()
        {
            Assert.AreEqual(0,
                            m_History.Count);
        }

        [Test]
        public void DefaultInformationsTest()
        {
            Assert.AreEqual(0,
                            m_History.Information.Count());
        }

        [Test]
        public void DefaultLengthsTest()
        {
            Assert.AreEqual(0,
                            m_History.Lengths.Count());
        }

        [Test]
        public void IsKnownAlternativeReturnsFalseForUnknownTest()
        {
            Assert.False(m_History.IsKnownLength(1));
        }

        [Test]
        public void IsKnownAlternativeReturnsTrueForKnownTest()
        {
            var information1 = Substitute.For <ITrailInformation>();
            information1.TrailBuilder.Returns(m_BuilderOne);

            m_History.AddTrailInformation(information1);

            Assert.True(m_History.IsKnownLength(( int ) information1.TrailBuilder.Length));
        }

        [Test]
        public void IsKnownAlternativeReturnsTrueForZeroTest()
        {
            Assert.True(m_History.IsKnownLength(0));
        }

        [Test]
        public void ToStringTest()
        {
            var information1 = Substitute.For <ITrailInformation>();
            information1.TrailBuilder.Returns(m_BuilderOne);

            var information2 = Substitute.For <ITrailInformation>();
            information2.TrailBuilder.Returns(m_BuilderTwo);

            m_History.AddTrailInformation(information1);
            m_History.AddTrailInformation(information2);

            const string expected = "Length 6: \r\nSystem.Int32[]\r\nLength 10: \r\nSystem.Int32[]\r\n";
            string actual = m_History.ToString();

            Assert.AreEqual(expected,
                            actual);
        }
    }
}