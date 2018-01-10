using System;
using System.Collections.Generic;

// ReSharper disable NotAccessedField.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Core2.Selkie.Aco.Anthill
{
    public class BestTrailChangedEventArgs : EventArgs
    {
        public double Alpha { get; set; }
        public double Beta { get; set; }
        public double Gamma { get; set; }
        public int Iteration { get; set; }
        public double Length { get; set; }
        public IEnumerable <int> Trail { get; set; }
        public string AntType { get; set; }
    }
}