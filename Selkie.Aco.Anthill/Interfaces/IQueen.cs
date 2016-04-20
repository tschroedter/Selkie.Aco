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

        void UpdateAnts();
        void Clear();
        void RandomSelection();

        void NaturalSelection([NotNull] IChromosome male,
                              [NotNull] IChromosome female);

        void RestartFromTrail([NotNull] IEnumerable <int> trail);
    }
}