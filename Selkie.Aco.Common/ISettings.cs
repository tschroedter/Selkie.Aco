using JetBrains.Annotations;

namespace Selkie.Aco.Common
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