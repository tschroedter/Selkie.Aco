using System.Diagnostics.CodeAnalysis;
using Core2.Selkie.Aco.Anthill.Interfaces;
using Core2.Selkie.Aco.Anthill.TypedFactories;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Common;

namespace Core2.Selkie.Aco.Anthill.Tests
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