using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Aco.Trails;
using Selkie.Common;
using Selkie.Windsor.Extensions;

namespace Selkie.Aco.Anthill.Tests.NUnit
{
    [ExcludeFromCodeCoverage]
    public class TestTrailBuilderFactory : ITrailBuilderFactory
    {
        // ReSharper disable once TooManyArguments
        public T Create <T>(IChromosome chromosome,
                            IPheromonesTracker tracker,
                            IDistanceGraph graph,
                            IOptimizer optimizer) where T : ITrailBuilder
        {
            object[] args =
            {
                new SelkieRandom(),
                chromosome,
                tracker,
                graph,
                optimizer
            };

            var trailBuilder = CreateTrailBuilderInstance <T>(args);

            return trailBuilder;
        }

        // ReSharper disable once MethodTooLong
        // ReSharper disable once ExcessiveIndentation

        // ReSharper disable once TooManyArguments
        public T Create <T>(IChromosome chromosome,
                            IPheromonesTracker tracker,
                            IDistanceGraph graph,
                            IOptimizer optimizer,
                            IEnumerable <int> trail) where T : ITrailBuilder
        {
            object[] args =
            {
                new SelkieRandom(),
                chromosome,
                tracker,
                graph,
                optimizer,
                trail
            };

            var trailBuilder = CreateTrailBuilderInstance <T>(args);

            return trailBuilder;
        }

        public void Release(ITrailBuilder trailBuilder)
        {
        }

        // ReSharper disable once ExcessiveIndentation
        // ReSharper disable once MethodTooLong
        private static T CreateTrailBuilderInstance <T>([NotNull] object[] args) where T : ITrailBuilder
        {
            T trailBuilder;

            if ( typeof ( T ).Name.Contains("IRandomTrailBuilder") )
            {
                trailBuilder = ( T ) Activator.CreateInstance(typeof ( RandomTrailBuilder ),
                                                              args);
            }
            else if ( typeof ( T ).Name.Contains("IFixedTrailBuilder") )
            {
                trailBuilder = ( T ) Activator.CreateInstance(typeof ( FixedTrailBuilder ),
                                                              args);
            }
            else if ( typeof ( T ).Name.Contains("ICandidateListTrailBuilder") )
            {
                trailBuilder = ( T ) Activator.CreateInstance(typeof ( CandidateListTrailBuilder ),
                                                              args);
            }
            else if ( typeof ( T ).Name.Contains("IUnknownTrailBuilder") )
            {
                trailBuilder = ( T ) Activator.CreateInstance(typeof ( UnknownTrailBuilder ),
                                                              args);
            }
            else if ( typeof ( T ).Name.Contains("IStandardTrailBuilder") )
            {
                trailBuilder = ( T ) Activator.CreateInstance(typeof ( StandardTrailBuilder ),
                                                              args);
            }
            else
            {
                throw new ArgumentException("Unknown ITrailBuilder interface '{0}'!".Inject(typeof ( T )));
            }

            return trailBuilder;
        }
    }
}