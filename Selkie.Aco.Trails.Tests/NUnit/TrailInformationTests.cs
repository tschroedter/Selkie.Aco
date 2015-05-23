using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Aco.Common;

namespace Selkie.Aco.Trails.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class TrailInformationTests
    {
        [SetUp]
        public void Setup()
        {
            m_TrailBuilder = Substitute.For <ITrailBuilder>();
            m_TrailBuilder.Length.Returns(100);

            m_Settings = Substitute.For <ISettings>();
            m_Settings.AntType.Returns("AntType");
            m_Settings.Alpha.Returns(0.1);
            m_Settings.Beta.Returns(0.2);
            m_Settings.Gamma.Returns(0.3);
            m_Settings.Rho.Returns(0.4);
            m_Settings.Q.Returns(0.5);

            m_Info = new TrailInformation(m_TrailBuilder,
                                          m_Settings,
                                          10);
        }

        private TrailInformation m_Info;
        private ISettings m_Settings;
        private ITrailBuilder m_TrailBuilder;

        [Test]
        public void SettingsTest()
        {
            Assert.AreEqual(m_Settings,
                            m_Info.Settings);
        }

        [Test]
        public void TimeTest()
        {
            Assert.AreEqual(10,
                            m_Info.Time);
        }

        [Test]
        public void ToStringTest()
        {
            const string expected =
                "Time: 0010 Length: 0100 Trail: Castle.Proxies.ITrailBuilderProxy Type: AntType Alpha: 0.1000 Beta: 0.2000 Gamma: 0.3000 Rho: 0.4000 Q: 0.5000";
            string actual = m_Info.ToString();

            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        public void TrailBuilderTest()
        {
            Assert.AreEqual(m_TrailBuilder,
                            m_Info.TrailBuilder);
        }
    }
}