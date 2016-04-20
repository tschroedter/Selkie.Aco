using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Trails.Interfaces;

// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable EventNeverSubscribedTo.Global

namespace Selkie.Aco.Anthill.Interfaces
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
        void Start(int times);
        event EventHandler <BestTrailChangedEventArgs> BestTrailChanged;
        event EventHandler <FinishedEventArgs> Finished;

        [NotNull]
        PheromonesInformation PheromonesInformation();

        void Stop();
        event EventHandler Stopped;
        event EventHandler Started;

        [NotNull]
        double[][] PheromonesToArray();
    }
}