using JetBrains.Annotations;
using Selkie.Common;
using Selkie.Windsor;

namespace Selkie.Aco.Common
{
    [ProjectComponent(Lifestyle.Transient)]
    public class PheromonesTracker : IPheromonesTracker
    {
        private const double InitialValueFactor = 1.25;
        internal const double RhoMinimumValue = 0.005;
        internal const double RhoMaximumValue = 0.1;
        internal const double QMinimumValue = 2.0;
        internal const double QMaximumValue = 5.0;
        private const double QRandomFactor = QMaximumValue - QMinimumValue;
        private const double RhoRandomFactor = RhoMaximumValue - RhoMinimumValue;
        private readonly IDistanceGraph m_Graph;
        private readonly double m_InitialValue;
        private readonly double m_MaximumValue;
        private readonly double m_MinimumValue;
        private readonly IPheromones m_Pheromones;
        private readonly IRandom m_Random;
        private double m_Q = 2.0; // pheromone increase factor
        private double m_Rho = 0.005; // pheromone decrease factor

        public PheromonesTracker([NotNull] IRandom random,
                                 [NotNull] IPheromones pheromones,
                                 [NotNull] IDistanceGraph graph)
        {
            m_Random = random;
            m_Graph = graph;
            m_Pheromones = pheromones;
            m_MinimumValue = m_Q / ( m_Graph.MaximumDistance * m_Graph.NumberOfUniqueNodes );
            m_MaximumValue = m_Q / ( m_Graph.MinimumDistance * m_Graph.NumberOfUniqueNodes );
            m_InitialValue = ( m_MaximumValue + m_MinimumValue ) / InitialValueFactor;

            var information = new InitializeInformation
                              {
                                  NumberOfNodes = m_Graph.NumberOfNodes,
                                  Rho = m_Rho,
                                  Q = m_Q,
                                  MinimumValue = m_MinimumValue,
                                  MaximumValue = m_MaximumValue,
                                  InitialValue = m_InitialValue
                              };

            m_Pheromones.Initialize(information);
        }

        public PheromonesTracker([NotNull] IRandom random,
                                 [NotNull] IDistanceGraph graph,
                                 [NotNull] IPheromones pheromones,
                                 double rho,
                                 double q)
        {
            m_Random = random;
            m_Graph = graph;
            m_Pheromones = pheromones;
            m_Rho = rho;
            m_Q = q;
            m_MinimumValue = m_Q / ( m_Graph.MaximumDistance * m_Graph.NumberOfUniqueNodes );
            m_MaximumValue = m_Q / ( m_Graph.MinimumDistance * m_Graph.NumberOfUniqueNodes );
            m_InitialValue = ( m_MaximumValue + m_MinimumValue ) / InitialValueFactor;

            var information = new InitializeInformation
                              {
                                  NumberOfNodes = m_Graph.NumberOfNodes,
                                  Rho = m_Rho,
                                  Q = m_Q,
                                  MinimumValue = m_MinimumValue,
                                  MaximumValue = m_MaximumValue,
                                  InitialValue = m_InitialValue
                              };

            m_Pheromones.Initialize(information);
        }

        public double InitialValue
        {
            get
            {
                return m_InitialValue;
            }
        }

        public int NumberOfNodes
        {
            get
            {
                return m_Pheromones.NumberOfNodes;
            }
        }

        public double MinimumValue
        {
            get
            {
                return m_MinimumValue;
            }
        }

        public double MaximumValue
        {
            get
            {
                return m_MaximumValue;
            }
        }

        public double AverageValue
        {
            get
            {
                return m_Pheromones.CalculateAverageValue();
            }
        }

        public double Rho
        {
            get
            {
                return m_Rho;
            }
        }

        public double Q
        {
            get
            {
                return m_Q;
            }
        }

        public double GetValue(int indexFrom,
                               int indexTo)
        {
            return m_Pheromones.GetValue(indexFrom,
                                         indexTo);
        }

        public double[][] PheromonesToArray()
        {
            return m_Pheromones.ToArray();
        }

        public void Update(IAnt[] ants)
        {
            for ( var i = 0 ; i < m_Pheromones.NumberOfNodes ; ++i )
            {
                for ( int j = i + 1 ; j < m_Pheromones.NumberOfNodes ; ++j )
                {
                    foreach ( IAnt ant in ants )
                    {
                        m_Pheromones.UpdateForAnt(ant,
                                                  j,
                                                  i);
                    }
                }
            }
        }

        public void Update(IAnt ant)
        {
            for ( var i = 0 ; i < m_Pheromones.NumberOfNodes ; ++i )
            {
                for ( int j = i + 1 ; j < m_Pheromones.NumberOfNodes ; ++j )
                {
                    m_Pheromones.UpdateForAnt(ant,
                                              j,
                                              i);
                }
            }
        }

        public void Clear()
        {
            var information = new InitializeInformation
                              {
                                  NumberOfNodes = m_Graph.NumberOfNodes,
                                  Rho = m_Rho,
                                  Q = m_Q,
                                  MinimumValue = m_MinimumValue,
                                  MaximumValue = m_MaximumValue,
                                  InitialValue = m_InitialValue
                              };

            m_Pheromones.Initialize(information);
        }

        public void Randomize()
        {
            m_Rho = m_Random.NextDouble() * RhoRandomFactor + RhoMinimumValue;
            m_Q = m_Random.NextDouble() * QRandomFactor + QMinimumValue;
        }
    }
}