using DataStructuresVisualizer.DataStructures.Common.Utilities;

namespace DataStructuresVisualizer.DataStructures.BinarySearchTree
{
    public class BinarySearchTree<T>
    {
        public TreeNode<T> Root { get; private set; }

        public void Insert(T value)
        {
            Root = InsertRecursive(Root, value);
        }

        private TreeNode<T> InsertRecursive(TreeNode<T> node, T value)
        {

            var comparer = Comparer<T>.Default;

            if (node == null)
            {
                node = new TreeNode<T>(value);
                return node;
            }

            if (comparer.Compare(value, node.data) < 0)
            {
                node.Left = InsertRecursive(node.Left, value);
            }
            else if (comparer.Compare(value,node.data) > 0)
            { 
                node.Right = InsertRecursive(node.Right, value);
            }

            return node;
        }

        public TreeNode<T> Search(T value)
        {
            return SearchRecursive(Root, value);
        }

        private TreeNode<T> SearchRecursive(TreeNode<T> node, T value)
        {
            var comparer = Comparer<T>.Default;

            if (node == null || EqualityComparer<T>.Default.Equals(node.data, value))
            {
                return node;
            }

            if (comparer.Compare(value,node.data) < 0)
            {
                return SearchRecursive(node.Left, value);
            }

            return SearchRecursive(node.Right, value);
        }

        public TreeNode<T> SearchMin()
        {
            TreeNode<T> current = Root;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }

        public TreeNode<T> SearchMax()
        {
            TreeNode<T> current = Root;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current;
        }

        public void Remove(T value)
        {
            Root = RemoveRecursive(Root, value);
        }

        private TreeNode<T> RemoveRecursive(TreeNode<T> node, T value)
        {
            var comparer = Comparer<T>.Default;

            if (node == null) return null;

            if (comparer.Compare(value, node.data) < 0)
            {
                node.Left = RemoveRecursive(node.Left, value);
            }
            else if (comparer.Compare(value, node.data) > 0)
            {
                node.Right = RemoveRecursive(node.Right, value);
            }
            else
            {
                // node with only one child or no child
                if (node.Left == null)
                    return node.Right;
                if (node.Right == null)
                    return node.Left;

                // node with two children
                TreeNode <T> temp = SearchMin(node.Right);
                node.data = temp.data;
                node.Right = RemoveRecursive(node.Right, temp.data);
            }

            return node;
        }

        private TreeNode<T> SearchMin(TreeNode<T> node)
        {
            TreeNode<T> current = node;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }

        public void Traverse(TreeNode<T> node)
        {
            if (node != null)
            {
                Traverse(node.Left);
                Console.WriteLine(node.data);
                Traverse(node.Right);
            }
        }

        public void PreOrder(TreeNode<T> node)
        {
            if (node != null)
            {
                Console.WriteLine(node.data);
                PreOrder(node.Left);
                PreOrder(node.Right);
            }
        }

        public void PostOrder(TreeNode<T> node)
        {
            if (node != null)
            {
                PostOrder(node.Left);
                PostOrder(node.Right);
                Console.WriteLine(node.data);
            }
        }

        // code testing methods

        public void PrintSearch(T value)
        {
            var node = Search(value);
            Console.WriteLine(node != null ? $"Node with value {value} found." : $"Node with value {value} not found.");
        }

        public void PrintSearchMin()
        {
            var node = SearchMin();
            Console.WriteLine(node != null ? $"Minimum value: {node.data}" : "Tree is empty.");
        }

        public void PrintSearchMax()
        {
            var node = SearchMax();
            Console.WriteLine(node != null ? $"Maximum value: {node.data}" : "Tree is empty.");
        }

        public void PrintTraverse()
        {
            Console.WriteLine("In-Order Traversal:");
            Traverse(Root, data => Console.WriteLine(data));
        }

        public void PrintPreOrder()
        {
            Console.WriteLine("Pre-Order Traversal:");
            PreOrder(Root,data => Console.WriteLine(data));
        }


        public void PrintPostOrder()
        {
            Console.WriteLine("Post-Order Traversal:");
            PostOrder(Root,data => Console.WriteLine(data));
        }

        private void Traverse(TreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                Traverse(node.Left, action);
                action(node.data);
                Traverse(node.Right, action);
            }
        }

        private void PreOrder(TreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                action(node.data);
                PreOrder(node.Left, action);
                PreOrder(node.Right, action);
            }
        }

        private void PostOrder(TreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                PostOrder(node.Left, action);
                PostOrder(node.Right, action);
                action(node.data);
            }
        }
    }
}
