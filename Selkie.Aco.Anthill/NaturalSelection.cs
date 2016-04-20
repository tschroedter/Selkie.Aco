using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Aco.Anthill.Interfaces;
using Selkie.Aco.Ants;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aco.Trails.Interfaces;
using Selkie.Common;
using Selkie.Windsor;

namespace Selkie.Aco.Anthill
{
    [ProjectComponent(Lifestyle.Transient)]
    public class NaturalSelection : INaturalSelection
    {
        private readonly IQueen m_Queen;
        private readonly IRandom m_Random;
        private readonly ITrailHistory m_TrailHistory;
        // ReSharper disable once NotNullMemberIsNotInitialized
        public NaturalSelection([NotNull] IRandom random,
                                [NotNull] ITrailHistory trailHistory,
                                [NotNull] IQueen queen)
        {
            m_Random = random;
            m_TrailHistory = trailHistory;
            m_Queen = queen;
        }

        public IQueen Queen
        {
            get
            {
                return m_Queen;
            }
        }

        public ITrailHistory TrailHistory
        {
            get
            {
                return m_TrailHistory;
            }
        }

        public void DoSelection()
        {
            if ( TrailHistory.Information.Count() < 2 )
            {
                Queen.RandomSelection();
            }
            else
            {
                Tuple <IChromosome, IChromosome> pair = FindBestChromosomePair();
                IChromosome male = pair.Item1;
                IChromosome female = pair.Item2;

                if ( male.Equals(female) )
                {
                    Queen.RandomSelection();
                }
                else
                {
                    Queen.NaturalSelection(male,
                                           female);
                }
            }
        }

        [NotNull]
        internal Tuple <IChromosome, IChromosome> FindBestChromosomePair()
        {
            List <int> sortedKeys = TrailHistory.Lengths.ToList();
            sortedKeys.Sort();

            IEnumerable <ITrailInformation> maleBestTrailInformations = TrailHistory [ sortedKeys.First() ];
            ISettings maleSettings = maleBestTrailInformations.First().Settings;
            IChromosome male = SettingsToChromosome(maleSettings);
            IChromosome female = male;

            female = FindBestChromosomePairInSortedKeys(sortedKeys,
                                                        male,
                                                        female);

            return new Tuple <IChromosome, IChromosome>(male,
                                                        female);
        }

        [NotNull]
        private IChromosome FindBestChromosomePairInSortedKeys([NotNull] IEnumerable <int> sortedKeys,
                                                               [NotNull] IChromosome male,
                                                               [NotNull] IChromosome female)
        {
            foreach ( int sortedKey in sortedKeys )
            {
                IEnumerable <ITrailInformation> femaleBestTrailInformations = TrailHistory [ sortedKey ];
                ISettings femaleSettings = femaleBestTrailInformations.First().Settings;
                IChromosome current = SettingsToChromosome(femaleSettings);

                if ( male.Equals(current) )
                {
                    continue;
                }

                female = current;
                break;
            }
            return female;
        }

        [NotNull]
        internal IChromosome SettingsToChromosome([NotNull] ISettings settings)
        {
            return new Chromosome(m_Random,
                                  settings.Alpha,
                                  settings.Beta,
                                  settings.Gamma);
        }
    }
}