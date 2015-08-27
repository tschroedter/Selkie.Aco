using System;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Aco.Trails;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Aco.Anthill
{
    [ProjectComponent(Lifestyle.Transient)]
    public class ColonyLogger : IColonyLogger
    {
        private readonly ISelkieLogger m_Logger;

        public ColonyLogger([NotNull] ISelkieLogger logger,
                            [NotNull] ITrailHistory trailHistory)
        {
            m_Logger = logger;
            TrailHistory = trailHistory;
        }

        public ITrailHistory TrailHistory { get; set; }

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

            string chromosomeText = " (Alpha: {0} Beta: {1} Gamma: {2})".Inject(chromosome.Alpha,
                                                                                chromosome.Beta,
                                                                                chromosome.Gamma);
            string timeText = "[Time: {0:D5} {1} {2}]".Inject(information.Time,
                                                              information.TurnsSinceNewBestTrailFound,
                                                              information.TurnsRemaining);

            string trailText = "Trail with length of {0} found at time {1}: {2} Nodes: {3}".Inject(trailBuilder,
                                                                                                   trailBuilder.Length
                                                                                                               .ToString
                                                                                                       ("F1",
                                                                                                        CultureInfo
                                                                                                            .InvariantCulture),
                                                                                                   information.Time,
                                                                                                   trailBuilder);

            string message = "{0} {1} - {2} - {3}".Inject(timeText,
                                                          information.Prefix,
                                                          trailText,
                                                          chromosomeText);

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

        private void LogResultNoTrailFound()
        {
            m_Logger.Info("No trail found!");
        }

        private void LogResultForTrailFound(TimeSpan runTimeSpan)
        {
            IOrderedEnumerable <ITrailInformation> orderedByLength = from information in TrailHistory.Information
                                                                     orderby information.TrailBuilder.Length
                                                                     select information;

            m_Logger.Info("Best trail found:");
            m_Logger.Info("=================");
            m_Logger.Info("{0}".Inject(orderedByLength.First()));
            m_Logger.Info("Run time: {0}".Inject(runTimeSpan));

            m_Logger.Info("Alternative Trails:");
            m_Logger.Info("-------------");

            foreach ( ITrailInformation trailInformation in orderedByLength )
            {
                m_Logger.Info(trailInformation.ToString());
            }

            m_Logger.Info("End Ant Colony Optimization demo!");
        }
    }
}