using System.Text;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Trails
{
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class TrailInformation : ITrailInformation
    {
        public TrailInformation([NotNull] ITrailBuilder trailBuilder,
                                [NotNull] ISettings settings,
                                int time)
        {
            m_TrailBuilder = trailBuilder;
            m_Settings = settings;
            m_Time = time;
        }

        private readonly ISettings m_Settings;
        private readonly int m_Time;
        private readonly ITrailBuilder m_TrailBuilder;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"Time: {m_Time:D4} ");
            sb.Append($"Length: {(( int ) m_TrailBuilder.Length):D4} ");
            sb.Append($"Trail: {m_TrailBuilder} ");
            sb.Append($"Type: {m_Settings.AntType} ");
            sb.Append($"Alpha: {m_Settings.Alpha:F4} ");
            sb.Append($"Beta: {m_Settings.Beta:F4} ");
            sb.Append($"Gamma: {m_Settings.Gamma:F4} ");
            sb.Append($"Rho: {m_Settings.Rho:F4} ");
            sb.Append($"Q: {m_Settings.Q:F4}");

            return sb.ToString();
        }

        #region ITrailInformation Members

        public ITrailBuilder TrailBuilder
        {
            get
            {
                return m_TrailBuilder;
            }
        }

        public int Time
        {
            get
            {
                return m_Time;
            }
        }

        public ISettings Settings
        {
            get
            {
                return m_Settings;
            }
        }

        #endregion
    }
}