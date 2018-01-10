using System;
using System.Collections.Generic;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Interfaces;
using JetBrains.Annotations;

// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable EventNeverSubscribedTo.Global

namespace Core2.Selkie.Aco.Anthill.Interfaces
{
    public interface IColony
    {
        int NumberOfNodes { get; }
        int NumberOfAnts { get; }

        [NotNull]
        IEnumerable <IAnt> Ants { get; }

        [NotNull]
        ITrailBuilder BestTrailBuilder { get; }

        [NotNull]
        IEnumerable <ITrailBuilder> Alternatives { get; }

        TimeSpan Runtime { get; }
        int TurnsBeforeSelection { get; set; }
        int Time { get; }

        [NotNull]
        ITrailHistory TrailHistory { get; }

        int TurnsSinceNewBestTrailFound { get; }
        int TurnsRemaining { get; }
        DateTime StartTime { get; }
        DateTime FinishTime { get; }
        double PheromonesMinimum { get; }
        double PheromonesMaximum { get; }
        double PheromonesAverage { get; }
        event EventHandler <BestTrailChangedEventArgs> BestTrailChanged;
        event EventHandler <FinishedEventArgs> Finished;

        [NotNull]
        PheromonesInformation PheromonesInformation();

        [NotNull]
        double[][] PheromonesToArray();

        void Start(int times);
        event EventHandler Started;

        void Stop();
        event EventHandler Stopped;
    }
}