using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Common;

namespace Selkie.Aco.Ants
{
    public abstract class BaseAnt <TAnt, TBuilder> : IAnt
        where TAnt : IAnt
        where TBuilder : ITrailBuilder
    {
        // ReSharper disable once StaticMemberInGenericType
        private static int s_NextId; // means s_NextId per <TAnt, TBuilder>
        private readonly IDistanceGraph m_Graph;
        private readonly int m_Id;
        private readonly IOptimizer m_Optimizer;
        private readonly IRandom m_Random;
        private readonly IPheromonesTracker m_Tracker;
        // ReSharper disable TooManyDependencies
        protected BaseAnt([NotNull] IRandom random,
                          [NotNull] ITrailBuilderFactory trailBuilderFactory,
                          [NotNull] IChromosome chromosome,
                          [NotNull] IPheromonesTracker tracker,
                          [NotNull] IDistanceGraph graph,
                          [NotNull] IOptimizer optimizer,
                          [NotNull] IEnumerable <int> trail)
        {
            m_Id = s_NextId++;
            m_Random = random;
            Chromosome = chromosome;
            m_Tracker = tracker;
            m_Graph = graph;
            m_Optimizer = optimizer;
            TrailBuilder = trailBuilderFactory.Create <TBuilder>(Chromosome,
                                                                 m_Tracker,
                                                                 DistanceGraph,
                                                                 m_Optimizer,
                                                                 trail);
        }

        // ReSharper restore TooManyDependencies
        [NotNull]
        protected IRandom Random
        {
            get
            {
                return m_Random;
            }
        }

        [NotNull]
        protected IDistanceGraph DistanceGraph
        {
            get
            {
                return m_Graph;
            }
        }

        public double Alpha
        {
            get
            {
                return Chromosome.Alpha;
            }
        }

        public double Beta
        {
            get
            {
                return Chromosome.Beta;
            }
        }

        public double Gamma
        {
            get
            {
                return Chromosome.Gamma;
            }
        }

        public string Type
        {
            get
            {
                return typeof ( TAnt ).Name;
            }
        }

        public int Id
        {
            get
            {
                return m_Id;
            }
        }

        public IChromosome Chromosome { get; set; }
        public ITrailBuilder TrailBuilder { get; protected set; }
        public abstract void Update();

        public virtual IAnt Clone(IAntFactory antFactory,
                                  IChromosomeFactory chromosomeFactory)
        {
            var clone = antFactory.Create <TAnt>(Chromosome.Clone(chromosomeFactory),
                                                 m_Tracker,
                                                 DistanceGraph,
                                                 m_Optimizer,
                                                 TrailBuilder.Trail.ToArray());

            return clone;
        }

        public void RandomizeChromosome()
        {
            Chromosome = Chromosome.Randomize();
        }
    }
}