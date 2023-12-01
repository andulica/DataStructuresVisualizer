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
