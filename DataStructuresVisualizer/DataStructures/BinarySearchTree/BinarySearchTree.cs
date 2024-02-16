using System.Windows.Markup;

namespace DataStructuresVisualizer.DataStructures.BinarySearchTree
{
    public class BinarySearchTree<T>
    {
        // Root node of the binary search tree.
        public TreeNode<T> Root { get; private set; }
        private Random random = new Random();


        public BinarySearchTree(int numberOfNodes)
        {
            for (int i = 0; i < numberOfNodes; i++)
            {
                Insert((T)Convert.ChangeType(random.Next(1, 100), typeof(T)));
            }
        }
        /// <summary>
        /// Inserts a new value into the binary search tree.
        /// </summary>
        /// <param name="value">The value to be inserted.</param>
        public bool Insert(T value)
        {
            Console.WriteLine("Inserting: " + value);
            if (value is int intValue)
            {
                if (intValue <= 0 || intValue > 100)
                {
                    return false;
                }
            }
            Root = InsertRecursive(Root, value);
            return true;
        }

        /// <summary>
        /// Helper method for recursively inserting a new value into the tree.
        /// </summary>
        /// <param name="node">Current node in the recursion.</param>
        /// <param name="value">Value to insert.</param>
        /// <returns>The updated node after insertion.</returns>
        private TreeNode<T> InsertRecursive(TreeNode<T> node, T value)
        {
            // Comparer to handle generic types
            var comparer = Comparer<T>.Default;

            // Create new node if current node is null
            if (node == null)
            {
                node = new TreeNode<T>(value);
                return node;
            }

            // Recursive calls for left or right subtree based on comparison
            if (comparer.Compare(value, node.Data) < 0)
            {
                node.Left = InsertRecursive(node.Left, value);
                node.Left.IsVisited = true;

            }
            else if (comparer.Compare(value, node.Data) > 0)
            {
                node.Right = InsertRecursive(node.Right, value);
                node.Right.IsVisited = true;

            }

            return node;
        }

        /// <summary>
        /// Searches for a value in the binary search tree.
        /// </summary>
        /// <param name="value">Value to search for.</param>
        /// <returns>The node containing the value, if found; otherwise null.</returns>
        public TreeNode<T> Search(T value)
        {
            return SearchRecursive(Root, value);
        }

        /// <summary>
        /// Helper method for recursively searching a value in the tree.
        /// </summary>
        /// <param name="node">Current node in the recursion.</param>
        /// <param name="value">Value to search for.</param>
        /// <returns>The node containing the value, if found; otherwise null.</returns>
        private TreeNode<T> SearchRecursive(TreeNode<T> node, T value)
        {
            // Comparer to handle generic types
            var comparer = Comparer<T>.Default;

            // Return node if found or if reached end of branch
            if (node == null || EqualityComparer<T>.Default.Equals(node.Data, value))
            {
                return node;
            }

            // Recursive calls for left or right subtree based on comparison
            if (comparer.Compare(value, node.Data) < 0) 
            {
                node.IsVisited = true;
                return SearchRecursive(node.Left, value);
            }

            node.IsVisited = true;
            return SearchRecursive(node.Right, value);
        }

        /// <summary>
        /// Searches for the minimum value in the binary search tree.
        /// </summary>
        /// <returns>The node containing the minimum value.</returns>
        public TreeNode<T> SearchMin()
        {
            TreeNode<T> current = Root;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }

        /// <summary>
        /// Searches for the maximum value in the binary search tree.
        /// </summary>
        /// <returns>The node containing the maximum value.</returns>
        public TreeNode<T> SearchMax()
        {
            TreeNode<T> current = Root;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current;
        }

        /// <summary>
        /// Removes a value from the binary search tree.
        /// </summary>
        /// <param name="value">Value to be removed.</param>
        public void Remove(T value)
        {
            Root = RemoveRecursive(Root, value);
        }

        /// <summary>
        /// Helper method for recursively removing a value from the tree.
        /// </summary>
        /// <param name="node">Current node in the recursion.</param>
        /// <param name="value">Value to remove.</param>
        /// <returns>The updated node after removal.</returns>
        private TreeNode<T> RemoveRecursive(TreeNode<T> node, T value)
        {
            // Comparer to handle generic types
            var comparer = Comparer<T>.Default;

            if (node == null) return null;

            // Navigate to the node to be removed
            if (comparer.Compare(value, node.Data) < 0)
            {
                node.Left = RemoveRecursive(node.Left, value);
            }
            else if (comparer.Compare(value, node.Data) > 0)
            {
                node.Right = RemoveRecursive(node.Right, value);
            }
            else
            {
                // Handle nodes with only one child or no child
                if (node.Left == null)
                    return node.Right;
                if (node.Right == null)
                    return node.Left;

                // Node with two children: Get the inorder successor (smallest in the right subtree)
                TreeNode<T> temp = SearchMin(node.Right);
                node.Data = temp.Data; // Copy the inorder successor's data to this node
                node.Right = RemoveRecursive(node.Right, temp.Data); // Delete the inorder successor
            }

            return node;
        }

