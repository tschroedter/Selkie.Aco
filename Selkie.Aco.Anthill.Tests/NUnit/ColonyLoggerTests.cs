using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Core.Logging;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Common;
using Selkie.Aco.Trails;

namespace Selkie.Aco.Anthill.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class ColonyLoggerTests
    {
        [SetUp]
        public void Setup()
        {
            m_Logger = Substitute.For <ILogger>();
            m_TrailHistory = Substitute.For <ITrailHistory>();

            m_ColonyLogger = new ColonyLogger(m_Logger,
                                              m_TrailHistory);
        }

        private ColonyLogger m_ColonyLogger;
        private ILogger m_Logger;
        private ITrailHistory m_TrailHistory;

        [Test]
        public void ErrorTest()
        {
            m_ColonyLogger.Info("Error");

            m_Logger.Received().Info("Error");
        }

        [Test]
        public void InfoTest()
        {
            m_ColonyLogger.Info("Info");

            m_Logger.Received().Info("Info");
        }

        [Test]
        public void LogResultCallsLoggerForHistoryContainsElementTest()
        {
            m_TrailHistory.Information.Returns(new[]
                                               {
                                                   Substitute.For <ITrailInformation>()
                                               });

            var oneMinute = new TimeSpan(0,
                                         1,
                                         0);

            m_ColonyLogger.LogResult(oneMinute);

            m_Logger.Received().Info(Arg.Any <string>());
        }

        [Test]
        public void LogResultCallsLoggerForHistoryEmptyTest()
        {
            m_TrailHistory.Information.Returns(new ITrailInformation[]
                                               {
                                               });

            var oneMinute = new TimeSpan(0,
                                         1,
                                         0);

            m_ColonyLogger.LogResult(oneMinute);

            m_Logger.Received().Info("No trail found!");
        }

        [Test]
        public void LogTrailBuilderTest()
        {
            var information = new LogTrailBuilderInformation("Prefix",
                                                             123,
                                                             Substitute.For <ITrailBuilder>(),
                                                             50,
                                                             10);

            m_ColonyLogger.LogTrailBuilder(information);

            const string expected =
                "[Time: 00123 10 50] Prefix - Trail with length of Castle.Proxies.ITrailBuilderProxy found at time 0.0: 123 Nodes: Castle.Proxies.ITrailBuilderProxy -  (Alpha: 0 Beta: 0 Gamma: 0)";

            m_Logger.Received().Info(Arg.Is <string>(x => string.Compare(x,
                                                                         expected,
                                                                         StringComparison.Ordinal) == 0));
        }

        [Test]
        public void TrailHistoryDefaultTest()
        {
            Assert.AreEqual(m_TrailHistory,
                            m_ColonyLogger.TrailHistory);
        }

        [Test]
        public void TrailHistoryRoundtripTest()
        {
            var newTrailHistory = Substitute.For <ITrailHistory>();

            m_ColonyLogger.TrailHistory = newTrailHistory;

            Assert.AreEqual(newTrailHistory,
                            m_ColonyLogger.TrailHistory);
        }
    }
}