using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using Selkie.Aco.Anthill.Interfaces;
using Selkie.Aco.Anthill.TypedFactories;
using Selkie.Aco.Ants;
using Selkie.Aco.Ants.Interfaces;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Common;
using Selkie.Windsor;

namespace Selkie.Aco.Anthill.Tests.NUnit
{
    [ExcludeFromCodeCoverage]
    internal sealed class TestQueenFactory : IQueenFactory
    {
        private readonly IAntFactory m_AntFactory;
        private readonly IBestTrailFinderFactory m_BestTrailFinderFactory;
        private readonly TestChromosomeFactory m_ChromosomeFactory;
        private readonly ICrossover m_Crossover;
        private readonly ISelkieLogger m_Logger;
        private readonly IRandom m_Random;
        private readonly ISquadFactory m_SquadFactory;

        public TestQueenFactory()
        {
            var disposer = new Disposer();
            m_Random = new SelkieRandom();
            m_AntFactory = new TestAntFactory();
            m_ChromosomeFactory = new TestChromosomeFactory();
            m_BestTrailFinderFactory = new TestBestTrailFinderFactory();
            m_Logger = Substitute.For <ISelkieLogger>();
            m_SquadFactory = Substitute.For <ISquadFactory>();

            m_Crossover = new Crossover(disposer,
                                        m_Logger,
                                        m_Random,
                                        Substitute.For <IChromosomeFactory>());
        }

        public IQueen Create(IDistanceGraph graph,
                             IPheromonesTracker tracker,
                             IOptimizer optimizer,
                             IAntSettings antSettings)
        {
            var nest = new Squad(new Disposer(),
                                 m_Logger,
                                 m_Random,
                                 m_AntFactory,
                                 graph,
                                 tracker,
                                 optimizer,
                                 antSettings);

            ISquad squad = m_SquadFactory.Create(Arg.Any <IDistanceGraph>(),
                                                 Arg.Any <IPheromonesTracker>(),
                                                 Arg.Any <IOptimizer>(),
                                                 Arg.Any <IAntSettings>());
            squad.Returns(nest);

            return new Queen(m_Logger,
                             m_AntFactory,
                             m_ChromosomeFactory,
                             m_BestTrailFinderFactory,
                             graph,
                             tracker,
                             optimizer,
                             m_Crossover,
                             antSettings,
                             m_SquadFactory);
        }

        public void Release(IQueen queen)
        {
        }
    }
}