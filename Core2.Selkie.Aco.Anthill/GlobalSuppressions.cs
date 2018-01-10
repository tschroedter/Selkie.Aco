// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project. 
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc. 
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File". 
// You do not need to add suppressions to this file manually. 

using System.Diagnostics.CodeAnalysis;

[assembly :
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Aco",
        Scope = "namespace", Target = "Aco.Interfaces")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "Aco.Ants.NUnit")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "Aco.Trails.Optimizers")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Aco.NUnit")
]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "Aco.Trails.Optimizers.NUnit")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Scope = "type",
        Target = "Aco.Ants.ICandidateListAnt")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Scope = "type",
        Target = "Aco.Trails.Builders.ICandidateListBuilder")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Scope = "type", Target = "Aco.Ants.IFixedAnt")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Scope = "type",
        Target = "Aco.Trails.Builders.IFixedTrailBuilder")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Scope = "type",
        Target = "Aco.Trails.Builders.IRandomTrailBuilder")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Scope = "type", Target = "Aco.Ants.IStandardAnt")
]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Scope = "type",
        Target = "Aco.Trails.Builders.IStandardTrailBuilder")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Scope = "type",
        Target = "Aco.Trails.Optimizers.ITwoOptSimple")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Scope = "type", Target = "Aco.Ants.IUnknownAnt")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Scope = "type",
        Target = "Aco.Trails.Builders.IUnknownTrailBuilder")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Aco",
        Scope = "namespace", Target = "Aco.Ants.NUnit")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Aco",
        Scope = "namespace", Target = "Aco.Trails")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Aco",
        Scope = "namespace", Target = "Aco.Trails.Optimizers")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Aco",
        Scope = "namespace", Target = "Aco.Trails.Optimizers.NUnit")]
