using System;
using JetBrains.Annotations;

namespace Selkie.Aco.Anthill.Interfaces
{
    public interface IColonyLogger
    {
        void Error([NotNull] string message);
        void Info([NotNull] string message);
        void LogResult(TimeSpan runtimeSpan);
        void LogTrailBuilder([NotNull] LogTrailBuilderInformation information);
    }
}