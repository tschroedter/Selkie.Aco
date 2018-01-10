using System.Diagnostics.CodeAnalysis;
using Core2.Selkie.Aco.Anthill.Interfaces;
using Core2.Selkie.Aco.Anthill.TypedFactories;
using Core2.Selkie.Aco.Trails;
using Core2.Selkie.Common;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill.Tests
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class TestNaturalSelectionFactory : INaturalSelectionFactory
    {
        public INaturalSelection Create(IQueen queen)
        {
            var naturalSelection = new NaturalSelection(new SelkieRandom(),
                                                        new TrailHistory(),
                                                        queen);

            return naturalSelection;
        }

        public void Release(INaturalSelection squad)
        {
        }
    }
}