using System;
using JetBrains.Annotations;
using Selkie.Aco.Common;

namespace Selkie.Aco.Ants
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