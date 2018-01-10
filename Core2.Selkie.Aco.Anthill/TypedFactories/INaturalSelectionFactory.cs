using Core2.Selkie.Aco.Anthill.Interfaces;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill.TypedFactories
{
    public interface INaturalSelectionFactory : ITypedFactory
    {
        [NotNull]
        INaturalSelection Create([NotNull] IQueen queen);

        [UsedImplicitly]
        void Release([NotNull] INaturalSelection squad);
    }
}