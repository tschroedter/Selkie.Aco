using Selkie.Aco.Anthill.Interfaces;
using Selkie.Aco.Anthill.TypedFactories;
using Selkie.Aco.Trails;
using Selkie.Common;

namespace Selkie.Aco.Anthill.Tests
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