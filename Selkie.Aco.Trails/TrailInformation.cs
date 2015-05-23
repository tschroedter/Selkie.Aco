using System.Text;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Aco.Trails
{
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class TrailInformation : ITrailInformation
    {
        private readonly ISettings m_Settings;
        private readonly int m_Time;
        private readonly ITrailBuilder m_TrailBuilder;

        public TrailInformation([NotNull] ITrailBuilder trailBuilder,
                                [NotNull] ISettings settings,
                                int time)
        {
            m_TrailBuilder = trailBuilder;
            m_Settings = settings;
            m_Time = time;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Time: {0:D4} ".Inject(m_Time));
            sb.Append("Length: {0:D4} ".Inject(( int ) m_TrailBuilder.Length));
            sb.Append("Trail: {0} ".Inject(m_TrailBuilder));
            sb.Append("Type: {0} ".Inject(m_Settings.AntType));
            sb.Append("Alpha: {0:F4} ".Inject(m_Settings.Alpha));
            sb.Append("Beta: {0:F4} ".Inject(m_Settings.Beta));
            sb.Append("Gamma: {0:F4} ".Inject(m_Settings.Gamma));
            sb.Append("Rho: {0:F4} ".Inject(m_Settings.Rho));
            sb.Append("Q: {0:F4}".Inject(m_Settings.Q));

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