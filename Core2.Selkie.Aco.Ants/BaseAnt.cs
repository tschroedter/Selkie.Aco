using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Common.TypedFactories;
using Core2.Selkie.Common.Interfaces;
using JetBrains.Annotations;

[assembly: InternalsVisibleTo("Core2.Selkie.Aco.Ants.Tests")]

namespace Core2.Selkie.Aco.Ants
{
    public abstract class BaseAnt <TAnt, TBuilder> : IAnt
        where TAnt : IAnt
        where TBuilder : ITrailBuilder
    {
        // ReSharper disable TooManyDependencies
        protected BaseAnt([NotNull] IRandom random,
                          [NotNull] ITrailBuilderFactory trailBuilderFactory,
                          [NotNull] IChromosome chromosome,
                          [NotNull] IPheromonesTracker tracker,
                          [NotNull] IDistanceGraph graph,
                          [NotNull] IOptimizer optimizer,
                          [NotNull] IAntSettings antSettings,
                          [NotNull] IEnumerable <int> trail)
        {
            Type = typeof( TAnt ).Name;
            Id = s_NextId++;
            Random = random;
            Chromosome = chromosome;
            m_Tracker = tracker;
            DistanceGraph = graph;
            m_Optimizer = optimizer;
            AntSettings = antSettings;
            TrailBuilder = trailBuilderFactory.Create <TBuilder>(Chromosome,
                                                                 m_Tracker,
                                                                 DistanceGraph,
                                                                 m_Optimizer,
                                                                 trail);
        }

        // ReSharper restore TooManyDependencies
        // ReSharper disable once StaticMemberInGenericType
        private static int s_NextId; // means s_NextId per <TAnt, TBuilder>

        [NotNull]
        protected IAntSettings AntSettings { get; private set; }

        [NotNull]
        protected IRandom Random { get; private set; }

        [NotNull]
        protected IDistanceGraph DistanceGraph { get; private set; }

        public double Alpha => Chromosome.Alpha;

        public double Beta => Chromosome.Beta;

        public double Gamma => Chromosome.Gamma;

        private readonly IOptimizer m_Optimizer;
        private readonly IPheromonesTracker m_Tracker;

        public string Type { get; private set; }

        public int Id { get; private set; }

        public IChromosome Chromosome { get; set; }
        public ITrailBuilder TrailBuilder { get; protected set; }

        public void Update()
        {
            if ( AntSettings.IsFixedStartNode )
            {
                UpdateWithFixedStartNode(AntSettings.FixedStartNode);
            }
            else
            {
                UpdateWithRandomStartNode();
            }
        }

        public virtual IAnt Clone(IAntFactory antFactory,
                                  IChromosomeFactory chromosomeFactory)
        {
            var clone = antFactory.Create <TAnt>(Chromosome.Clone(chromosomeFactory),
                                                 m_Tracker,
                                                 DistanceGraph,
                                                 m_Optimizer,
                                                 AntSettings,
                                                 TrailBuilder.Trail.ToArray());

            return clone;
        }

        public void RandomizeChromosome()
        {
            Chromosome = Chromosome.Randomize();
        }

        protected virtual void UpdateWithFixedStartNode(int startNode)
        {
            TrailBuilder.Build(startNode);
        }

        protected virtual void UpdateWithRandomStartNode()
        {
            int startNode = Random.Next(0,
                                        DistanceGraph.NumberOfNodes - 1);

            TrailBuilder.Build(startNode);
        }
    }
}