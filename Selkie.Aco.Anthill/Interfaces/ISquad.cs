using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;

namespace Selkie.Aco.Anthill.Interfaces
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