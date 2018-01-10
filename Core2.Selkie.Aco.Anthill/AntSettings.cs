using System;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill
{
    [ProjectComponent(Lifestyle.Transient)]
    public class AntSettings : IAntSettings
    {
        private AntSettings()
        {
            IsUnknown = true;
        }

        [UsedImplicitly]
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
                    throw new ArgumentException($"Unknown type '{trailStartNodeType}'!");
            }

            FixedStartNode = fixedStartNode;
        }

        public enum TrailStartNodeType
        {
            Random,
            Fixed
        }

        public static readonly IAntSettings Unknown = new AntSettings();

        public bool IsUnknown { get; }
        public bool IsFixedStartNode { get; }
        public int FixedStartNode { get; }
    }
}