[assembly : SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Aco")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Aco",
        Scope = "namespace", Target = "Aco.Trails.Builders")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Aco",
        Scope = "namespace", Target = "Aco.NUnit")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Aco",
        Scope = "namespace", Target = "Aco.Ants")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Aco",
        Scope = "namespace", Target = "Aco")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.Trails.Builders.NUnit.UnknownTrailBuilderTests")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.Ants.NUnit.UnknownAntTests")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#DecideNumberOfCandidatesForZeroTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#EqualsOperatorReturnsTrueForSameTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#EqualsReturnsFalseForDifferentChromosomeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#EqualsReturnsFalseForDifferentTypeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#EqualsReturnsFalseForNullTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#EqualsReturnsFalseForObjectNullTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#EqualsReturnsFalseForWrongObjectTypeTest()")
]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#EqualsReturnsTrueForSameTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#EqualsReturnsTrueForSameValuesTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#GetHashCodeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#IsUnknownReturnsFalseTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#NextCityReturnsCityOneForOtherTwoVisitedAndForwardTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#NextCityReturnsCityOneForOtherTwoVisitedAndReverseExpectedTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#NextCityReturnsCityTwoForOtherTwoUnvisitedAndForwardExpectedTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#NextCityReturnsCityTwoForOtherTwoUnvisitedAndReverseExpectedTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#NextCityThrowsForNoCityLeftTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#NotEqualsOperatorReturnsTrueForSameTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#NumberOfCandidatesForLessThanZeroTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#NumberOfCandidatesForMoreThanNumberOfUniqueNodesTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#NumberOfCandidatesForNumberOfUniqueNodesTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#NumberOfCandidatesForValidValueTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#NumberOfCandidatesForZeroTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#NumberOfCandidatesTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#TypeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#DecideNumberOfCandidatesForMoreThanFourTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#DecideNumberOfCandidatesForOneToFourTest()")
]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#CloneTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#BuildTrailThrowsForStartLessZeroTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#BuildTrailThrowsForStartGreaterNumberOfGraphNodesTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#BuildTrailThrowsForStartEqualNumberOfGraphNodesTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#BuildTrailThrowsForStartBiggerNumberOfNodesTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#BuildTrailStartsWithTheGivenCityTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#BuildTrailLengthTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#BuildTrailDoesNotContainRelatedCitiesTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#Setup()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.Ants.NUnit.CandidateListAntTests")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.NUnit.BestTrailInformationTests")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BetaMax",
        Scope = "member", Target = "Aco.Ants.IChromosome.#BetaMaxValue")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "betaMax",
        Scope = "member",
        Target =
            "Aco.Ants.Chromosome.#.ctor(System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double)"
        )]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "betaMax",
        Scope = "member",
        Target =
            "Aco.Ants.Chromosome.#.ctor(System.Double,System.Double,System.Double,System.Double,System.Double,System.Double)"
        )]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Q", Scope = "member",
        Target = "Aco.Interfaces.IPheromonesTracker.#Q")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Q", Scope = "member",
        Target = "Aco.Interfaces.ISettings.#Q")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#Setup()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#AntTypeToStringForCandidateListTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#AntTypeToStringForFixedTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#AntTypeToStringForStandardTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#AntTypeToStringForUnknownTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#AntTypeToStringForUnknownTypeStandardTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#ChromosomeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#DefaultAlfaTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#DefaultBetaTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#DefaultChromosomeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#DefaultGammaTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#IdTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#StartNodeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#TrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.FixedTrailBuilderTests.#CloneTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.BestTrailFinderTests.#ClearTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.BestTrailFinderTests.#ClearTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestTrail",
        Scope = "member", Target = "Aco.NUnit.BestTrailFinderTests.#FindBestTrailForFirstIsBestTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.NUnit.BestTrailFinderTests.#FindBestTrailForFirstIsBestTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlternativeTrails",
        Scope = "member", Target = "Aco.NUnit.BestTrailFinderTests.#FindBestTrailForFirstIsBestTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.BestTrailFinderTests.#FindBestTrailForFirstIsBestTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestTrail",
        Scope = "member", Target = "Aco.NUnit.BestTrailFinderTests.#FindBestTrailForSecondIsBestTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.NUnit.BestTrailFinderTests.#FindBestTrailForSecondIsBestTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlternativeTrails",
        Scope = "member", Target = "Aco.NUnit.BestTrailFinderTests.#FindBestTrailForSecondIsBestTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.BestTrailFinderTests.#FindBestTrailForSecondIsBestTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestTrail",
        Scope = "member", Target = "Aco.NUnit.BestTrailFinderTests.#FindBestTrailForTwoBestTrailsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.NUnit.BestTrailFinderTests.#FindBestTrailForTwoBestTrailsTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlternativeTrails",
        Scope = "member", Target = "Aco.NUnit.BestTrailFinderTests.#FindBestTrailForTwoBestTrailsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.BestTrailFinderTests.#FindBestTrailForTwoBestTrailsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.NUnit.BestTrailFinderTests.#SettingsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.CandidateListAntTests.#TrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.False(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#BuildTrailDoesNotContainRelatedCitiesTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#DecideNumberOfCandidatesForMoreThanFourTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#DecideNumberOfCandidatesForMoreThanFourTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#ConstructorForThreeAntsAndThreeNodeTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#ConstructorForThreeAntsAndThreeNodeTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#ConstructorForTwoAntsAndTwoNodesTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#CostMatrixTwoPathsAlternativesTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#CostMatrixSimpleAlternativesTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#CostMatrixThreeLinesAlternativesTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#PheromonesTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.CrossoverTests.#MutationForAlphaTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.CrossoverTests.#MutationForBetaTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.CrossoverTests.#MutationForGammaTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Common.NUnitHelper.AssertIsEquivalent(System.Double,System.Double,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.CrossoverTests.#OffspringForAlphaTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Common.NUnitHelper.AssertIsEquivalent(System.Double,System.Double,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.CrossoverTests.#OffspringForBetaTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Common.NUnitHelper.AssertIsEquivalent(System.Double,System.Double,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.CrossoverTests.#OffspringForGammaTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#TrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.NUnit.BestTrailFinderTests.#ClearTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#DecideNumberOfCandidatesForOneToFourTest()")
]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#CloneAlphaBetaGammaTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#CloneMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorAlphaBetaGammaTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorMinMaxValueTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorRandomTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#RangeTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Castle.Core.Logging.ISelkieLogger.Info(System.String)", Scope = "member",
        Target = "Aco.Colony.#Start(System.Int32)")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Castle.Core.Logging.ISelkieLogger.Info(System.String)", Scope = "member",
        Target = "Aco.Colony.#LogResult()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#BestTrailsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#ConstructorForTwoAntsAndTwoNodesTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#CostMatrixAllSameAlternativesTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.DistanceGraphTests.#CalculateNearestNeighboursTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.DistanceGraphTests.#ConvertToArrayTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.DistanceGraphTests.#CostMatrixLengthTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.DistanceGraphTests.#CreateNearestNeighboursTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.NUnit.DistanceGraphTests.#DistanceTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.FixedAntTests.#CloneTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.FixedAntTests.#CloneTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.FixedAntTests.#ConstructorWithoutTrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.FixedAntTests.#ConstructorWithoutTrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.FixedAntTests.#TrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.FixedTrailBuilderTests.#ConstructorWithoutTrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.FixedTrailBuilderTests.#ConstructorWithoutTrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target =
            "Aco.NUnit.NUnitHelper.#IsEquivalent(System.Collections.Generic.IList`1<System.Double>,System.Collections.Generic.IList`1<System.Double>)"
        )]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider",
        MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)", Scope = "member",
        Target =
            "Aco.NUnit.NUnitHelper.#IsEquivalent(System.Collections.Generic.IList`1<System.Double>,System.Collections.Generic.IList`1<System.Double>)"
        )]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.IsTrue(System.Boolean,System.String)", Scope = "member",
        Target =
            "Aco.NUnit.NUnitHelper.#IsEquivalent(System.Collections.Generic.IList`1<System.Double>,System.Collections.Generic.IList`1<System.Double>)"
        )]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.NUnit.PheromonesTrackerTests.#ClearTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.NUnit.PheromonesTrackerTests.#ConstructorWithThreeParametersTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Aco.NUnit.NUnitHelper.AssertIsEquivalent(System.Double,System.Double,System.Double,System.String)",
        Scope = "member", Target = "Aco.NUnit.PheromonesTrackerTests.#ConstructorWithThreeParametersTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.NUnit.PheromonesTrackerTests.#CreateContentTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.PheromonesTrackerTests.#CreateLengthTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Aco.NUnit.NUnitHelper.AssertIsEquivalent(System.Double,System.Double,System.Double,System.String)",
        Scope = "member", Target = "Aco.NUnit.PheromonesTrackerTests.#MaximumValueTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Aco.NUnit.NUnitHelper.AssertIsEquivalent(System.Double,System.Double,System.Double,System.String)",
        Scope = "member", Target = "Aco.NUnit.PheromonesTrackerTests.#MinimumValueTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.NUnit.PheromonesTrackerTests.#PheromonesContentTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.PheromonesTrackerTests.#PheromonesLengthTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.PheromonesTrackerTests.#RandomizeTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Aco.NUnit.NUnitHelper.AssertIsEquivalent(System.Double,System.Double,System.Double,System.String)",
        Scope = "member",
        Target = "Aco.NUnit.PheromonesTrackerTests.#UpdateAllEdgeInTrailIsFalseForPheromonesEvaporateTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Aco.NUnit.NUnitHelper.AssertIsEquivalent(System.Double,System.Double,System.Double,System.String)",
        Scope = "member", Target = "Aco.NUnit.PheromonesTrackerTests.#UpdateAllEdgeInTrailIsFalseTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Aco.NUnit.NUnitHelper.AssertIsEquivalent(System.Double,System.Double,System.Double,System.String)",
        Scope = "member",
        Target = "Aco.NUnit.PheromonesTrackerTests.#UpdateAllEdgeInTrailIsTrueForPheromonesDoNotEvaporateTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Aco.NUnit.NUnitHelper.AssertIsEquivalent(System.Double,System.Double,System.Double,System.String)",
        Scope = "member", Target = "Aco.NUnit.PheromonesTrackerTests.#UpdateAllEdgeInTrailIsTrueTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#CreateAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#NaturalSelectionTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#NaturalSelectionTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#RandomSelectionTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreNotEqual(System.Double,System.Double,System.String)", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#RandomSelectionTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#UpdateChromosomesTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#UpdateChromosomesTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#BuildTrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#CreateTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#CreateTrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#MoveNodeToStartForNodeZeroTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#RandomizeTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#RemoveReverseNodesForFourTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#RemoveReverseNodesForStartNodeAfterReverseTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#RemoveReverseNodesForStartNodeBeforeReverseTest()")
]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#RemoveReverseNodesForTwoTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.NUnit.SettingsTests.#ToChromosomeTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.StandardAntTests.#CloneTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.StandardAntTests.#CloneTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.StandardAntTests.#TrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.False(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#BuildTrailDoesNotContainRelatedCitiesTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Trails.NUnit.TrailAlternativesTests.#AddTwoUnknownTrailsNodesTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Trails.NUnit.TrailAlternativesTests.#ConvertValuesToListTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.NUnit.TrailHistoryTests.#AddOneUpdatesCountTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.NUnit.TrailHistoryTests.#AddOneUpdatesInformationsCountTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.NUnit.TrailHistoryTests.#AddTwoDifferentLengthUpdatesCountTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Trails.NUnit.TrailHistoryTests.#AddTwoDifferentLengthUpdatesCountTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.NUnit.TrailHistoryTests.#AddTwoSameLengthIsIgnoredTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.NUnit.TrailHistoryTests.#ClearTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.NUnit.TrailHistoryTests.#ConvertValuesToListTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Trails.NUnit.TrailHistoryTests.#ConvertValuesToListTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Trails.Optimizers.NUnit.TwoOptSimpleTests.#TwoOptForCaseOneLengthTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Trails.Optimizers.NUnit.TwoOptSimpleTests.#TwoOptForCaseTwoLengthTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.UnknownAntTests.#CloneTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.UnknownAntTests.#CloneTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.UnknownTrailBuilderTests.#CloneTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.UnknownTrailBuilderTests.#CloneTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IsUnknown",
        Scope = "member", Target = "Aco.Trails.Builders.NUnit.UnknownTrailBuilderTests.#CloneTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Trails.Optimizers.NUnit.TwoOptSimpleTests.#SwapTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.Trails.NUnit.TrailInformationTests")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.Trails.NUnit.TrailHistoryTests")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.Trails.Optimizers.TwoOptSimple.#Optimize(System.Int32[])")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Trails.NUnit.TrailHistoryTests.#ConvertValuesToListTest()")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.Trails.TrailHistory.#AddAlternative(Aco.Trails.ITrailInformation)")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.Trails.NUnit.TrailAlternativesTests")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.Trails.TrailAlternatives.#AddAlternative(Aco.Ants.IAnt)")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#TypeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#Setup()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#BuildTrailDoesNotContainRelatedCitiesTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#BuildTrailLengthTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#BuildTrailStartsWithTheGivenCityTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#BuildTrailThrowsForStartBiggerNumberOfNodesTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#BuildTrailThrowsForStartLessZeroTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#CloneTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#EqualsOperatorReturnsTrueForSameTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#EqualsReturnsFalseForDifferentChromosomeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#EqualsReturnsFalseForDifferentTypeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#EqualsReturnsFalseForNullTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#EqualsReturnsFalseForObjectNullTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#EqualsReturnsFalseForWrongObjectTypeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#EqualsReturnsTrueForSameTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#EqualsReturnsTrueForSameValuesTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#GetHashCodeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#IsUnknownReturnsFalseTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#NextCityReturnsCityOneForOtherTwoVisitedAndForwardTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#NextCityReturnsCityOneForOtherTwoVisitedAndReverseExpectedTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#NextCityReturnsCityTwoForOtherTwoUnvisitedAndForwardExpectedTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#NextCityReturnsCityTwoForOtherTwoUnvisitedAndReverseExpectedTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#NextCityThrowsForNoCityLeftTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#NotEqualsOperatorReturnsTrueForSameTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.Ants.NUnit.StandardAntTests")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.NUnit.SettingsTests")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.Settings.#.ctor(Aco.Ants.IAnt,Aco.Interfaces.IPheromonesTracker)")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member",
        Target = "Aco.Settings.#.ctor(Aco.Ants.IAnt,Aco.Interfaces.IPheromonesTracker)")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.NUnit.TrailAlternativesTests.#CreateTrailOne()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.NUnit.TrailAlternativesTests.#CreateTrailTwo()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#RandomizeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#CreateTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "NumberOfAnts",
        Scope = "member", Target = "Aco.NUnit.QueenTests.#UpdateChromosomesTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "NumberOfAnts",
        Scope = "member", Target = "Aco.NUnit.QueenTests.#RandomSelectionTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "NumberOfAnts",
        Scope = "member", Target = "Aco.NUnit.QueenTests.#NaturalSelectionTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.NUnit.QueenTests")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#CreatePheromones(System.Int32)")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "2", Scope = "member",
        Target =
            "Aco.Queen.#.ctor(Castle.Core.Logging.ISelkieLogger,Aco.Ants.IAntFactory,Aco.IBestTrailFinderFactory,Aco.Interfaces.IDistanceGraph,Aco.Interfaces.IPheromonesTracker,Aco.Trails.Optimizers.IOptimizer,Aco.Interfaces.ICrossover)"
        )]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GetLength",
        Scope = "member", Target = "Aco.NUnit.PheromonesTrackerTests.#PheromonesLengthTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "MinimumValue",
        Scope = "member", Target = "Aco.NUnit.PheromonesTrackerTests.#ConstructorWithThreeParametersTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "MaximumValue",
        Scope = "member", Target = "Aco.NUnit.PheromonesTrackerTests.#ConstructorWithThreeParametersTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GetLength",
        Scope = "member", Target = "Aco.NUnit.PheromonesTrackerTests.#CreateLengthTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "MaximumValue",
        Scope = "member", Target = "Aco.NUnit.PheromonesTrackerTests.#MaximumValueTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "MinimumValue",
        Scope = "member", Target = "Aco.NUnit.PheromonesTrackerTests.#MinimumValueTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.NUnit.PheromonesTrackerTests")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.NUnit.PheromonesTrackerTests.#CalculateAverageValueTest()")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.PheromonesTracker.#Initialize(Aco.Ants.IAnt[])")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.PheromonesTracker.#.ctor(Aco.Interfaces.IDistanceGraph,System.Double,System.Double)")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.PheromonesTracker.#.ctor(Aco.Interfaces.IDistanceGraph)")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "q", Scope = "member",
        Target = "Aco.PheromonesTracker.#.ctor(Aco.Interfaces.IDistanceGraph,System.Double,System.Double)")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member",
        Target =
            "Aco.NUnit.NUnitHelper.#IsEquivalent(System.Collections.Generic.IList`1<System.Double>,System.Collections.Generic.IList`1<System.Double>)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target =
            "Aco.NUnit.NUnitHelper.#IsEquivalent(System.Collections.Generic.IList`1<System.Double>,System.Collections.Generic.IList`1<System.Double>)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target =
            "Aco.Installers.#Install(Castle.Windsor.IWindsorContainer,Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore)"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.Trails.Builders.NUnit.FixedTrailBuilderTests")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "TrailBuilder",
        Scope = "member", Target = "Aco.Ants.NUnit.FixedAntTests.#ConstructorWithoutTrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.Ants.NUnit.FixedAntTests")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neighbours",
        Scope = "member", Target = "Aco.NUnit.DistanceGraphTests.#CreateNearestNeighboursTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GetLength",
        Scope = "member", Target = "Aco.NUnit.DistanceGraphTests.#CostMatrixLengthTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neighbours",
        Scope = "member", Target = "Aco.NUnit.DistanceGraphTests.#CalculateNearestNeighboursTest()")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.DistanceGraph.#Length(System.Int32[])")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.DistanceGraph.#Length(Aco.Trails.Builders.ITrailBuilder)")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.DistanceGraph.#.ctor(System.Int32[][],System.Int32[])")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.Ants.NUnit.CrossoverTests")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GetLength",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#PheromonesTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "NumberOfAnts",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#ConstructorForThreeAntsAndThreeNodeTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "NumberOfNodes",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#ConstructorForThreeAntsAndThreeNodeTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestTrail",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#ConstructorForThreeAntsAndThreeNodeTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlternativeTrails",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#ConstructorForThreeAntsAndThreeNodeTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "NumberOfAnts",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#ConstructorForTwoAntsAndTwoNodesTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "NumberOfNodes",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#ConstructorForTwoAntsAndTwoNodesTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestTrail",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#ConstructorForTwoAntsAndTwoNodesTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlternativeTrails",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#ConstructorForTwoAntsAndTwoNodesTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestTrailBuilder",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#CostMatrixAllSameAlternativesTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlternativeTrails",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#CostMatrixAllSameAlternativesTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestTrailBuilder",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#CostMatrixTwoPathsAlternativesTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlternativeTrails",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#CostMatrixTwoPathsAlternativesTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestTrailBuilder",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#CostMatrixSimpleAlternativesTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlternativeTrails",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#CostMatrixSimpleAlternativesTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestTrailBuilder",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#CostMatrixThreeLinesAlternativesTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlternativeTrails",
        Scope = "member", Target = "Aco.NUnit.ColonyTests.#CostMatrixThreeLinesAlternativesTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#CreateCostPerLine(System.Int32[,])")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.NUnit.ColonyTests")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member",
        Target =
            "Aco.Colony.#.ctor(Castle.Core.Logging.ISelkieLogger,Aco.IQueenFactory,Aco.Trails.Builders.ITrailBuilderFactory,Aco.Interfaces.IPheromonesTracker,Aco.Interfaces.IDistanceGraph,Aco.Trails.Optimizers.IOptimizer,Aco.Trails.ITrailHistory)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "5", Scope = "member",
        Target =
            "Aco.Colony.#.ctor(Castle.Core.Logging.ISelkieLogger,Aco.IQueenFactory,Aco.Trails.Builders.ITrailBuilderFactory,Aco.Interfaces.IPheromonesTracker,Aco.Interfaces.IDistanceGraph,Aco.Trails.Optimizers.IOptimizer,Aco.Trails.ITrailHistory)"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#ToStringTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#Setup()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#CloneAlphaBetaGammaTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#CloneMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlphaMinValue",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#CloneMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlphaMaxValue",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#CloneMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BetaMinValue",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#CloneMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BetaMaxValue",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#CloneMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GammaMinValue",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#CloneMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GammaMaxValue",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#CloneMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorAlphaBetaGammaTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlphaMinValue",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlphaMaxValue",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BetaMinValue",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BetaMaxValue",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GammaMinValue",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GammaMaxValue",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorMinMaxTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorMinMaxValueTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#ConstructorRandomTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#EqualsForOtherIsNullTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#EqualsForSameTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#EqualsForSameValuesTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#IsUnknownReturnsFalseTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#IsUnknownReturnsTrueTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Ants.NUnit.ChromosomeTests.#RangeTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AlphaRange",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#RangeTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BetaRange",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#RangeTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GammaRange",
        Scope = "member", Target = "Aco.Ants.NUnit.ChromosomeTests.#RangeTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#DecideNumberOfCandidatesForZeroTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#DecideNumberOfCandidatesForOneToFourTest()")
]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestTrailBuilder",
        Scope = "member", Target = "Aco.NUnit.BestTrailFinderTests.#ClearTest()")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.BestTrailFinder.#FindBestTrail(System.Collections.Generic.IEnumerable`1<Aco.Ants.IAnt>)")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target =
            "Aco.BestTrailFinder.#.ctor(Aco.Ants.IAntFactory,Aco.Interfaces.IDistanceGraph,Aco.Interfaces.IPheromonesTracker,Aco.Trails.Optimizers.IOptimizer,Aco.Trails.ITrailAlternatives)"
        )]
