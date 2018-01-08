using System.Globalization;
using System.Text;
using Core2.Selkie.Aco.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Common
{
    [UsedImplicitly]
    public sealed class Settings : ISettings
    {
        public Settings([NotNull] IAnt ant,
                        [NotNull] IPheromonesTracker tracker)
        {
            IChromosome chromosome = ant.Chromosome;

            AntType = ant.Type;
            Alpha = chromosome.Alpha;
            Beta = chromosome.Beta;
            Gamma = chromosome.Gamma;
            Rho = tracker.Rho;
            Q = tracker.Q;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(string.Format(CultureInfo.InvariantCulture,
                                    "AntType = {0} ",
                                    AntType));
            sb.Append(string.Format(CultureInfo.InvariantCulture,
                                    "Alpha = {0:F4} ",
                                    Alpha));
            sb.Append(string.Format(CultureInfo.InvariantCulture,
                                    "Betta = {0:F4} ",
                                    Beta));
            sb.Append(string.Format(CultureInfo.InvariantCulture,
                                    "Gamma = {0:F4} ",
                                    Gamma));
            sb.Append(string.Format(CultureInfo.InvariantCulture,
                                    "Rho = {0:F4} ",
                                    Rho));
            sb.Append(string.Format(CultureInfo.InvariantCulture,
                                    "Q = {0:F4}",
                                    Q));

            return sb.ToString();
        }

        #region ISettings Members

        public string AntType { get; }

        public double Alpha { get; }

        public double Beta { get; }

        public double Gamma { get; }

        public double Rho { get; }

        public double Q { get; }

        #endregion
    }
}