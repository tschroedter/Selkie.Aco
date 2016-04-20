using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;

namespace Selkie.Aco.Anthill
{
    public class LogTrailBuilderInformation
    {
        private readonly string m_Prefix;
        private readonly int m_Time;
        private readonly ITrailBuilder m_TrailBuilder;
        private readonly int m_TurnsRemaining;
        private readonly int m_TurnsSinceNewBestTrailFound;

        public LogTrailBuilderInformation([NotNull] string prefix,
                                          int time,
                                          [NotNull] ITrailBuilder trailBuilder,
                                          int turnsRemaining,
                                          int turnsSinceNewBestTrailFound)
        {
            m_Prefix = prefix;
            m_Time = time;
            m_TrailBuilder = trailBuilder;
            m_TurnsRemaining = turnsRemaining;
            m_TurnsSinceNewBestTrailFound = turnsSinceNewBestTrailFound;
        }

        [NotNull]
        public string Prefix
        {
            get
            {
                return m_Prefix;
            }
        }

        public int Time
        {
            get
            {
                return m_Time;
            }
        }

        [NotNull]
        public ITrailBuilder TrailBuilder
        {
            get
            {
                return m_TrailBuilder;
            }
        }

        public int TurnsRemaining
        {
            get
            {
                return m_TurnsRemaining;
            }
        }

        public int TurnsSinceNewBestTrailFound
        {
            get
            {
                return m_TurnsSinceNewBestTrailFound;
            }
        }
    }
}