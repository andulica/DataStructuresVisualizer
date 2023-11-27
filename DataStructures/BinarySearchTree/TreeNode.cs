namespace DataStructuresVisualizer.DataStructures.BinarySearchTree
{
    /// <summary>
    /// Represents a single node in a binary tree.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the node.</typeparam>
    public class TreeNode<T>
    {
        /// <summary>
        /// Gets or sets the data stored in this node.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the left child of this node.
        /// </summary>
        public TreeNode<T> Left { get; set; }

        /// <summary>
        /// Gets or sets the right child of this node.
        /// </summary>
        public TreeNode<T> Right { get; set; }

        /// <summary>
        /// Constructor that initializes the TreeNode with the specified data.
        /// </summary>
        /// <param name="data">The data to store in the node.</param>
        public TreeNode(T data)
        {
            this.Data = data;
            // Initialize left and right children as null
            Left = null;
            Right = null;
        }
    }
}