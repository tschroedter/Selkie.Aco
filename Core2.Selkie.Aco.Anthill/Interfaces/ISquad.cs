using System;
using System.Collections.Generic;
using Core2.Selkie.Aco.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill.Interfaces
{
    public interface ISquad : IDisposable
    {
        int NumberOfAnts { get; }

        [NotNull]
        IEnumerable <IAnt> Ants { get; }

        void AddBestAnt([NotNull] IAnt ant);
        void Clear();

        void Restart();
    }
}