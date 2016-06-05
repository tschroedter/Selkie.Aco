using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;

namespace Selkie.Aco.Anthill.Interfaces
{
    public interface IQueen
    {
        [NotNull]
        IEnumerable <IAnt> Ants { get; }

        int NumberOfAnts { get; }

        [NotNull]
        IEnumerable <ITrailBuilder> Alternatives { get; }

        int NumberOfNodes { get; }

        [NotNull]
        ISettings Settings { get; }

        [NotNull]
        ITrailBuilder BestTrailBuilder { get; }

        void Clear();

        void NaturalSelection([NotNull] IChromosome male,
                              [NotNull] IChromosome female);

        void RandomSelection();

        void RestartFromTrail([NotNull] IEnumerable <int> trail);

        void UpdateAnts();
    }
}