using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Common;
using Selkie.Aco.Trails;
using Selkie.Common;
using Selkie.NUnit.Extensions;

namespace Selkie.Aco.Anthill.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class NaturalSelectionTests
    {
        [SetUp]
        public void Setup()
        {
            m_Random = Substitute.For <IRandom>();
            m_Queen = Substitute.For <IQueen>();
            m_TrailHistory = Substitute.For <ITrailHistory>();

            m_Selection = new NaturalSelection(m_Random,
                                               m_TrailHistory,
                                               m_Queen);
        }

        private IQueen m_Queen;
        private IRandom m_Random;
        private NaturalSelection m_Selection;
        private ITrailHistory m_TrailHistory;

        [NotNull]
        private ITrailHistory CreateTrailHistoryWithThreeElements()
        {
            int[] lengths =
            {
                3,
                2,
                1
            };
            ITrailInformation one = CreateTrailInformationOne();
            ITrailInformation two = CreateTrailInformationTwo();
            ITrailInformation three = CreateTrailInformationThree();
            ITrailInformation[] trailInformation =
            {
                one,
                two,
                three
            };

            var trailHistory = Substitute.For <ITrailHistory>();
            trailHistory.Lengths.Returns(lengths);
            trailHistory [ 1 ].Returns(new[]
                                       {
                                           one
                                       });
            trailHistory [ 2 ].Returns(new[]
                                       {
                                           two
                                       });
            trailHistory [ 3 ].Returns(new[]
                                       {
                                           three
                                       });
            trailHistory.Information.Returns(trailInformation);

            return trailHistory;
        }

        [NotNull]
        private ITrailHistory CreateTrailHistoryWithOneElement()
        {
            int[] lengths =
            {
                1
            };
            ITrailInformation one = CreateTrailInformationOne();
            ITrailInformation[] trailInformations =
            {
                one
            };

            var trailHistory = Substitute.For <ITrailHistory>();
            trailHistory.Lengths.Returns(lengths);
            trailHistory [ 1 ].Returns(trailInformations);
            trailHistory.Information.Returns(trailInformations);

            return trailHistory;
        }

        [NotNull]
        private ITrailHistory CreateTrailHistoryWithTwoElementsSameChromosomes()
        {
            int[] lengths =
            {
                2,
                1
            };
            ITrailInformation one = CreateTrailInformationOne();
            ITrailInformation[] trailInformations =
            {
                one,
                one
            };

            var trailHistory = Substitute.For <ITrailHistory>();
            trailHistory.Lengths.Returns(lengths);
            trailHistory.Information.Returns(trailInformations);
            trailHistory [ 1 ].Returns(new[]
                                       {
                                           one
                                       });
            trailHistory [ 2 ].Returns(new[]
                                       {
                                           one
                                       });
            trailHistory.Information.Returns(trailInformations);

            return trailHistory;
        }

        [NotNull]
        private ITrailInformation CreateTrailInformationOne()
        {
            var settings = Substitute.For <ISettings>();
            settings.Alpha.Returns(1.0);
            settings.Beta.Returns(2.0);
            settings.Gamma.Returns(3.0);

            var trailInformation = Substitute.For <ITrailInformation>();
            trailInformation.Settings.Returns(settings);

            return trailInformation;
        }

        [NotNull]
        private ITrailInformation CreateTrailInformationTwo()
        {
            var settings = Substitute.For <ISettings>();
            settings.Alpha.Returns(10.0);
            settings.Beta.Returns(20.0);
            settings.Gamma.Returns(30.0);

            var trailInformation = Substitute.For <ITrailInformation>();
            trailInformation.Settings.Returns(settings);

            return trailInformation;
        }

        [NotNull]
        private ITrailInformation CreateTrailInformationThree()
        {
            var settings = Substitute.For <ISettings>();
            settings.Alpha.Returns(100.0);
            settings.Beta.Returns(200.0);
            settings.Gamma.Returns(300.0);

            var trailInformation = Substitute.For <ITrailInformation>();
            trailInformation.Settings.Returns(settings);

            return trailInformation;
        }

        [Test]
        public void FindBestChromosomePairForOneElementForFemaleTest()
        {
            ITrailHistory trailHistory = CreateTrailHistoryWithOneElement();

            var selection = new NaturalSelection(m_Random,
                                                 trailHistory,
                                                 m_Queen);

            IChromosome actual = selection.FindBestChromosomePair().Item2;

            NUnitHelper.AssertIsEquivalent(1.0,
                                           actual.Alpha,
                                           "Alpha");
            NUnitHelper.AssertIsEquivalent(2.0,
                                           actual.Beta,
                                           "Beta");
            NUnitHelper.AssertIsEquivalent(3.0,
                                           actual.Gamma,
                                           "Gamma");
        }

        [Test]
        public void FindBestChromosomePairForOneElementForMaleTest()
        {
            ITrailHistory trailHistory = CreateTrailHistoryWithOneElement();

            var selection = new NaturalSelection(m_Random,
                                                 trailHistory,
                                                 m_Queen);

            IChromosome actual = selection.FindBestChromosomePair().Item1;

            NUnitHelper.AssertIsEquivalent(1.0,
                                           actual.Alpha,
                                           "Alpha");
            NUnitHelper.AssertIsEquivalent(2.0,
                                           actual.Beta,
                                           "Beta");
            NUnitHelper.AssertIsEquivalent(3.0,
                                           actual.Gamma,
                                           "Gamma");
        }

        [Test]
        public void FindBestChromosomePairForThreeElementsForFemaleTest()
        {
            ITrailHistory trailHistory = CreateTrailHistoryWithThreeElements();

            var selection = new NaturalSelection(m_Random,
                                                 trailHistory,
                                                 m_Queen);

            IChromosome actual = selection.FindBestChromosomePair().Item2;

            NUnitHelper.AssertIsEquivalent(10.0,
                                           actual.Alpha,
                                           "Alpha");
            NUnitHelper.AssertIsEquivalent(20.0,
                                           actual.Beta,
                                           "Beta");
            NUnitHelper.AssertIsEquivalent(30.0,
                                           actual.Gamma,
                                           "Gamma");
        }

        [Test]
        public void FindBestChromosomePairForThreeElementsForMaleTest()
        {
            ITrailHistory trailHistory = CreateTrailHistoryWithThreeElements();

            var selection = new NaturalSelection(m_Random,
                                                 trailHistory,
                                                 m_Queen);

            IChromosome actual = selection.FindBestChromosomePair().Item1;

            NUnitHelper.AssertIsEquivalent(1.0,
                                           actual.Alpha,
                                           "Alpha");
            NUnitHelper.AssertIsEquivalent(2.0,
                                           actual.Beta,
                                           "Beta");
            NUnitHelper.AssertIsEquivalent(3.0,
                                           actual.Gamma,
                                           "Gamma");
        }

        [Test]
        public void QueenDefaultTest()
        {
            Assert.AreEqual(m_Queen,
                            m_Selection.Queen);
        }

        [Test]
        public void SelectCallsRandomSelectionForTrailHistoryCountOneTest()
        {
            ITrailHistory trailHistory = CreateTrailHistoryWithOneElement();

            var selection = new NaturalSelection(m_Random,
                                                 trailHistory,
                                                 m_Queen);

            selection.DoSelection();

            m_Queen.Received(1).RandomSelection();
        }

        [Test]
        public void SelectCallsRandomSelectionForTrailHistoryCountTwoAndChromosomeSameTest()
        {
            ITrailHistory trailHistory = CreateTrailHistoryWithTwoElementsSameChromosomes();

            var selection = new NaturalSelection(m_Random,
                                                 trailHistory,
                                                 m_Queen);

            selection.DoSelection();

            m_Queen.Received(1).RandomSelection();
        }

        [Test]
        public void SelectCallsRandomSelectionForTrailHistoryCountZeroTest()
        {
            m_Selection.DoSelection();

            m_Queen.Received(1).RandomSelection();
        }

        [Test]
        public void SelectrForThreeElementsCallsNaturalSelectionTest()
        {
            ITrailHistory trailHistory = CreateTrailHistoryWithThreeElements();

            var selection = new NaturalSelection(m_Random,
                                                 trailHistory,
                                                 m_Queen);

            selection.DoSelection();

            m_Queen.Received(1).NaturalSelection(Arg.Any <IChromosome>(),
                                                 Arg.Any <IChromosome>());
        }

        [Test]
        public void SettingsToChromosomeTest()
        {
            var settings = Substitute.For <ISettings>();
            settings.Alpha.Returns(1.0);
            settings.Beta.Returns(2.0);
            settings.Gamma.Returns(3.0);

            IChromosome actual = m_Selection.SettingsToChromosome(settings);

            Assert.AreEqual(settings.Alpha,
                            actual.Alpha,
                            "Alpha");
            Assert.AreEqual(settings.Beta,
                            actual.Beta,
                            "Beta");
            Assert.AreEqual(settings.Gamma,
                            actual.Gamma,
                            "Gamma");
        }

        [Test]
        public void TrailHistoryDefaultTest()
        {
            Assert.AreEqual(m_TrailHistory,
                            m_Selection.TrailHistory);
        }
    }
}