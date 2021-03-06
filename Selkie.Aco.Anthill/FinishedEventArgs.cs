using System;

// ReSharper disable NotAccessedField.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Selkie.Aco.Anthill
{
    public class FinishedEventArgs : EventArgs
    {
        public DateTime FinishTime { get; set; }
        public DateTime StartTime { get; set; }
        public int Times { get; set; }
    }
}