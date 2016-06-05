using System;
using Selkie.Aco.Common.Interfaces;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Aco.Anthill
{
    [ProjectComponent(Lifestyle.Transient)]
    public class AntSettings : IAntSettings
    {
        private AntSettings()
        {
            IsUnknown = true;
        }

        public AntSettings(TrailStartNodeType trailStartNodeType,
                           int fixedStartNode)
        {
            switch ( trailStartNodeType )
            {
                case TrailStartNodeType.Random:
                    IsFixedStartNode = false;
                    break;

                case TrailStartNodeType.Fixed:
                    IsFixedStartNode = true;
                    break;

                default:
                    throw new ArgumentException("Unknown type '{0}'!".Inject(trailStartNodeType));
            }

            FixedStartNode = fixedStartNode;
        }

        public enum TrailStartNodeType
        {
            Random,
            Fixed
        }

        public static readonly IAntSettings Unknown = new AntSettings();

        public bool IsUnknown { get; private set; }
        public bool IsFixedStartNode { get; private set; }
        public int FixedStartNode { get; private set; }
    }
}