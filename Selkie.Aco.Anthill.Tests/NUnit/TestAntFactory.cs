using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Selkie.Aco.Ants;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Common;
using Selkie.Windsor.Extensions;

namespace Selkie.Aco.Anthill.Tests.NUnit
{
    [ExcludeFromCodeCoverage]
    public class TestAntFactory : IAntFactory
    {
        private readonly IRandom m_Random = new SelkieRandom();
        private readonly IList <IAnt> m_ReleasedAnts = new List <IAnt>();
        private readonly ITrailBuilderFactory m_TrailBuilderFactory = new TestTrailBuilderFactory();
        // ReSharper disable once MethodTooLong
        // ReSharper disable once TooManyArguments
        public T Create <T>(IChromosome chromosome,
                            IPheromonesTracker tracker,
                            IDistanceGraph graph,
                            IOptimizer optimizer,
                            IEnumerable <int> trail) where T : IAnt
        {
            lock ( this )
            {
                object[] args =
                {
                    m_Random,
                    m_TrailBuilderFactory,
                    chromosome,
                    tracker,
                    graph,
                    optimizer,
                    trail
                };

                var ant = CreateAntInstance <T>(args);

                return ant;
            }
        }

        public void Release(IAnt ant)
        {
            lock ( this )
            {
                m_ReleasedAnts.Add(ant);
            }
        }

        // ReSharper disable TooManyArguments
        public T Create <T>(IChromosome chromosome,
                            IPheromonesTracker tracker,
                            IDistanceGraph graph,
                            IOptimizer optimizer) where T : IAnt
        {
            lock ( this )
            {
                object[] args =
                {
                    m_Random,
                    m_TrailBuilderFactory,
                    chromosome,
                    tracker,
                    graph,
                    optimizer,
                    new int[0]
                };

                var ant = CreateAntInstance <T>(args);

                return ant;
            }
        }

        // ReSharper restore TooManyArguments
        // ReSharper disable once MethodTooLong
        // ReSharper disable once ExcessiveIndentation
        private static T CreateAntInstance <T>([NotNull] object[] args) where T : IAnt
        {
            T ant;

            if ( typeof ( T ).Name.Contains("IFixedAnt") )
            {
                ant = ( T ) Activator.CreateInstance(typeof ( FixedAnt ),
                                                     args);
            }
            else if ( typeof ( T ).Name.Contains("ICandidateListAnt") )
            {
                ant = ( T ) Activator.CreateInstance(typeof ( CandidateListAnt ),
                                                     args);
            }
            else if ( typeof ( T ).Name.Contains("IUnknownAnt") )
            {
                ant = ( T ) Activator.CreateInstance(typeof ( UnknownAnt ),
                                                     args);
            }
            else if ( typeof ( T ).Name.Contains("IStandardAnt") )
            {
                ant = ( T ) Activator.CreateInstance(typeof ( StandardAnt ),
                                                     args);
            }
            else
            {
                throw new ArgumentException("Unknown IAnt interface '{0}'!".Inject(typeof ( T )));
            }
            return ant;
        }

        public bool Released([NotNull] IAnt ant)
        {
            lock ( this )
            {
                return m_ReleasedAnts.Contains(ant);
            }
        }
    }
}