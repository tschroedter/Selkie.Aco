using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Common.Interfaces
{
    public interface IAntSettings
    {
        [UsedImplicitly]
        bool IsFixedStartNode { get; }

        [UsedImplicitly]
        int FixedStartNode { get; }

        [UsedImplicitly]
        bool IsUnknown { get; }
    }
}