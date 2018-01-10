using Core2.Selkie.Aco.Anthill.Interfaces;
using Core2.Selkie.Aco.Anthill.TypedFactories;
using Core2.Selkie.Aco.Trails;
using Core2.Selkie.Common;

namespace Core2.Selkie.Aco.Anthill.Tests
{
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