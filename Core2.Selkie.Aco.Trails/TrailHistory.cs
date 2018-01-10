using System.Collections.Generic;
using System.Text;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Trails
{
    [ProjectComponent(Lifestyle.Transient)]
    public class TrailHistory : ITrailHistory
    {
        public TrailHistory()
        {
            m_Dictonary = new Dictionary <int, List <ITrailInformation>>();
            Information = new List <ITrailInformation>();
        }

        private Dictionary <int, List <ITrailInformation>> m_Dictonary;

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach ( int length in m_Dictonary.Keys )
            {
                var sbForKey = new StringBuilder();

                sbForKey.Append("Length " + length + ": ");

                foreach ( ITrailInformation information in m_Dictonary [ length ] )
                {
                    sbForKey.Append("\r\n" + information.TrailBuilder.Trail);
                }

                sb.Append(sbForKey + "\r\n");
            }

            return sb.ToString();
        }

        [NotNull]
        internal static IEnumerable <ITrailInformation> ConvertValuesToList(
            [NotNull] Dictionary <int, List <ITrailInformation>>.ValueCollection values)
        {
            var list = new List <ITrailInformation>();

            foreach ( List <ITrailInformation> trailInformations in values )
            {
                list.AddRange(trailInformations);
            }

            return list;
        }

        #region IColonyBestTrails Members

        public IEnumerable <ITrailInformation> this[int index]
        {
            get
            {
                return m_Dictonary [ index ];
            }
        }

        public IEnumerable <int> Lengths
        {
            get
            {
                return m_Dictonary.Keys;
            }
        }

        public int Count
        {
            get
            {
                return m_Dictonary.Count;
            }
        }

        public IEnumerable <ITrailInformation> Information { get; private set; }

        public void Clear()
        {
            m_Dictonary = new Dictionary <int, List <ITrailInformation>>();
            Information = new List <ITrailInformation>();
        }

        public void AddTrailInformation(ITrailBuilder trailBuilder,
                                        ISettings settings,
                                        int time)
        {
            ITrailInformation information = new TrailInformation(trailBuilder,
                                                                 settings,
                                                                 time);

            AddTrailInformation(information);
        }

        public void AddTrailInformation(ITrailInformation information)
        {
            ITrailBuilder trailBuilder = information.TrailBuilder;

            if ( trailBuilder.IsUnknown )
            {
                return;
            }

            var length = ( int ) trailBuilder.Length;

            if ( IsKnownLength(length) )
            {
                AddUnknownLength(information,
                                 length);
            }
            else
            {
                AddKnownLength(information,
                               length);
            }
        }

        private void AddKnownLength([NotNull] ITrailInformation information,
                                    int length)
        {
            var list = new List <ITrailInformation>
                       {
                           information
                       };

            m_Dictonary.Add(length,
                            list);

            Information = ConvertValuesToList(m_Dictonary.Values);
        }

        private void AddUnknownLength([NotNull] ITrailInformation information,
                                      int length)
        {
            List <ITrailInformation> list = m_Dictonary [ length ];

            if ( !list.Contains(information) )
            {
                list.Add(information);
            }
        }

        internal bool IsKnownLength(int length)
        {
            if ( length <= 0 )
            {
                return true;
            }

            bool isKnownLength = m_Dictonary.ContainsKey(length);

            return isKnownLength;
        }

        #endregion
    }
}