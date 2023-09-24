using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresVisualizer.DataStructures.BinarySearchTree
{
    public class BinarySearchTree
    {
        public Node Root { get; private set; }

        public BinarySearchTree()
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                Insert(random.Next(1, 100));
            }
        }

        public void Insert(int value)
        {
            Root = InsertRecursive(Root, value);
        }

        private Node InsertRecursive(Node node, int value)
        {
            if (node == null)
            {
                node = new Node(value);
                return node;
            }

            if (value < node.Value)
                node.Left = InsertRecursive(node.Left, value);
            else if (value > node.Value)
                node.Right = InsertRecursive(node.Right, value);

            return node;
        }

        public Node Search(int value)
        {
            return SearchRecursive(Root, value);
        }

        private Node SearchRecursive(Node node, int value)
        {
            if (node == null || node.Value == value)
                return node;

            if (value < node.Value)
                return SearchRecursive(node.Left, value);

            return SearchRecursive(node.Right, value);
        }

        public Node SearchMin()
        {
            Node current = Root;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }

        public Node SearchMax()
        {
            Node current = Root;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current;
        }

        public void Remove(int value)
        {
            Root = RemoveRecursive(Root, value);
        }

        private Node RemoveRecursive(Node node, int value)
        {
            if (node == null) return null;

            if (value < node.Value)
                node.Left = RemoveRecursive(node.Left, value);
            else if (value > node.Value)
                node.Right = RemoveRecursive(node.Right, value);
            else
            {
                // node with only one child or no child
                if (node.Left == null)
                    return node.Right;
                if (node.Right == null)
                    return node.Left;

                // node with two children
                Node temp = SearchMin(node.Right);
                node.Value = temp.Value;
                node.Right = RemoveRecursive(node.Right, temp.Value);
            }

            return node;
        }

        private Node SearchMin(Node node)
        {
            Node current = node;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }

        public void Traverse(Node node)
        {
            if (node != null)
            {
                Traverse(node.Left);
                Console.WriteLine(node.Value);
                Traverse(node.Right);
            }
        }

        public void PreOrder(Node node)
        {
            if (node != null)
            {
                Console.WriteLine(node.Value);
                PreOrder(node.Left);
                PreOrder(node.Right);
            }
        }

        public void PostOrder(Node node)
        {
            if (node != null)
            {
                PostOrder(node.Left);
                PostOrder(node.Right);
                Console.WriteLine(node.Value);
            }
        }

        // code testing methods

        public void PrintSearch(int value)
        {
            var node = Search(value);
            Console.WriteLine(node != null ? $"Node with value {value} found." : $"Node with value {value} not found.");
        }

        public void PrintSearchMin()
        {
            var node = SearchMin();
            Console.WriteLine(node != null ? $"Minimum value: {node.Value}" : "Tree is empty.");
        }

        public void PrintSearchMax()
        {
            var node = SearchMax();
            Console.WriteLine(node != null ? $"Maximum value: {node.Value}" : "Tree is empty.");
        }

        public void PrintTraverse()
        {
            Console.WriteLine("In-Order Traversal:");
            Traverse(Root, Console.WriteLine);
        }

        public void PrintPreOrder()
        {
            Console.WriteLine("Pre-Order Traversal:");
            PreOrder(Root, Console.WriteLine);
        }

        public void PrintPostOrder()
        {
            Console.WriteLine("Post-Order Traversal:");
            PostOrder(Root, Console.WriteLine);
        }

        private void Traverse(Node node, Action<int> action)
        {
            if (node != null)
            {
                Traverse(node.Left, action);
                action(node.Value);
                Traverse(node.Right, action);
            }
        }

        private void PreOrder(Node node, Action<int> action)
        {
            if (node != null)
            {
                action(node.Value);
                PreOrder(node.Left, action);
                PreOrder(node.Right, action);
            }
        }

        private void PostOrder(Node node, Action<int> action)
        {
            if (node != null)
            {
                PostOrder(node.Left, action);
                PostOrder(node.Right, action);
                action(node.Value);
            }
        }
    }
}
