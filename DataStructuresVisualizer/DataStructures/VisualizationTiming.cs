using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresVisualizer.DataStructures
{
    public class VisualizationTiming
    {
        public int HighlightDelay { get; set; } = 0; // Delay for code highlighting
        public int NodeMovementDelay { get; set; } = 0; // Delay for node movement
        public int JavaScriptDelay { get; set; } = 0; // Delay for JavaScript visualization
    }
}
