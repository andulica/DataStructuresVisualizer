using DataStructuresVisualizer.DataStructures.BinarySearchTree;
using DataStructuresVisualizer.DataStructures.HashMap;
using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;

namespace DataStructuresVisualizer
{
    internal class Program
    {

        static void Main(string[] args)
        {

            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            tree.Insert(2);
            tree.Insert(5);
            tree.Insert(1);

            tree.PrintTraverse();
            tree.PrintSearchMax();


        //    // Initialize grid with nulls
        //    GridElement[,] grid = new GridElement[20, 20];
        //    for (int i = 0; i < 20; i++)
        //    {
        //        for (int j = 0; j < 20; j++)
        //        {
        //            grid[i, j] = null;
        //        }
        //    }

        //    // Initialize linked list
        //    NodeTest linkedList = new NodeTest(5);
        //    linkedList.Append(2).Append(1);

        //    // Insert linked list into grid
        //    InsertLinkedListIntoGrid(grid, linkedList, 5, 5);

        //    // Print grid (for visualization, GUI representation is required)
        //    PrintGrid(grid);

        //    AddNodeWithVisualRepresentation(grid, linkedList, 7);
        //    PrependNodeWithVisualRepresentation(grid, ref linkedList, 3);

        //    // Insert new node at specific index with visual representation
        //    InsertAtWithVisualRepresentation(grid, ref linkedList, 4, 4);

        //    // Deletion of an existing node at a specified index with a visual representation
        //    DeleteAtWithVisualRepresentation(grid, ref linkedList, 2);
        //    // Print grid after deletion
        //    PrintGrid(grid);

        //    Console.WriteLine("\nOperation Complete. Press Enter to exit.");
        //    Console.ReadLine();
        //}
        //static void InsertLinkedListIntoGrid(GridElement[,] grid, NodeTest linkedList, int startX, int startY)
        //{
        //    NodeTest currentNode = linkedList;
        //    int y = startY;

        //    while (currentNode != null && y < grid.GetLength(1))
        //    {
        //        grid[startX, y] = new GridElement(currentNode, "blue");
        //        currentNode = currentNode.Next;
        //        y++;
        //    }
        //}

        //static void PrintGrid(GridElement[,] grid)
        //{
        //    for (int i = 0; i < grid.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < grid.GetLength(1); j++)
        //        {
        //            if (grid[i, j]?.Node != null)
        //            {
        //                if (grid[i, j].Color == "red")
        //                {
        //                    Console.ForegroundColor = ConsoleColor.Red;
        //                }
        //                else if (grid[i, j].Color == "blue")
        //                {
        //                    Console.ForegroundColor = ConsoleColor.Blue;
        //                }

        //                Console.Write(grid[i, j].Node.Data.ToString("D"));
        //                Console.ResetColor();

        //                // Print an arrow if there's another node in the next column
        //                if (j + 1 < grid.GetLength(1) && grid[i, j + 1]?.Node != null)
        //                {
        //                    Console.Write("->");
        //                }
        //            }
        //            else
        //            {
        //                Console.Write("   ");  // Adjust space accordingly
        //            }
        //        }
        //        Console.WriteLine();
        //    }
        //}



        //static void AddNodeWithVisualRepresentation(GridElement[,] grid, NodeTest linkedList, int data)
        //{
        //    // Step 1: Display the new node at the bottom of the grid
        //    DisplayNewNodeAtBottom(grid, data);
        //    Console.WriteLine("\nPress Enter to integrate the node into the linked list...");
        //    Console.ReadLine();

        //    // Step 2: Add the node to the linked list
        //    linkedList.Append(data);

        //    // Clear the previous display of the new node at the bottom
        //    grid[9, 4] = null;

        //    // Step 3: Insert the updated list into the grid and print
        //    InsertLinkedListIntoGrid(grid, linkedList, 5, 5);
        //    PrintGrid(grid);
        //}



        //static void DisplayNewNodeAtBottom(GridElement[,] grid, int data)
        //{
        //    // Assuming the grid is 10x10
        //    // Place the new node at the bottom row, center column
        //    grid[9, 4] = new GridElement(new NodeTest(data), "green"); // Using green to indicate the new node

        //    // Print the grid showing the new node at the bottom
        //    PrintGrid(grid);
        //}

        //static void PrependNodeWithVisualRepresentation(GridElement[,] grid, ref NodeTest linkedList, int data)
        //{
        //    // Step 1: Display the new node at the bottom of the grid
        //    DisplayNewNodeAtBottom(grid, data);
        //    Console.WriteLine("\nPress Enter to integrate the node into the linked list...");
        //    Console.ReadLine();

        //    // Step 2: Prepend the node to the linked list
        //    linkedList = linkedList.Prepend(data);

        //    // Clear the previous display of the new node at the bottom
        //    grid[9, 4] = null;

        //    // Step 3: Insert the updated list into the grid and print
        //    // Assuming that your list can fit in the grid starting at 5,5
        //    InsertLinkedListIntoGrid(grid, linkedList, 5, 5);
        //    PrintGrid(grid);
        //}

        //static void VisuallyTraverse(GridElement[,] grid, NodeTest linkedList, int startX, int startY, int targetIndex)
        //{
        //    NodeTest current = linkedList;
        //    int y = startY;

        //    for (int i = 0; i <= targetIndex && current != null && y < grid.GetLength(1); i++)
        //    {
        //        // Update the color of the node in the grid to red
        //        if (grid[startX, y] != null)
        //        {
        //            grid[startX, y].Color = "red";
        //        }

        //        // Print the grid to show the node in red
        //        PrintGrid(grid);
        //        System.Threading.Thread.Sleep(500);  // Pausing for 0.5 seconds for visualization

        //        // Update the color of the node in the grid back to blue
        //        if (grid[startX, y] != null)
        //        {
        //            grid[startX, y].Color = "blue";
        //        }

        //        // Print the grid to show the node back in blue
        //        PrintGrid(grid);

        //        current = current.Next;
        //        y++;
        //    }
        //}


        //static void InsertAtWithVisualRepresentation(GridElement[,] grid, ref NodeTest linkedList, int data, int index)
        //{
        //    // Display the new node at the bottom of the grid
        //    DisplayNewNodeAtBottom(grid, data);
        //    Console.WriteLine($"\nPreparing to insert the node at index {index}. Press Enter to proceed...");
        //    Console.ReadLine();

        //    // Visually traverse up to the insertion point
        //    VisuallyTraverse(grid, linkedList, 5, 5, index - 1);

        //    // Insert the node at the specified index
        //    linkedList.InsertAt(index, data);

        //    // Clear the previous display of the new node at the bottom
        //    grid[9, 4] = null;

        //    // Display the updated list into the grid
        //    InsertLinkedListIntoGrid(grid, linkedList, 5, 5);
        //    PrintGrid(grid);
        //}

        //static void DeleteAtWithVisualRepresentation(GridElement[,] grid, ref NodeTest linkedList, int index)
        //{
        //    // Visually traverse up to the deletion point
        //    VisuallyTraverse(grid, linkedList, 5, 5, index);

        //    // Pause before deletion
        //    Console.WriteLine($"\nPreparing to delete the node at index {index}. Deleting... Press enter to continue.");
        //    Console.ReadLine();
        //    // Delete the node at the specified index
        //    linkedList.DeleteAt(index);

        //    // Clear the entire grid
        //    for (int i = 0; i < grid.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < grid.GetLength(1); j++)
        //        {
        //            grid[i, j] = null;
        //        }
        //    }

        //    // Redraw the linked list on the grid
        //    InsertLinkedListIntoGrid(grid, linkedList, 5, 5);
        }
    }
}