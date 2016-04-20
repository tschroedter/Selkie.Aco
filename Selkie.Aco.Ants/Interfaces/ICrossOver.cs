using System;
using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;

namespace Selkie.Aco.Ants.Interfaces
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