[assembly :
    SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "cityIndex+1", Scope = "member",
        Target = "Aco.Trails.Builders.BaseTrailBuilder.#FindRelatedCity(System.Int32)")]
[assembly :
    SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "cityIndex-1", Scope = "member",
        Target = "Aco.Trails.Builders.BaseTrailBuilder.#FindRelatedCity(System.Int32)")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.NUnit.DistanceGraphTests.#CostMatrixLengthTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.NUnit.DistanceGraphTests.#ConvertToArrayTest()")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.Trails.Builders.BaseTrailBuilder.#BuildDictionaryIndexOfTarget(System.Int32[])")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "3", Scope = "member",
        Target =
            "Aco.Trails.Builders.BaseTrailBuilder.#.ctor(Aco.Trails.Builders.ITrailBuilderFactory,Aco.Ants.IChromosome,Aco.Interfaces.IPheromonesTracker,Aco.Interfaces.IDistanceGraph,Aco.Trails.Optimizers.IOptimizer,Aco.Trails.Builders.TrailBuilderType)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member",
        Target =
            "Aco.Ants.BaseAnt.#.ctor(Aco.Ants.IAntFactory,Aco.Trails.Builders.ITrailBuilderFactory,Aco.Ants.IChromosome,Aco.Interfaces.IPheromonesTracker,Aco.Interfaces.IDistanceGraph,Aco.Trails.Optimizers.IOptimizer,Aco.Ants.AntType)"
        )]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "mId",
        Scope = "member", Target = "Aco.Ants.BaseAnt.#m_Id")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#RemoveReverseNodesForFourTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#RemoveReverseNodesForStartNodeAfterReverseTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#RemoveReverseNodesForStartNodeBeforeReverseTest()")
]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.RandomTrailBuilderTests.#RemoveReverseNodesForTwoTest()")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target =
            "Aco.NUnit.NUnitHelper.#Contains(System.Collections.Generic.IEnumerable`1<System.Collections.Generic.IEnumerable`1<System.Int32>>,System.Collections.Generic.IEnumerable`1<System.Int32>)"
        )]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neighbours",
        Scope = "member", Target = "Aco.Interfaces.IDistanceGraph.#NearestNeighbours")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target =
            "Aco.PheromonesTracker.#.ctor(Aco.Interfaces.IDistanceGraph,Aco.IPheromones,System.Double,System.Double)")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.PheromonesTracker.#.ctor(Aco.Interfaces.IDistanceGraph,Aco.IPheromones)")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "q", Scope = "member",
        Target =
            "Aco.PheromonesTracker.#.ctor(Aco.Interfaces.IDistanceGraph,Aco.IPheromones,System.Double,System.Double)")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Aco.NUnit.NUnitHelper.AssertIsEquivalent(System.Double,System.Double,System.String)",
        Scope = "member", Target = "Aco.NUnit.PheromonesTests.#ToArrayTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.PheromonesTests.#ToArrayTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.NUnit.PheromonesTests")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.NUnit.BestTrailFinderTests.#CreatePheromones(System.Int32)")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "mId",
        Scope = "member", Target = "Aco.Trails.Builders.BaseTrailBuilder.#m_Id")]
