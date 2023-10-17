namespace DataStructuresVisualizer.DataStructures.BinarySearchTree
{
    public class TreeNode : Node
    {
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        public TreeNode Root { get; set; }

        public TreeNode(int data) : base(data)
        {
            Right = null;
            Left = null;
        }
    }
}
