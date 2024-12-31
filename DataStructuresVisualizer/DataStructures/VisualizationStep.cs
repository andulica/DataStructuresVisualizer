using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresVisualizer.DataStructures
{
    public class VisualizationStep
    {
        public int Index {  get; set; }
        public string Name { get; set; }
        public Func<Task> ForwardAction { get; set; }
        public Func<Task> BackwardAction { get; set; }
    }

}