[assembly :
    SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member",
        Target = "Aco.Trails.Builders.BaseTrailBuilder.#m_TrailBuilderFactory")]
[assembly :
    SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member",
        Target = "Aco.Trails.Builders.BaseTrailBuilder.#m_Tracker")]
[assembly :
    SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member",
        Target = "Aco.Trails.Builders.BaseTrailBuilder.#m_Optimizer")]
[assembly :
    SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member",
        Target = "Aco.Trails.Builders.BaseTrailBuilder.#m_Graph")]
[assembly :
    SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member",
        Target = "Aco.Trails.Builders.BaseTrailBuilder.#m_Chromosome")]
[assembly :
    SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member",
        Target = "Aco.Trails.Builders.BaseTrailBuilder.#Random")]
[assembly :
    SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member",
        Target = "Aco.Ants.BaseAnt.#m_Optimizer")]
[assembly :
    SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member",
        Target = "Aco.Ants.BaseAnt.#m_Tracker")]
[assembly :
    SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member",
        Target = "Aco.Ants.BaseAnt.#m_TrailBuilderFactory")]
[assembly :
    SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member",
        Target = "Aco.Ants.BaseAnt.#Random")]
[assembly :
    SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member",
        Target = "Aco.Ants.BaseAnt.#m_Graph")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.UnknownAntTests.#CreatePheromonesTracker()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.StandardTrailBuilderTests.#CreatePheromonesTracker()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.StandardAntTests.#CreatePheromonesTracker()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.NUnit.NearestNeighboursTests")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.DoesNotThrow(NUnit.Framework.TestDelegate,System.String)", Scope = "member",
        Target = "Aco.NUnit.NearestNeighboursTests.#ValidateDoesNotThrowForCorrectIndexTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neighbours",
        Scope = "type", Target = "Aco.NearestNeighbours")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neighbours",
        Scope = "member", Target = "Aco.INearestNeighbours.#GetNeighbours(System.Int32)")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neighbours",
        Scope = "type", Target = "Aco.INearestNeighbours")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neighbours",
        Scope = "member", Target = "Aco.Interfaces.IDistanceGraph.#GetNeighbours(System.Int32)")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "q", Scope = "member",
        Target =
            "Aco.IPheromones.#Initialize(System.Int32,System.Double,System.Double,System.Double,System.Double,System.Double)"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.FixedAntTests.#CreatePheromonesTracker()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.DistanceGraphTests.#GetCostTest()")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.DistanceGraph.#.ctor(Aco.INearestNeighbours,System.Int32[][],System.Int32[])")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neighbours",
        Scope = "member", Target = "Aco.DistanceGraph.#.ctor(Aco.INearestNeighbours,System.Int32[][],System.Int32[])")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#CreateDistanceGraph(Aco.INearestNeighbours,System.Int32[][],System.Int32[])")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#CreateCostPerLine(System.Int32[][])")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#CreatePheromonesTracker()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#CreateDistanceGraph(System.Int32[][],System.Int32[])")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.CandidateListAntTests.#CreatePheromonesTracker()")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target =
            "Aco.NUnit.TestQueenFactory.#Create(Aco.Interfaces.IDistanceGraph,Aco.Interfaces.IPheromonesTracker,Aco.Trails.Optimizers.IOptimizer)"
        )]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#UpdateAntsCallsFindBestTrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#ClearTest()")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#BestTrailDefaultTest()")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#Setup()")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type",
        Target = "Aco.NUnit.QueenTests")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#AddBestAntTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#AddBestAntTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#ClearCreatesAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#ClearReleasesBestAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#CreateAntsCreatesNewAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#CreateAntsReleasesBestAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#ReleaseAllAntsReleasesBestAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#ReleaseAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#RestartCreatesAntsContainingBestAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.False(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#RestartDoesNotClearBestAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestAnts",
        Scope = "member", Target = "Aco.NUnit.SquadTests.#SetNumberOfAntsClearsBestAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#SetNumberOfAntsClearsBestAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "NumberOfAnts",
        Scope = "member", Target = "Aco.NUnit.SquadTests.#SetNumberOfAntsCreatesNewAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#SetNumberOfAntsCreatesNewAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestAnts",
        Scope = "member", Target = "Aco.NUnit.SquadTests.#SetNumberOfAntsCreatesNewAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#SetNumberOfAntsReleasesBestAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison",
        MessageId = "System.String.StartsWith(System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#ToStringTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.NUnit.TestAntFactory.#Released(Aco.Ants.IAnt)")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestAnts",
        Scope = "member", Target = "Aco.NUnit.SquadTests.#CreateAntsCreatesNewAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "NumberOfAnts",
        Scope = "member", Target = "Aco.NUnit.SquadTests.#CreateAntsCreatesNewAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Int32,System.Int32,System.String)", Scope = "member",
        Target = "Aco.NUnit.SquadTests.#CreateAntsClearsBestAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BestAnts",
        Scope = "member", Target = "Aco.NUnit.SquadTests.#CreateAntsClearsBestAntsTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type",
        Target = "Aco.NUnit.SquadTests")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type",
        Target = "Aco.NUnit.SquadTests")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "4", Scope = "member",
        Target =
            "Aco.Trails.Builders.BaseTrailBuilder.#.ctor(Common.IRandom,Aco.Trails.Builders.ITrailBuilderFactory,Aco.Ants.IChromosome,Aco.Interfaces.IPheromonesTracker,Aco.Interfaces.IDistanceGraph,Aco.Trails.Optimizers.IOptimizer,Aco.Trails.Builders.TrailBuilderType)"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests.#Teardown()")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.Ants.NUnit.BaseAntTests+TestBaseAnt.#Clone()")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type",
        Target = "Aco.NUnit.BestTrailFinderTests")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#FindCandidatesForCandidateZeroIsVisitedTest()")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#BuildCandidateListForNumberOfCandidatesIsOneTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#BuildCandidateListForNumberOfCandidatesIsTwoTest()"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#SearchGeneralForOtherTwoUnvisitedTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#SearchGeneralForOtherTwoUnvisitedTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "betaMax",
        Scope = "member",
        Target =
            "Aco.Ants.Chromosome.#.ctor(Common.IRandom,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double)"
        )]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "betaMax",
        Scope = "member",
        Target =
            "Aco.Ants.Chromosome.#.ctor(Common.IRandom,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "6", Scope = "member",
        Target =
            "Aco.Colony.#.ctor(Castle.Core.Logging.ISelkieLogger,Common.IRandom,Aco.IQueenFactory,Aco.Trails.Builders.ITrailBuilderFactory,Aco.Interfaces.IPheromonesTracker,Aco.Interfaces.IDistanceGraph,Aco.Trails.Optimizers.IOptimizer,Aco.Trails.ITrailHistory)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "2", Scope = "member",
        Target =
            "Aco.Colony.#.ctor(Castle.Core.Logging.ISelkieLogger,Common.IRandom,Aco.IQueenFactory,Aco.Trails.Builders.ITrailBuilderFactory,Aco.Interfaces.IPheromonesTracker,Aco.Interfaces.IDistanceGraph,Aco.Trails.Optimizers.IOptimizer,Aco.Trails.ITrailHistory)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Scope = "member",
        Target = "Aco.Colony.#Dispose()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.NUnit.ColonyTests.#SettingsToChromosomeTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neighbours",
        Scope = "member",
        Target = "Aco.DistanceGraph.#.ctor(Common.IRandom,Aco.INearestNeighbours,System.Int32[][],System.Int32[])")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member",
        Target = "Aco.DistanceGraph.#.ctor(Common.IRandom,Aco.INearestNeighbours,System.Int32[][],System.Int32[])")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "q", Scope = "member",
        Target =
            "Aco.PheromonesTracker.#.ctor(Common.IRandom,Aco.Interfaces.IDistanceGraph,Aco.IPheromones,System.Double,System.Double)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member",
        Target = "Aco.PheromonesTracker.#.ctor(Common.IRandom,Aco.Interfaces.IDistanceGraph,Aco.IPheromones)")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member",
        Target =
            "Aco.PheromonesTracker.#.ctor(Common.IRandom,Aco.Interfaces.IDistanceGraph,Aco.IPheromones,System.Double,System.Double)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Aco.Trails.TrailAlternatives.#IsKnownAlternative(Aco.Trails.Builders.ITrailBuilder)")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type",
        Target = "Aco.Ants.NUnit.UnknownAntTests")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target =
            "Aco.Trails.Builders.NUnit.CandidateListTrailBuilderTests.#FindCandidatesForAllCandidatesNotVisitedTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.AreEqual(System.Object,System.Object,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.UnknownAntTests.#ConstructorForTrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IsUnknown",
        Scope = "member", Target = "Aco.Ants.NUnit.UnknownAntTests.#ConstructorForTrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "NUnit.Framework.Assert.True(System.Boolean,System.String)", Scope = "member",
        Target = "Aco.Ants.NUnit.UnknownAntTests.#ConstructorForTrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type",
        Target = "Aco.Ants.NUnit.BaseAntTests")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type",
        Target = "Aco.Ants.NUnit.StandardAntTests")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type",
        Target = "Aco.Ants.NUnit.CandidateListAntTests")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type",
        Target = "Aco.NUnit.ColonyTests")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type",
        Target = "Aco.Ants.NUnit.FixedAntTests")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.Ants.NUnit.UnknownAntTests.#ConstructorForTrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target =
            "Aco.NUnit.TestBestTrailFinderFactory.#Create(Aco.Interfaces.IDistanceGraph,Aco.Interfaces.IPheromonesTracker,Aco.Trails.Optimizers.IOptimizer)"
        )]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.Ants.NUnit.FixedAntTests.#ConstructorWithoutTrailTest()")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#UpdateAntsCallsTrackerUpdateTest()")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#UpdateAntsUpdatesBestAntTest()")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#UpdateAntsUsingOffspringCallsMutationTest()")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#UpdateAntsUsingOffspringCallsOffspringTest()")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#UpdateBestAntDoesNotSetsNewBestWhenNewIsLongerTest()")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#UpdateBestAntSetsNewBestWhenNewIsShorterTest()")]
