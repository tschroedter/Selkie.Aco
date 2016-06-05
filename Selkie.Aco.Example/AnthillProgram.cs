using System;
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using JetBrains.Annotations;
using Selkie.Aco.Anthill;
using Selkie.Aco.Anthill.Interfaces;
using Selkie.Aco.Anthill.TypedFactories;
using Selkie.Aco.Common.Interfaces;
using Selkie.Common.Interfaces;
using Selkie.Windsor.Extensions;

namespace Selkie.Aco.Example
{
    public sealed class AnthillProgram : IDisposable
    {
        public AnthillProgram([NotNull] IWindsorContainer container,
                              [NotNull] IWindsorInstaller installer)
        {
            m_Container = container;
            m_Container.Install(installer);

            m_ColonyFactory = container.Resolve <IColonyFactory>();
            m_GraphFactory = container.Resolve <IDistanceGraphFactory>();
            m_AntSettingsFactory = container.Resolve <IAntSettingsFactory>();
            m_Console = container.Resolve <ISelkieConsole>();
        }

        private readonly IAntSettingsFactory m_AntSettingsFactory;
        private readonly IColonyFactory m_ColonyFactory;
        private readonly ISelkieConsole m_Console;
        private readonly IWindsorContainer m_Container;

        private readonly int[][] m_CostMatrix =
        {
            new[]
            {
                0,
                0,
                10,
                20
            },
            new[]
            {
                0,
                0,
                20,
                20
            },
            new[]
            {
                10,
                20,
                0,
                0
            },
            new[]
            {
                20,
                20,
                0,
                0
            }
        };

        private readonly int[] m_CostPerLine =
        {
            2000,
            20,
            2000,
            20
        };

        private readonly IDistanceGraphFactory m_GraphFactory;
        private bool m_IsFinished;

        public void Dispose()
        {
            m_Container.Release(m_ColonyFactory);
            m_Container.Release(m_GraphFactory);
            m_Container.Release(m_Console);
        }

        public void Main()
        {
            for ( var i = 0 ; i < 1000 ; i++ )
            {
                m_Console.WriteLine("{0} Create new colony...".Inject(i));

                IDistanceGraph graph = m_GraphFactory.Create(m_CostMatrix,
                                                             m_CostPerLine);

                IAntSettings settings = m_AntSettingsFactory.Create(AntSettings.TrailStartNodeType.Random,
                                                                    0);

                IColony colony = m_ColonyFactory.Create(graph,
                                                        settings);

                colony.BestTrailChanged += BestTrailChangedHandler;
                colony.Finished += FinishHandler;

                colony.Start(1000);

                do
                {
                    Thread.Sleep(1000);
                }
                while ( !m_IsFinished );

                m_Console.WriteLine("IsFinished: {0}".Inject(m_IsFinished));

                colony.Finished -= FinishHandler;
                colony.BestTrailChanged -= BestTrailChangedHandler;

                m_AntSettingsFactory.Release(settings);
                m_ColonyFactory.Release(colony);
                m_GraphFactory.Release(graph);
            }

            m_Console.ReadLine();
        }

        private void BestTrailChangedHandler([NotNull] object sender,
                                             [NotNull] BestTrailChangedEventArgs eventArgs)
        {
            string trail = string.Join(",",
                                       eventArgs.Trail);

            string text = "Length: {0} Trail: [{1}]".Inject(eventArgs.Length,
                                                            trail);

            m_Console.WriteLine(text);
        }

        private void FinishHandler([NotNull] object sender,
                                   [NotNull] FinishedEventArgs eventArgs)
        {
            m_IsFinished = true;
        }
    }
}