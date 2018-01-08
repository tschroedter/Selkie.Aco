using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global

namespace Core2.Selkie.Aco.Common.Interfaces
{
    public interface ISettings
    {
        double Alpha { get; }
        double Beta { get; }
        double Gamma { get; }
        double Rho { get; }
        double Q { get; }

        [NotNull]
        string AntType { get; }
    }
}