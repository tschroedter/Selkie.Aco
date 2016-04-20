using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;
using Selkie.Windsor;

namespace Selkie.Aco.Common.TypedFactories
{
    public interface IChromosomeFactory : ITypedFactory
    {
        [NotNull]
        IChromosome Create(double alpha,
                           double beta,
                           double gamma);

        [NotNull]
        // ReSharper disable TooManyArguments
        IChromosome Create(double alpha,
                           double beta,
                           double gamma,
                           double alphaMinValue,
                           double alphaMaxValue,
                           double betaMinValue,
                           double betaMaxValue,
                           double gammaMinValue,
                           double gammaMaxValue);

        // ReSharper restore TooManyArguments
        [UsedImplicitly]
        void Release([NotNull] IChromosome chromosome);
    }
}