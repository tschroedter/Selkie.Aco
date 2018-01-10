using System;
using System.Collections.Generic;
using Core2.Selkie.Aco.Common.Interfaces;
using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global

namespace Core2.Selkie.Aco.Anthill.Interfaces
{
    public interface ISquad : IDisposable
    {
        int NumberOfAnts { get; }

        [NotNull]
        IEnumerable <IAnt> Ants { get; }

        IEnumerable <IAnt> BestAnts { get; }

        void AddBestAnt([NotNull] IAnt ant);
        void Clear();

        void Restart();
        void SetNumberOfAnts(int numberOfAnts);
    }
}