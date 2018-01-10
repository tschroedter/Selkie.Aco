using System;
using Core2.Selkie.Aco.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Ants.Interfaces
{
    public interface ICrossover : IDisposable
    {
        [NotNull]
        IChromosome Mutation([NotNull] IChromosome chromosome);

        [NotNull]
        IChromosome Offspring([NotNull] IChromosome male,
                              [NotNull] IChromosome female);
    }
}