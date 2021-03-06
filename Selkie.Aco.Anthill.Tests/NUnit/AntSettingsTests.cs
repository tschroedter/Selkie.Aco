using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Aco.Common.Interfaces;

namespace Selkie.Aco.Anthill.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    // ReSharper disable once ClassTooBig
    internal sealed class AntSettingsTests
    {
        [Test]
        public void Constructor_SetsFixedStartNode_ForGivenStartNode()
        {
            // Arrange
            // Act
            var sut = new AntSettings(AntSettings.TrailStartNodeType.Fixed,
                                      1);

            // Assert
            Assert.AreEqual(1,
                            sut.FixedStartNode);
        }

        [Test]
        public void Constructor_SetsIsFixedStartNodeToFalse_ForTrailStartNodeTypeRandom()
        {
            // Arrange
            // Act
            var sut = new AntSettings(AntSettings.TrailStartNodeType.Random,
                                      0);

            // Assert
            Assert.False(sut.IsFixedStartNode);
        }

        [Test]
        public void Constructor_SetsIsFixedStartNodeToTrue_ForTrailStartNodeTypeFixed()
        {
            // Arrange
            // Act
            var sut = new AntSettings(AntSettings.TrailStartNodeType.Fixed,
                                      0);

            // Assert
            Assert.True(sut.IsFixedStartNode);
        }

        [Test]
        public void Constructor_SetsIsUnknownToFalse_WhenCreated()
        {
            // Arrange
            // Act
            var sut = new AntSettings(AntSettings.TrailStartNodeType.Random,
                                      0);

            // Assert
            Assert.False(sut.IsUnknown);
        }

        [Test]
        public void Unknown_SetsIsUnknownToTrue_WhenCreated()
        {
            // Arrange
            // Act
            IAntSettings sut = AntSettings.Unknown;

            // Assert
            Assert.True(sut.IsUnknown);
        }
    }
}