using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Trails;

namespace Selkie.Aco.Anthill.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class TrailInformationTests
    {
        [SetUp]
        public void Setup()
        {
            m_TrailBuilder = Substitute.For <ITrailBuilder>();
            m_TrailBuilder.Length.Returns(6.0);

            m_Settings = Substitute.For <ISettings>();
            m_Settings.AntType.Returns("AntType");
            m_Settings.Alpha.Returns(1.0);
            m_Settings.Beta.Returns(2.0);
            m_Settings.Gamma.Returns(3.0);
            m_Settings.Rho.Returns(4.0);
            m_Settings.Q.Returns(5.0);

            m_Sut = new TrailInformation(m_TrailBuilder,
                                         m_Settings,
                                         7);
        }

        private TrailInformation m_Sut;
        private ISettings m_Settings;
        private ITrailBuilder m_TrailBuilder;

        [Test]
        public void Settings_ReturnsValues_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_Settings,
                            m_Sut.Settings);
        }

        [Test]
        public void ToString_ReturnsText_WhenCalled()
        {
            // Arrange
            string expected = "Time: 0007 Length: 0006 Trail: " + m_TrailBuilder +
                              " Type: AntType Alpha: 1.0000 Beta: 2.0000 Gamma: 3.0000 Rho: 4.0000 Q: 5.0000";

            // Act
            string actual = m_Sut.ToString();

            // Assert
            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void Trail_ReturnsTrail_WhenCalled()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(m_TrailBuilder,
                            m_Sut.TrailBuilder);
        }
    }
}