[assembly :
    SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member",
        Target = "Aco.NUnit.QueenTests.#UpdateBestAntSetsNewBestWhenStillUnknownTest()")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type",
        Target = "Selkie.Aco.Anthill.NUnit.TestQueenFactory")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member",
        Target =
            "Selkie.Aco.Anthill.BestTrailFinder.#.ctor(Selkie.Common.IDisposer,Selkie.Aco.Common.IAntFactory,Selkie.Aco.Common.IDistanceGraph,Selkie.Aco.Common.IPheromonesTracker,Selkie.Aco.Common.IOptimizer,Selkie.Aco.Trails.ITrailAlternatives)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "3", Scope = "member",
        Target =
            "Selkie.Aco.Anthill.Queen.#.ctor(Castle.Core.Logging.ISelkieLogger,Selkie.Aco.Common.IAntFactory,Selkie.Aco.Common.TypedFactories.IChromosomeFactory,Selkie.Aco.Anthill.TypedFactories.IBestTrailFinderFactory,Selkie.Aco.Common.IDistanceGraph,Selkie.Aco.Common.IPheromonesTracker,Selkie.Aco.Common.IOptimizer,Selkie.Aco.Ants.ICrossover,Selkie.Aco.Anthill.TypedFactories.ISquadFactory)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "8", Scope = "member",
        Target =
            "Selkie.Aco.Anthill.Queen.#.ctor(Castle.Core.Logging.ISelkieLogger,Selkie.Aco.Common.IAntFactory,Selkie.Aco.Common.TypedFactories.IChromosomeFactory,Selkie.Aco.Anthill.TypedFactories.IBestTrailFinderFactory,Selkie.Aco.Common.IDistanceGraph,Selkie.Aco.Common.IPheromonesTracker,Selkie.Aco.Common.IOptimizer,Selkie.Aco.Ants.ICrossover,Selkie.Aco.Anthill.TypedFactories.ISquadFactory)"
        )]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member",
        Target = "Selkie.Aco.Anthill.PheromonesInformation.#Values")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Error",
        Scope = "member", Target = "Selkie.Aco.Anthill.IColonyLogger.#Error(System.String)")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Stop", Scope = "member",
        Target = "Selkie.Aco.Anthill.IColony.#Stop()")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Castle.Core.Logging.ISelkieLogger.Info(System.String)", Scope = "member",
        Target = "Selkie.Aco.Anthill.ColonyLogger.#LogResultForTrailFound(System.TimeSpan)")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "Selkie.Aco.Anthill.ColonyLogger.#LogTrailBuilder(Selkie.Aco.Anthill.LogTrailBuilderInformation)")]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Selkie.Aco.Anthill.IColonyLogger.Info(System.String)", Scope = "member",
        Target = "Selkie.Aco.Anthill.Colony.#Start(System.Int32)")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "8", Scope = "member",
        Target =
            "Selkie.Aco.Anthill.Colony.#.ctor(Selkie.Aco.Anthill.IColonyLogger,Selkie.Common.IDateTime,Selkie.Aco.Anthill.TypedFactories.IQueenFactory,Selkie.Aco.Common.TypedFactories.IChromosomeFactory,Selkie.Aco.Common.TypedFactories.ITrailBuilderFactory,Selkie.Aco.Anthill.TypedFactories.IPheromonesTrackerFactory,Selkie.Aco.Common.IDistanceGraph,Selkie.Aco.Common.IOptimizer,Selkie.Aco.Anthill.TypedFactories.INaturalSelectionFactory)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "2", Scope = "member",
        Target =
            "Selkie.Aco.Anthill.Colony.#.ctor(Selkie.Aco.Anthill.IColonyLogger,Selkie.Common.IDateTime,Selkie.Aco.Anthill.TypedFactories.IQueenFactory,Selkie.Aco.Common.TypedFactories.IChromosomeFactory,Selkie.Aco.Common.TypedFactories.ITrailBuilderFactory,Selkie.Aco.Anthill.TypedFactories.IPheromonesTrackerFactory,Selkie.Aco.Common.IDistanceGraph,Selkie.Aco.Common.IOptimizer,Selkie.Aco.Anthill.TypedFactories.INaturalSelectionFactory)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "5", Scope = "member",
        Target =
            "Selkie.Aco.Anthill.Colony.#.ctor(Selkie.Aco.Anthill.IColonyLogger,Selkie.Common.IDateTime,Selkie.Aco.Anthill.TypedFactories.IQueenFactory,Selkie.Aco.Common.TypedFactories.IChromosomeFactory,Selkie.Aco.Common.TypedFactories.ITrailBuilderFactory,Selkie.Aco.Anthill.TypedFactories.IPheromonesTrackerFactory,Selkie.Aco.Common.IDistanceGraph,Selkie.Aco.Common.IOptimizer,Selkie.Aco.Anthill.TypedFactories.INaturalSelectionFactory)"
        )]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target =
            "Selkie.Aco.Anthill.BestTrailFinder.#FindBestTrail(System.Collections.Generic.IEnumerable`1<Selkie.Aco.Common.IAnt>)"
        )]
[assembly :
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "Castle.Core.Logging.ISelkieLogger.Info(System.String)", Scope = "member",
        Target = "Selkie.Aco.Anthill.ColonyLogger.#LogResultNoTrailFound()")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Aco",
        Scope = "namespace", Target = "Selkie.Aco.Anthill.TypedFactories")]
[assembly : SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Aco",
        Scope = "namespace", Target = "Selkie.Aco.Anthill")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Selkie",
        Scope = "namespace", Target = "Selkie.Aco.Anthill")]
[assembly : SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Selkie")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Selkie",
        Scope = "namespace", Target = "Selkie.Aco.Anthill.TypedFactories")]
[assembly : SuppressMessage("Microsoft.Design", "CA1014:MarkAssembliesWithClsCompliant")]
[assembly : SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Aco")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Aco",
        Scope = "namespace", Target = "Selkie.Aco.Anthill.TypedFactories")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Aco",
        Scope = "namespace", Target = "Selkie.Aco.Anthill")]