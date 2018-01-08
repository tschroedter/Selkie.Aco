using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Common.Interfaces;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Common
{
    [ProjectComponent(Lifestyle.Transient)]
    [UsedImplicitly]
    public class PheromonesTracker : IPheromonesTracker
    {
        public PheromonesTracker([NotNull] IRandom random,
                                 [NotNull] IPheromones pheromones,
                                 [NotNull] IDistanceGraph graph)
        {
            m_Random = random;
            m_Graph = graph;
            m_Pheromones = pheromones;
            MinimumValue = Q / ( m_Graph.MaximumDistance * m_Graph.NumberOfUniqueNodes );
            MaximumValue = Q / ( m_Graph.MinimumDistance * m_Graph.NumberOfUniqueNodes );
            InitialValue = ( MaximumValue + MinimumValue ) / InitialValueFactor;

            InitializeInformation information = CreateInitializeInformation();

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
            Rho = rho;
            Q = q;
            MinimumValue = Q / ( m_Graph.MaximumDistance * m_Graph.NumberOfUniqueNodes );
            MaximumValue = Q / ( m_Graph.MinimumDistance * m_Graph.NumberOfUniqueNodes );
            InitialValue = ( MaximumValue + MinimumValue ) / InitialValueFactor;

            InitializeInformation information = CreateInitializeInformation();

            m_Pheromones.Initialize(information);
        }

        [UsedImplicitly]
        internal const double InitialValueFactor = 1.25;

        [UsedImplicitly]
        internal const double RhoMinimumValue = 0.005;

        [UsedImplicitly]
        internal const double RhoMaximumValue = 0.1;

        [UsedImplicitly]
        internal const double QMinimumValue = 2.0;

        [UsedImplicitly]
        internal const double QMaximumValue = 5.0;

        [UsedImplicitly]
        internal const double QRandomFactor = QMaximumValue - QMinimumValue;

        [UsedImplicitly]
        internal const double RhoRandomFactor = RhoMaximumValue - RhoMinimumValue;

        [UsedImplicitly]
        public double InitialValue { get; }

        [UsedImplicitly]
        public int NumberOfNodes => m_Pheromones.NumberOfNodes;

        private readonly IDistanceGraph m_Graph;
        private readonly IPheromones m_Pheromones;
        private readonly IRandom m_Random;

        public double MinimumValue { get; }

        public double MaximumValue { get; }

        public double AverageValue => m_Pheromones.CalculateAverageValue();

        public double Rho { get; private set; } = 0.005;

        public double Q { get; private set; } = 2.0;

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
                                  Rho = Rho,
                                  Q = Q,
                                  MinimumValue = MinimumValue,
                                  MaximumValue = MaximumValue,
                                  InitialValue = InitialValue
                              };

            m_Pheromones.Initialize(information);
        }

        [UsedImplicitly]
        public void Randomize()
        {
            Rho = m_Random.NextDouble() * RhoRandomFactor + RhoMinimumValue;
            Q = m_Random.NextDouble() * QRandomFactor + QMinimumValue;
        }

        private InitializeInformation CreateInitializeInformation()
        {
            return new InitializeInformation
                   {
                       NumberOfNodes = m_Graph.NumberOfNodes,
                       Rho = Rho,
                       Q = Q,
                       MinimumValue = MinimumValue,
                       MaximumValue = MaximumValue,
                       InitialValue = InitialValue
                   };
        }
    }
}