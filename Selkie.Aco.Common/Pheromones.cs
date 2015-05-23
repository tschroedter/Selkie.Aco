using System;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Windsor;

namespace Selkie.Aco.Common
{
    [ProjectComponent(Lifestyle.Transient)]
    public class Pheromones : IPheromones
    {
        private double m_Q;
        private double m_Rho;
        private double[][] m_Values = new double[0][];
        public double InitialValue { get; private set; }
        public double MinimumValue { get; private set; }
        public double MaximumValue { get; private set; }
        public int NumberOfNodes { get; private set; }

        public double GetValue(int fromIndex,
                               int toIndex)
        {
            return m_Values [ fromIndex ] [ toIndex ];
        }

        public void SetValue(int fromIndex,
                             int toIndex,
                             double value)
        {
            m_Values [ fromIndex ] [ toIndex ] = value;
        }

        public void Initialize(InitializeInformation information)
        {
            NumberOfNodes = information.NumberOfNodes;
            m_Rho = information.Rho;
            m_Q = information.Q;
            MinimumValue = information.MinimumValue;
            MaximumValue = information.MaximumValue;
            InitialValue = information.InitialValue;
            m_Values = Create(NumberOfNodes,
                              InitialValue);
        }

        public double CalculateAverageValue()
        {
            double average = double.NegativeInfinity;

            foreach ( double[] values in m_Values )
            {
                double localAverage = values.Average();

                if ( double.IsNegativeInfinity(average) )
                {
                    average = localAverage;
                }
                else
                {
                    average = ( average + localAverage ) / 2.0;
                }
            }

            return average;
        }

        public double[][] ToArray()
        {
            var valuesList = new double[NumberOfNodes][];

            for ( var i = 0 ; i < NumberOfNodes ; i++ )
            {
                var copy = new double[NumberOfNodes];

                Array.Copy(m_Values [ i ],
                           0,
                           copy,
                           0,
                           NumberOfNodes);

                valuesList [ i ] = copy;
            }

            return valuesList;
        }

        public void UpdateForAnt(IAnt ant,
                                 int fromIndex,
                                 int toIndex)
        {
            double trimValue = CalculateNewTrimValue(ant,
                                                     fromIndex,
                                                     toIndex);

            m_Values [ toIndex ] [ fromIndex ] = trimValue;
            m_Values [ fromIndex ] [ toIndex ] = m_Values [ toIndex ] [ fromIndex ];
        }

        [NotNull]
        private static double[][] Create(int numberOfNodes,
                                         double initialValue)
        {
            var pheromones = new double[numberOfNodes][];

            for ( var i = 0 ; i < numberOfNodes ; ++i )
            {
                pheromones [ i ] = new double[numberOfNodes];
            }

            foreach ( double[] t in pheromones )
            {
                for ( var j = 0 ; j < t.Length ; ++j )
                {
                    t [ j ] = initialValue;
                }
            }

            return pheromones;
        }

        internal double CalculateNewTrimValue([NotNull] IAnt ant,
                                              int j,
                                              int i)
        {
            double newValue = CaclulateNewValue(ant,
                                                j,
                                                i);
            double trimValue = TrimPheromoneValue(newValue);

            return trimValue;
        }

        internal double CaclulateNewValue([NotNull] IAnt ant,
                                          int j,
                                          int i)
        {
            double decrease = ( 1.0 - m_Rho ) * m_Values [ i ] [ j ];
            var increase = 0.0;

            bool edgeInTrail = ant.TrailBuilder.EdgeInTrail(i,
                                                            j);

            if ( edgeInTrail )
            {
                increase = ( m_Q / ant.TrailBuilder.Length );
            }

            double newValue = decrease + increase;

            return newValue;
        }

        internal double TrimPheromoneValue(double value)
        {
            if ( value < MinimumValue )
            {
                return MinimumValue;
            }
            return value > MaximumValue
                       ? MaximumValue
                       : value;
        }
    }
}