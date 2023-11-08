namespace DataStructuresVisualizer.DataStructures.BinarySearchTree
{
    public class TreeNode<T> : Node<T>
    {
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }
        public TreeNode<T> Root { get; set; }

        public TreeNode(T data) : base(data)
        {
            Right = null;
            Left = null;
        }
    }
}
