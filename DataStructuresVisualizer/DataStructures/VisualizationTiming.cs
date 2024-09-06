namespace DataStructuresVisualizer.DataStructures
{
    public struct VisualizationTiming
    {
        public int HighlightDelay { get; set; }
        public int NodeMovementDelay { get; set; }
        public int JavaScriptDelay { get; set; }

        public VisualizationTiming(int highlightDelay, int nodeMovementDelay, int javaScriptDelay)
        {
            HighlightDelay = highlightDelay;
            NodeMovementDelay = nodeMovementDelay;
            JavaScriptDelay = javaScriptDelay;
        }

        public static readonly VisualizationTiming Default = new VisualizationTiming
        {
            HighlightDelay = 1000,
            NodeMovementDelay = 1000,
            JavaScriptDelay = 1000
        };
    }
}
