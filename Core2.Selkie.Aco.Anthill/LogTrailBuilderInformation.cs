using Core2.Selkie.Aco.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill
{
    public class LogTrailBuilderInformation
    {
        public LogTrailBuilderInformation([NotNull] string prefix,
                                          int time,
                                          [NotNull] ITrailBuilder trailBuilder,
                                          int turnsRemaining,
                                          int turnsSinceNewBestTrailFound)
        {
            Prefix = prefix;
            Time = time;
            TrailBuilder = trailBuilder;
            TurnsRemaining = turnsRemaining;
            TurnsSinceNewBestTrailFound = turnsSinceNewBestTrailFound;
        }

        [NotNull]
        public string Prefix { get; }

        public int Time { get; }

        [NotNull]
        public ITrailBuilder TrailBuilder { get; }

        public int TurnsRemaining { get; }

        public int TurnsSinceNewBestTrailFound { get; }
    }
}