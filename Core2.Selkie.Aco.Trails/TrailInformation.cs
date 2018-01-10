using System.Text;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Trails
{
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class TrailInformation : ITrailInformation
    {
        public TrailInformation([NotNull] ITrailBuilder trailBuilder,
                                [NotNull] ISettings settings,
                                int time)
        {
            TrailBuilder = trailBuilder;
            Settings = settings;
            Time = time;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"Time: {Time:D4} ");
            sb.Append($"Length: {(( int ) TrailBuilder.Length):D4} ");
            sb.Append($"Trail: {TrailBuilder} ");
            sb.Append($"Type: {Settings.AntType} ");
            sb.Append($"Alpha: {Settings.Alpha:F4} ");
            sb.Append($"Beta: {Settings.Beta:F4} ");
            sb.Append($"Gamma: {Settings.Gamma:F4} ");
            sb.Append($"Rho: {Settings.Rho:F4} ");
            sb.Append($"Q: {Settings.Q:F4}");

            return sb.ToString();
        }

        #region ITrailInformation Members

        public ITrailBuilder TrailBuilder { get; }

        public int Time { get; }

        public ISettings Settings { get; }

        #endregion
    }
}