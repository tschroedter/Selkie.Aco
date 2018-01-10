using System;
using System.Globalization;
using System.Linq;
using Core2.Selkie.Aco.Anthill.Interfaces;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Windsor;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill
{
    [ProjectComponent(Lifestyle.Transient)]
    public class ColonyLogger : IColonyLogger
    {
        public ColonyLogger([NotNull] ISelkieLogger logger,
                            [NotNull] ITrailHistory trailHistory)
        {
            m_Logger = logger;
            TrailHistory = trailHistory;
        }

        public ITrailHistory TrailHistory { get; set; }
        private readonly ISelkieLogger m_Logger;

        public void LogResult(TimeSpan runtimeSpan)
        {
            if ( !TrailHistory.Information.Any() )
            {
                LogResultNoTrailFound();
                return;
            }

            LogResultForTrailFound(runtimeSpan);
        }

        public void LogTrailBuilder(LogTrailBuilderInformation information)
        {
            ITrailBuilder trailBuilder = information.TrailBuilder;

            IChromosome chromosome = trailBuilder.Chromosome;

            string chromosomeText = $" (Alpha: {chromosome.Alpha}" +
                                    $" Beta: {chromosome.Beta}" +
                                    $" Gamma: {chromosome.Gamma})";
            string timeText =
                $"[Time: {information.Time:D5} " +
                $"{information.TurnsSinceNewBestTrailFound} " +
                $"{information.TurnsRemaining}]";

            string length = trailBuilder.Length.ToString("F1",
                                                         CultureInfo.InvariantCulture);
            string nodes = string.Join(",",
                                       trailBuilder.Trail);
            string trailText = $"[{trailBuilder.Type}] Trail with length of {length}" +
                               $" found at time {information.Time} Nodes: {nodes}";

            string message = $"{timeText} {information.Prefix} - {trailText} - {chromosomeText}";

            m_Logger.Info(message);
        }

        public void Info(string message)
        {
            m_Logger.Info(message);
        }

        public void Error(string message)
        {
            m_Logger.Error(message);
        }

        private void LogResultForTrailFound(TimeSpan runTimeSpan)
        {
            IOrderedEnumerable <ITrailInformation> orderedByLength = from information in TrailHistory.Information
                                                                     orderby information.TrailBuilder.Length
                                                                     select information;

            var array = orderedByLength.ToArray();

            m_Logger.Info("Best trail found:");
            m_Logger.Info("=================");
            m_Logger.Info($"{array.First()}");
            m_Logger.Info($"Run time: {runTimeSpan}");

            m_Logger.Info("Alternative Trails:");
            m_Logger.Info("-------------");

            foreach ( ITrailInformation trailInformation in array)
            {
                m_Logger.Info(trailInformation.ToString());
            }

            m_Logger.Info("End Ant Colony Optimization demo!");
        }

        private void LogResultNoTrailFound()
        {
            m_Logger.Info("No trail found!");
        }
    }
}