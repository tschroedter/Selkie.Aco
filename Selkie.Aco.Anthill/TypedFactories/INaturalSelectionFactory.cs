using JetBrains.Annotations;
using Selkie.Aco.Anthill.Interfaces;
using Selkie.Windsor;

namespace Selkie.Aco.Anthill.TypedFactories
{
    public interface INaturalSelectionFactory : ITypedFactory
    {
        [NotNull]
        INaturalSelection Create([NotNull] IQueen queen);

        [UsedImplicitly]
        void Release([NotNull] INaturalSelection squad);
    }
}