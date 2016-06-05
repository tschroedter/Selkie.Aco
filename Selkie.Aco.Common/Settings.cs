using System.Globalization;
using System.Text;
using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;

namespace Selkie.Aco.Common
{
    public sealed class Settings : ISettings
    {
        public Settings([NotNull] IAnt ant,
                        [NotNull] IPheromonesTracker tracker)
        {
            IChromosome chromosome = ant.Chromosome;

            m_AntType = ant.Type;
            m_Alpha = chromosome.Alpha;
            m_Beta = chromosome.Beta;
            m_Gamma = chromosome.Gamma;
            m_Rho = tracker.Rho;
            m_Q = tracker.Q;
        }

        private readonly double m_Alpha;
        private readonly string m_AntType;
        private readonly double m_Beta;
        private readonly double m_Gamma;
        private readonly double m_Q;
        private readonly double m_Rho;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(string.Format(CultureInfo.InvariantCulture,
                                    "AntType = {0} ",
                                    m_AntType));
            sb.Append(string.Format(CultureInfo.InvariantCulture,
                                    "Alpha = {0:F4} ",
                                    m_Alpha));
            sb.Append(string.Format(CultureInfo.InvariantCulture,
                                    "Betta = {0:F4} ",
                                    m_Beta));
            sb.Append(string.Format(CultureInfo.InvariantCulture,
                                    "Gamma = {0:F4} ",
                                    m_Gamma));
            sb.Append(string.Format(CultureInfo.InvariantCulture,
                                    "Rho = {0:F4} ",
                                    m_Rho));
            sb.Append(string.Format(CultureInfo.InvariantCulture,
                                    "Q = {0:F4}",
                                    m_Q));

            return sb.ToString();
        }

        #region ISettings Members

        public string AntType
        {
            get
            {
                return m_AntType;
            }
        }

        public double Alpha
        {
            get
            {
                return m_Alpha;
            }
        }

        public double Beta
        {
            get
            {
                return m_Beta;
            }
        }

        public double Gamma
        {
            get
            {
                return m_Gamma;
            }
        }

        public double Rho
        {
            get
            {
                return m_Rho;
            }
        }

        public double Q
        {
            get
            {
                return m_Q;
            }
        }

        #endregion
    }
}