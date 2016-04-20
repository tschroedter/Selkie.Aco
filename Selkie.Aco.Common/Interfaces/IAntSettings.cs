namespace Selkie.Aco.Common.Interfaces
{
    public interface IAntSettings
    {
        bool IsFixedStartNode { get; }
        int FixedStartNode { get; }
        bool IsUnknown { get; }
    }
}