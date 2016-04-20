using System;
using JetBrains.Annotations;

namespace Selkie.Aco.Anthill.Interfaces
{
    public interface IColonyLogger
    {
        void LogResult(TimeSpan runtimeSpan);
        void Info([NotNull] string message);
        void Error([NotNull] string message);
        void LogTrailBuilder([NotNull] LogTrailBuilderInformation information);
    }
}