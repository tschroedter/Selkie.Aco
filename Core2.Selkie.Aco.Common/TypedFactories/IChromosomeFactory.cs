using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Common.TypedFactories
{
    public interface IChromosomeFactory : ITypedFactory
    {
        [NotNull]
        [UsedImplicitly]
        IChromosome Create(double alpha,
                           double beta,
                           double gamma);

        [NotNull]
        [UsedImplicitly]
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