using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Common;
using Selkie.Windsor.Extensions;

namespace Selkie.Aco.Trails
{
    public abstract class BaseTrailBuilder <T> : ITrailBuilder
        where T : ITrailBuilder
    {
        private const double PredefinedTendencyMinimum = 1E-50;
        public const int UnknownId = -1;
        // ReSharper disable once StaticMemberInGenericType
        private static int s_NextId; // means NextId per <T>
        private readonly IChromosome m_Chromosome;
        private readonly IDistanceGraph m_Graph;
        private readonly Dictionary <int, int> m_IndexOfTargets = new Dictionary <int, int>();
        private readonly IOptimizer m_Optimizer;
        private readonly IRandom m_Random;
        private readonly double m_TendencyMaximum;
        private readonly double m_TendencyMinimum;
        private readonly IPheromonesTracker m_Tracker;
        private double m_Length = double.MaxValue;
        private int[] m_Trail = new int[0];
        // ReSharper disable once TooManyDependencies
        protected BaseTrailBuilder([NotNull] IRandom random,
                                   [NotNull] IChromosome chromosome,
                                   [NotNull] IPheromonesTracker tracker,
                                   [NotNull] IDistanceGraph graph,
                                   [NotNull] IOptimizer optimizer)
        {
            Id = s_NextId++;
            m_Random = random;
            m_Chromosome = chromosome;
            m_Tracker = tracker;
            m_Graph = graph;
            m_Optimizer = optimizer;

            if ( m_Graph.NumberOfNodes == 0 ||
                 m_Graph.NumberOfNodes % 2 != 0 )
            {
                throw new ArgumentException("NumberOfNodes = " + m_Graph.NumberOfNodes);
            }

            if ( m_Graph.MinimumDistance <= 0.0 )
            {
                throw new ArgumentException("MinimumDistance = " + m_Graph.MinimumDistance);
            }

            m_TendencyMinimum = Math.Pow(m_Tracker.MinimumValue,
                                         m_Chromosome.Alpha) *
                                Math.Pow(m_Chromosome.Gamma / m_Graph.MaximumDistance,
                                         m_Chromosome.Beta);
            m_TendencyMaximum = double.MaxValue / ( graph.MinimumDistance * 100.0 );

            if ( m_TendencyMinimum < PredefinedTendencyMinimum ||
                 double.IsNaN(m_TendencyMinimum) )
            {
                m_TendencyMinimum = PredefinedTendencyMinimum;
            }
        }

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

        public IChromosome Chromosome
        {
            get
            {
                return m_Chromosome;
            }
        }

        #region IEquatable<BaseTrailBuilder> Members

        // ReSharper disable once CodeAnnotationAnalyzer
        public bool Equals(BaseTrailBuilder <T> other)
        {
            if ( ReferenceEquals(null,
                                 other) )
            {
                return false;
            }
            if ( ReferenceEquals(this,
                                 other) )
            {
                return true;
            }
            return Equals(other.m_Chromosome,
                          m_Chromosome) && Equals(other.Type,
                                                  Type);
        }

        #endregion

        internal abstract void BuildTrail(int startNode);

        protected void BuildDictionaryIndexOfTarget([NotNull] IEnumerable <int> trail)
        {
            m_IndexOfTargets.Clear();

            int[] trailArray = trail.ToArray();

            for ( var index = 0 ; index < trailArray.Length ; index++ )
            {
                m_IndexOfTargets.Add(trailArray [ index ],
                                     index);
            }
        }

        internal int IndexOfTarget(int target)
        {
            int index;

            if ( m_IndexOfTargets.TryGetValue(target,
                                              out index) )
            {
                return index;
            }

            return -1;
        }

        internal double CalculateLength([NotNull] IEnumerable <int> trail)
        {
            double result = m_Graph.Length(trail);

            return result;
        }

        public static int FindRelatedCity(int cityIndex)
        {
            bool isEndPoint = cityIndex % 2 == 1;

            if ( isEndPoint )
            {
                return cityIndex - 1;
            }

            return cityIndex + 1;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            string value = "Length: {0:D4}".Inject(( int ) m_Length);

            sb.Append(value);
            sb.Append(" [");

            sb.Append(string.Join(" ",
                                  m_Trail));

            sb.Append("]");

            return sb.ToString();
        }

        [NotNull]
        internal static double[] CalculateCuMul([NotNull] double[] probs)
        {
            var cumul = new double[probs.Length + 1];

            for ( var i = 0 ; i < probs.Length ; ++i )
            {
                cumul [ i + 1 ] = cumul [ i ] + probs [ i ];
            }

            return cumul;
        }

        [NotNull]
        internal double[] CalculateProbabilities(int cityX,
                                                 [NotNull] bool[] visited)
        {
            double[] tendency = CalculateTendencies(cityX,
                                                    visited);

            double[] probs = CreateProbabilities(tendency);

            return probs;
        }

        [NotNull]
        private double[] CalculateTendencies(int cityX,
                                             [NotNull] bool[] visited)
        {
            int numCities = m_Graph.NumberOfNodes;

            var tendency = new double[numCities];

            for ( var otherCity = 0 ; otherCity < tendency.Length ; ++otherCity )
            {
                tendency [ otherCity ] = CalculateTendency(cityX,
                                                           otherCity,
                                                           visited);
            }
            return tendency;
        }

        private double CalculateTendency(int cityX,
                                         int cityOther,
                                         [NotNull] bool[] visited)
        {
            if ( cityOther == cityX )
            {
                return 0.0;
            }

            if ( visited [ cityOther ] )
            {
                return 0.0;
            }

            // moving to an unvisited node
            double tendencyOther = Math.Pow(m_Tracker.GetValue(cityX,
                                                               cityOther),
                                            m_Chromosome.Alpha) * Math.Pow(m_Chromosome.Gamma / m_Graph.GetCost(cityX,
                                                                                                                cityOther),
                                                                           m_Chromosome.Beta);

            if ( tendencyOther < m_TendencyMinimum ||
                 double.IsNaN(tendencyOther) )
            {
                return m_TendencyMinimum;
            }

            if ( tendencyOther > m_TendencyMaximum )
            {
                tendencyOther = m_TendencyMaximum;
            }

            return tendencyOther;
        }

        [NotNull]
        private double[] CreateProbabilities([NotNull] double[] values)
        {
            int numCities = m_Graph.NumberOfNodes;
            var probs = new double[numCities];
            double sum = values.Sum();

            for ( var i = 0 ; i < probs.Length ; ++i )
            {
                probs [ i ] = values [ i ] / sum;
            }

            return probs;
        }

        // ReSharper disable once CodeAnnotationAnalyzer
        public override bool Equals(object obj)
        {
            if ( ReferenceEquals(null,
                                 obj) )
            {
                return false;
            }
            if ( ReferenceEquals(this,
                                 obj) )
            {
                return true;
            }
            if ( obj.GetType() != typeof ( BaseTrailBuilder <T> ) )
            {
                return false;
            }
            return Equals(( BaseTrailBuilder <T> ) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int code = m_Chromosome != null
                               ? m_Chromosome.GetHashCode()
                               : 0;

                return ( code * 397 ) ^ Type.GetHashCode();
            }
        }

        public static bool operator ==(BaseTrailBuilder <T> left,
                                       BaseTrailBuilder <T> right)
        {
            return Equals(left,
                          right);
        }

        public static bool operator !=(BaseTrailBuilder <T> left,
                                       BaseTrailBuilder <T> right)
        {
            return !Equals(left,
                           right);
        }

        #region ITrailBuilder Members

        public void Build(int startNode)
        {
            ValidateStartNode(startNode);
            BuildTrail(startNode); // todo better m_Trail =...

            Optimize(); // todo better m_Trail =...
            BuildIndexOfTarget(); // todo better m_Trail =...
        }

        private void Optimize()
        {
            IEnumerable <int> optimizedTrail = m_Optimizer.Optimize(m_Trail);
            m_Trail = optimizedTrail.ToArray();
            m_Length = CalculateLength(m_Trail);
        }

        private void ValidateStartNode(int startNode)
        {
            if ( startNode < 0 )
            {
                throw new ArgumentException("startNode '{0}' is less than zero!".Inject(startNode));
            }

            if ( startNode >= DistanceGraph.NumberOfNodes )
            {
                throw new ArgumentException("startNode '{0}' is equal or greater than " +
                                            "number of graph nodes '{1}'!".Inject(startNode,
                                                                                  DistanceGraph.NumberOfNodes));
            }
        }

        private void BuildIndexOfTarget()
        {
            BuildDictionaryIndexOfTarget(m_Trail);
        }

        public string Type
        {
            get
            {
                return typeof ( T ).Name;
            }
        }

        public int Id { get; protected set; }

        public bool IsUnknown
        {
            get
            {
                return Id == UnknownId;
            }
        }

        public IEnumerable <int> Trail
        {
            get
            {
                return m_Trail;
            }
            protected set
            {
                m_Trail = value.ToArray();
            }
        }

        public double Length
        {
            get
            {
                return m_Length;
            }
            protected set
            {
                m_Length = value;
            }
        }

        // todo maybe this is not required because all of them are connected
        // ReSharper disable once MethodTooLong
        public virtual bool EdgeInTrail(int cityX,
                                        int cityY)
        {
            // are cityX and cityY adjacent to each other in trail[]?
            int idx = IndexOfTarget(cityX);

            if ( idx == -1 )
            {
                return false;
            }

            if ( idx == 0 &&
                 m_Trail [ 1 ] == cityY )
            {
                return true;
            }

            int lastIndex = m_Trail.Length - 1;

            if ( idx == 0 &&
                 m_Trail [ lastIndex ] == cityY )
            {
                return true;
            }
            if ( idx == 0 )
            {
                return false;
            }
            if ( idx == lastIndex &&
                 m_Trail [ lastIndex - 1 ] == cityY )
            {
                return true;
            }
            if ( idx == lastIndex &&
                 m_Trail [ 0 ] == cityY )
            {
                return true;
            }
            if ( idx == lastIndex )
            {
                return false;
            }
            if ( m_Trail [ idx - 1 ] == cityY )
            {
                return true;
            }
            if ( m_Trail [ idx + 1 ] == cityY )
            {
                return true;
            }

            return false;
        }

        public virtual ITrailBuilder Clone(ITrailBuilderFactory trailBuilderFactory,
                                           IChromosomeFactory chromosomeFactory)
        {
            var builder = trailBuilderFactory.Create <T>(Chromosome.Clone(chromosomeFactory),
                                                         m_Tracker,
                                                         DistanceGraph,
                                                         m_Optimizer,
                                                         Trail.ToArray());

            return builder;
        }

        #endregion
    }
}