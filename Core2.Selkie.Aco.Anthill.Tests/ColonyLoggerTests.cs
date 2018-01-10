using System;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Windsor.Interfaces;

namespace Core2.Selkie.Aco.Anthill.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class ColonyLoggerTests
    {
        [SetUp]
        public void Setup()
        {
            m_Logger = Substitute.For <ISelkieLogger>();
            m_TrailHistory = Substitute.For <ITrailHistory>();

            m_Sut = new ColonyLogger(m_Logger,
                                     m_TrailHistory);
        }

        private ColonyLogger m_Sut;
        private ISelkieLogger m_Logger;
        private ITrailHistory m_TrailHistory;

        [Test]
        public void Error_CallsLogger_ForText()
        {
            // Arrange
            // Act
            // Assert
            m_Sut.Info("Error");

            m_Logger.Received().Info("Error");
        }

        [Test]
        public void Info_CallsLogger_ForText()
        {
            // Arrange
            // Act
            m_Sut.Info("Info");

            // Assert
            m_Logger.Received().Info("Info");
        }

        [Test]
        public void LogResult_CallsLogger_ForHistoryContainsElement()
        {
            // Arrange
            m_TrailHistory.Information.Returns(new[]
                                               {
                                                   Substitute.For <ITrailInformation>()
                                               });

            var oneMinute = new TimeSpan(0,
                                         1,
                                         0);

            // Act
            m_Sut.LogResult(oneMinute);

            // Assert
            m_Logger.Received().Info(Arg.Any <string>());
        }

        [Test]
        public void LogResult_CallsLogger_ForHistoryEmpty()
        {
            // Arrange
            m_TrailHistory.Information.Returns(new ITrailInformation[]
                                               {
                                               });

            var oneMinute = new TimeSpan(0,
                                         1,
                                         0);

            // Act
            m_Sut.LogResult(oneMinute);

            // Assert
            m_Logger.Received().Info("No trail found!");
        }

        [Test]
        public void LogTrailBuilder_CallsLogger_WhenCalled()
        {
            // Arrange
            const string expected =
                "[Time: 00123 10 50] Prefix - Trail with length of Castle.Proxies.ITrailBuilderProxy found at time 0.0: 123 Nodes: Castle.Proxies.ITrailBuilderProxy -  (Alpha: 0 Beta: 0 Gamma: 0)";

            var information = new LogTrailBuilderInformation("Prefix",
                                                             123,
                                                             Substitute.For <ITrailBuilder>(),
                                                             50,
                                                             10);

            // Act
            m_Sut.LogTrailBuilder(information);


            // Assert
            m_Logger.Received().Info(Arg.Is <string>(x => string.Compare(x,
                                                                         expected,
                                                                         StringComparison.Ordinal) == 0));
        }

        [Test]
        public void TrailHistory_RoundtripTest()
        {
            // Arrange
            var newTrailHistory = Substitute.For <ITrailHistory>();

            // Act
            m_Sut.TrailHistory = newTrailHistory;

            // Assert
            Assert.AreEqual(newTrailHistory,
                            m_Sut.TrailHistory);
        }

        [Test]
        public void TrailHistoryD_ReturnsDefault_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_TrailHistory,
                            m_Sut.TrailHistory);
        }
    }
}