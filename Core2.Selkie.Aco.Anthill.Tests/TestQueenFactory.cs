using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using Core2.Selkie.Aco.Anthill.Interfaces;
using Core2.Selkie.Aco.Anthill.TypedFactories;
using Core2.Selkie.Aco.Ants;
using Core2.Selkie.Aco.Ants.Interfaces;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Common.TypedFactories;
using Core2.Selkie.Common;
using Core2.Selkie.Common.Interfaces;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill.Tests
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    internal sealed class TestQueenFactory : IQueenFactory
    {
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

        private readonly IAntFactory m_AntFactory;
        private readonly IBestTrailFinderFactory m_BestTrailFinderFactory;
        private readonly TestChromosomeFactory m_ChromosomeFactory;
        private readonly ICrossover m_Crossover;
        private readonly ISelkieLogger m_Logger;
        private readonly IRandom m_Random;
        private readonly ISquadFactory m_SquadFactory;

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