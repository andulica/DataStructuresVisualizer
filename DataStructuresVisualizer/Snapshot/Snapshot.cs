
namespace DataStructuresVisualizer.Snapshot
{
    public class Snapshot
    {
        public List<NodeState> Nodes { get; set; }
        public List<LinkState> Links { get; set; }
        public Guid? HighlightedNodeId { get; set; }
        public Guid? HighlightedLinkId { get; set; }

        public Snapshot(List<NodeState> nodes, List<LinkState> links, Guid? highlightedNodeId = null, Guid? highlightedLinkId = null)
        {
            // Deep copy the node and link states to avoid issues with references
            Nodes = nodes.Select(node => new NodeState(node)).ToList();
            Links = links.Select(link => new LinkState(link)).ToList();
            HighlightedNodeId = highlightedNodeId;
            HighlightedLinkId = highlightedLinkId;
        }
    }

    public class NodeState
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string FillColor { get; set; }

        public NodeState(Guid id, int value, double x, double y, string fillColor)
        {
            Id = id;
            Value = value;
            X = x;
            Y = y;
            FillColor = fillColor;
        }

        public NodeState(NodeState node)
        {
            Id = node.Id;
            Value = node.Value;
            X = node.X;
            Y = node.Y;
            FillColor = node.FillColor;
        }
    }

    public class LinkState
    {
        public string Id { get; set; }
        public double StartX { get; set; }
        public double StartY { get; set; }
        public double EndX { get; set; }
        public double EndY { get; set; }

        public LinkState(string id, double startX, double startY, double endX, double endY)
        {
            Id = id;
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
        }

        public LinkState(LinkState link)
        {
            Id = link.Id;
            StartX = link.StartX;
            StartY = link.StartY;
            EndX = link.EndX;
            EndY = link.EndY;
        }
    }

}
