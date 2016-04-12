namespace Selkie.Aco.Common
{
    public interface IAntSettings
    {
        bool IsFixedStartNode { get; }
        int FixedStartNode { get; }
        bool IsUnknown { get; }
    }
}