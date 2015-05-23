using System.Diagnostics.CodeAnalysis;
using Selkie.Aco.Anthill.TypedFactories;
using Selkie.Aco.Common;
using Selkie.Aco.Trails;
using Selkie.Common;

namespace Selkie.Aco.Anthill.Tests.NUnit
{
    [ExcludeFromCodeCoverage]
    internal sealed class TestBestTrailFinderFactory : IBestTrailFinderFactory
    {
        private readonly IAntFactory m_AntFactory = new TestAntFactory();

        private readonly ITrailAlternatives m_TrailAlternatives = new TrailAlternatives(new TestTrailBuilderFactory(),
                                                                                        new TestChromosomeFactory());

        public IBestTrailFinder Create(IDistanceGraph graph,
                                       IPheromonesTracker tracker,
                                       IOptimizer optimizer)
        {
            return new BestTrailFinder(new Disposer(),
                                       m_AntFactory,
                                       graph,
                                       tracker,
                                       optimizer,
                                       m_TrailAlternatives);
        }

        public void Release(IBestTrailFinder bestTrailFinder)
        {
        }
    }
}