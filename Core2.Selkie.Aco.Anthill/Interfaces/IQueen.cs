using System.Collections.Generic;
using Core2.Selkie.Aco.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill.Interfaces
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