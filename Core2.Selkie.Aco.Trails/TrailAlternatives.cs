using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Common.TypedFactories;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Trails
{
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class TrailAlternatives : ITrailAlternatives
    {
        public TrailAlternatives([NotNull] ITrailBuilderFactory trailBuilderFactory,
                                 [NotNull] IChromosomeFactory chromosomeFactory)
        {
            m_TrailBuilderFactory = trailBuilderFactory;
            m_ChromosomeFactory = chromosomeFactory;
            m_Alternatives = new Dictionary <int, List <ITrailBuilder>>();
            m_Trails = new List <ITrailBuilder>();
        }

        public int Count
        {
            get
            {
                return m_Alternatives.Count;
            }
        }

        private readonly Dictionary <int, List <ITrailBuilder>> m_Alternatives;
        private readonly IChromosomeFactory m_ChromosomeFactory;
        private readonly ITrailBuilderFactory m_TrailBuilderFactory;
        private readonly List <ITrailBuilder> m_Trails;

        public IEnumerable <ITrailBuilder> Trails
        {
            get
            {
                return m_Trails;
            }
        }

        public void Clear()
        {
            ReleaseAlternatives();
            ReleaseTrails();
            EmptyInternalList();
        }

        public void AddAlternative(int id,
                                   ITrailBuilder trailBuilder)
        {
            if ( IsKnownAlternative(trailBuilder) )
            {
                return;
            }

            ITrailBuilder clone = trailBuilder.Clone(m_TrailBuilderFactory,
                                                     m_ChromosomeFactory);

            if ( !m_Alternatives.TryGetValue(id,
                                             out List <ITrailBuilder> list) )
            {
                list = new List <ITrailBuilder>
                       {
                           clone
                       };

                m_Alternatives.Add(id,
                                   list);

                m_Trails.Clear();
                m_Trails.AddRange(ConvertValuesToList());
            }
            else
            {
                list.Add(clone);
                m_Trails.Clear();
                m_Trails.AddRange(ConvertValuesToList());
            }
        }

        public bool IsKnownAlternative(ITrailBuilder trailBuilder)
        {
            if ( !Trails.Any() )
            {
                return false;
            }

            List <int> newTrail = trailBuilder.Trail.ToList();

            return Trails.Any(alternative => newTrail.SequenceEqual(alternative.Trail));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach ( int key in m_Alternatives.Keys )
            {
                var sbForKey = new StringBuilder();

                sbForKey.Append("Ant " + key + ": ");

                foreach ( ITrailBuilder trail in m_Alternatives [ key ] )
                {
                    sbForKey.Append("\r\n" + trail);
                }

                sb.Append(sbForKey + "\r\n");
            }

            return sb.ToString();
        }

        [NotNull]
        internal IEnumerable <ITrailBuilder> ConvertValuesToList()
        {
            var list = new List <ITrailBuilder>();

            foreach ( List <ITrailBuilder> trails in m_Alternatives.Values )
            {
                list.AddRange(trails);
            }

            return list;
        }

        private void EmptyInternalList()
        {
            m_Alternatives.Clear();
            m_Trails.Clear();
        }

        private void ReleaseAlternatives()
        {
            foreach ( List <ITrailBuilder> builders in m_Alternatives.Values )
            {
                foreach ( ITrailBuilder builder in builders )
                {
                    m_TrailBuilderFactory.Release(builder);
                }
            }
        }

        private void ReleaseTrails()
        {
            foreach ( ITrailBuilder builder in Trails )
            {
                m_TrailBuilderFactory.Release(builder);
            }
        }
    }
}