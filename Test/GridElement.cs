using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresVisualizer.Test
{
    public class GridElement
    {
        public NodeTest Node { get; set; }
        public string Color { get; set; }

        public GridElement(NodeTest node, string color)
        {
            Node = node;
            Color = color;
        }
    }

}