        /// <summary>
        /// Searches for the minimum value starting from a given node.
        /// </summary>
        /// <param name="node">The node from which to start the search.</param>
        /// <returns>The node containing the minimum value.</returns>
        private TreeNode<T> SearchMin(TreeNode<T> node)
        {
            TreeNode<T> current = node;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }

        /// <summary>
        /// Traverses the tree in in-order and performs an action on each node.
        /// </summary>
        /// <param name="node">The starting node for traversal.</param>
        public void Traverse(TreeNode<T> node)
        {
            if (node != null)
            {
                Traverse(node.Left);
                Console.WriteLine(node.Data);
                Traverse(node.Right);
            }
        }

        /// <summary>
        /// Traverses the tree in pre-order and performs an action on each node.
        /// </summary>
        /// <param name="node">The starting node for pre-order traversal.</param>
        public void PreOrder(TreeNode<T> node)
        {
            if (node != null)
            {
                Console.WriteLine(node.Data);
                PreOrder(node.Left);
                PreOrder(node.Right);
            }
        }

        /// <summary>
        /// Traverses the tree in post-order and performs an action on each node.
        /// </summary>
        /// <param name="node">The starting node for post-order traversal.</param>
        public void PostOrder(TreeNode<T> node)
        {
            if (node != null)
            {
                PostOrder(node.Left);
                PostOrder(node.Right);
                Console.WriteLine(node.Data);
            }
        }

        // Additional methods for testing and demonstration

        /// <summary>
        /// Prints the result of searching a specific value.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        public void PrintSearch(T value)
        {
            var node = Search(value);
            Console.WriteLine(node != null ? $"Node with value {value} found." : $"Node with value {value} not found.");
        }

        /// <summary>
        /// Prints the minimum value in the binary search tree.
        /// </summary>
        public void PrintSearchMin()
        {
            var node = SearchMin();
            Console.WriteLine(node != null ? $"Minimum value: {node.Data}" : "Tree is empty.");
        }

        /// <summary>
        /// Prints the maximum value in the binary search tree.
        /// </summary>
        public void PrintSearchMax()
        {
            var node = SearchMax();
            Console.WriteLine(node != null ? $"Maximum value: {node.Data}" : "Tree is empty.");
        }

        /// <summary>
        /// Prints all the nodes in the tree using in-order traversal.
        /// </summary>
        public void PrintTraverse()
        {
            Console.WriteLine("In-Order Traversal:");
            Traverse(Root, data => Console.WriteLine(data));
        }

        /// <summary>
        /// Prints all the nodes in the tree using pre-order traversal.
        /// </summary>
        public void PrintPreOrder()
        {
            Console.WriteLine("Pre-Order Traversal:");
            PreOrder(Root, data => Console.WriteLine(data));
        }

        /// <summary>
        /// Prints all the nodes in the tree using post-order traversal.
        /// </summary>
        public void PrintPostOrder()
        {
            Console.WriteLine("Post-Order Traversal:");
            PostOrder(Root, data => Console.WriteLine(data));
        }

        // Generic traversal methods with an action parameter

        /// <summary>
        /// Generic method for in-order traversal with a specified action.
        /// </summary>
        /// <param name="node">Starting node for traversal.</param>
        /// <param name="action">Action to perform on each node's data.</param>
        private void Traverse(TreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                Traverse(node.Left, action);
                action(node.Data);
                Traverse(node.Right, action);
            }
        }
        /// <summary>
        /// Generic method for pre-order traversal with a specified action.
        /// </summary>
        /// <param name="node">Starting node for traversal.</param>
        /// <param name="action">Action to perform on each node's data.</param>
        private void PreOrder(TreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                action(node.Data);
                PreOrder(node.Left, action);
                PreOrder(node.Right, action);
            }
        }

        /// <summary>
        /// Generic method for post-order traversal with a specified action.
        /// </summary>
        /// <param name="node">Starting node for traversal.</param>
        /// <param name="action">Action to perform on each node's data.</param>
        private void PostOrder(TreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                PostOrder(node.Left, action);
                PostOrder(node.Right, action);
                action(node.Data);
            }
        }
    }
}