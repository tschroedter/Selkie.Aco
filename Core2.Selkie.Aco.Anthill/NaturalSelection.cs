using System;
using System.Collections.Generic;
using System.Linq;
using Core2.Selkie.Aco.Anthill.Interfaces;
using Core2.Selkie.Aco.Ants;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Common.Interfaces;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill
{
    [ProjectComponent(Lifestyle.Transient)]
    public class NaturalSelection : INaturalSelection
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        public NaturalSelection([NotNull] IRandom random,
                                [NotNull] ITrailHistory trailHistory,
                                [NotNull] IQueen queen)
        {
            m_Random = random;
            TrailHistory = trailHistory;
            Queen = queen;
        }

        public IQueen Queen { get; }

        private readonly IRandom m_Random;

        public ITrailHistory TrailHistory { get; }

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
        internal IChromosome SettingsToChromosome([NotNull] ISettings settings)
        {
            return new Chromosome(m_Random,
                                  settings.Alpha,
                                  settings.Beta,
                                  settings.Gamma);
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
    }
}