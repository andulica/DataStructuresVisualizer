using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System.Collections;

public class SinglyLinkedList<T> : IEnumerable<SinglyLinkedListNode<T>>
{
    // The head node of the singly linked list.
    public SinglyLinkedListNode<T> head { get; set; }

    /// <summary>
    /// Appends a new node with the specified data to the end of the list.
    /// </summary>
    /// <param name="data">The data to append to the list.</param>
    public void Append(T data)
    {
        SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

        if (head == null)
        {
            head = newNode;
            return;
        }

        SinglyLinkedListNode<T> current = head;
        while (current.Next != null)
        {
            current = current.Next;
        }

        current.Next = newNode;
    }

    /// <summary>
    /// Prepends a new node with the specified data to the beginning of the list.
    /// </summary>
    /// <param name="data">The data to prepend to the list.</param>
    public void Prepend(T data)
    {
        SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            newNode.Next = head;
            head = newNode;
        }
    }

    /// <summary>
    /// Inserts a new node with the specified data at the specified index.
    /// </summary>
    /// <param name="index">The index at which to insert the node.</param>
    /// <param name="data">The data to insert into the list.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if the index is out of range.</exception>
    public void InsertAt(int index, T data)
    {
        SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);
        if (index == 0)
        {
            newNode.Next = head;
            head = newNode;
            return;
        }

        SinglyLinkedListNode<T> current = head;
        int currentIndex = 0;
        while (current != null && currentIndex < index - 1)
        {
            current = current.Next;
            currentIndex++;
        }

        if (current == null)
        {
            throw new IndexOutOfRangeException("Index out of range for the linked list.");
        }

        newNode.Next = current.Next;
        current.Next = newNode;
    }

    /// <summary>
    /// Deletes the first occurrence of a node with the specified data from the list.
    /// </summary>
    /// <param name="data">The data of the node to delete.</param>
    public void Delete(T data)
    {
        if (head == null) return; // Check if the list is empty

        // Check if the head contains the data to be deleted
        if (EqualityComparer<T>.Default.Equals(head._data, data))
        {
            head = head.Next; // Delete the head node
            return;
        }

        SinglyLinkedListNode<T> current = head;
        while (current.Next != null && !EqualityComparer<T>.Default.Equals(current.Next._data, data))
        {
            current = current.Next; // Traverse the list
        }

        // If the node after the current node needs to be deleted
        if (current.Next != null)
        {
            current.Next = current.Next.Next; // Delete the node
        }
    }


    /// <summary>
    /// Deletes the head node of the list and returns its data. Used for stack-like operations.
    /// </summary>
    /// <returns>The data of the deleted head node.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
    public T DeleteHeadForStack()
    {
        if (head == null)
        {
            throw new InvalidOperationException("The list is empty.");
        }

        T value = head._data;
        head = head.Next;
        return value;
    }

    /// <summary>
    /// Deletes the head node of the list.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
    public void DeleteHead()
    {
        if (head == null)
        {
            throw new InvalidOperationException("List is empty.");
        }
        head = head.Next;
    }

    /// <summary>
    /// Deletes the last node of the list.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
    public void DeleteTail()
    {
        if (head == null)
        {
            throw new InvalidOperationException("List is empty.");
        }

        if (head.Next == null)
        {
            head = null;
            return;
        }

        SinglyLinkedListNode<T> current = head;
        while (current.Next.Next != null)
        {
            current = current.Next;
        }
        current.Next = null;
    }

    /// <summary>
    /// Searches for a node with the specified data and returns it.
    /// </summary>
    /// <param name="data">The data to search for in the list.</param>
    /// <returns>The data if found in the list.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the value is not found in the list.</exception>
    public T Search(T data)
    {
        SinglyLinkedListNode<T> current = head;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current._data, data))
            {
                return data;
            }
            current = current.Next;
        }
        throw new KeyNotFoundException("Value not found in the list.");
    }

    /// <summary>
    /// Searches for the first occurrence of the specified value within the linked list and returns its index.
    /// </summary>
    /// <param name="data">The value to search for in the list.</param>
    /// <returns>The zero-based index of the first occurrence of the value in the list, if found.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the value is not present in the list.</exception>
    public int FindIndexOf(T data)
    {
        SinglyLinkedListNode<T> current = head;
        int index = 0;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current._data, data))
            {
                return index += 1;
            }
            current = current.Next;
            index++;
        }
        return -1;
    }


    /// <summary>
    /// Returns a string representation of the linked list.
    /// </summary>
    /// <returns>A string representing the linked list.</returns>
    public override string ToString()
    {
        string result = "";
        SinglyLinkedListNode<T> current = head;
        while (current != null)
        {
            result += current._data + " -> ";
            current = current.Next;
        }
        return result + "null";
    }

    /// <summary>
    /// Determines whether the list contains a specific value.
    /// </summary>
    /// <param name="value">The value to locate in the list.</param>
    /// <returns>True if the value is found; otherwise, false.</returns>
    public bool Contains(T value)
    {
        SinglyLinkedListNode<T> current = head;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current._data, value))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    /// <summary>
    /// Adds a new node with the specified value to the end of the list.
    /// </summary>
    /// <param name="value">The value to add to the list.</param>
    public void AddLast(T value)
    {
        if (head == null)
        {
            head = new SinglyLinkedListNode<T>(value);
            return;
        }

        SinglyLinkedListNode<T> current = head;
        while (current.Next != null)
        {
            current = current.Next;
        }
        current.Next = new SinglyLinkedListNode<T>(value);
    }

    /// <summary>
    /// Removes the first occurrence of a node with the specified value from the list.
    /// </summary>
    /// <param name="value">The value of the node to remove.</param>
    public void Remove(T value)
    {
        if (head == null) return;

        if (EqualityComparer<T>.Default.Equals(head._data, value))
        {
            head = head.Next;
            return;
        }

        SinglyLinkedListNode<T> current = head;
        SinglyLinkedListNode<T> prev = null;
        while (current != null && !EqualityComparer<T>.Default.Equals(head._data, value))
        {
            prev = current;
            current = current.Next;
        }

        if (current != null)
        {
            prev.Next = current.Next;
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the linked list.
    /// </summary>
    /// <returns>An IEnumerator for the linked list.</returns>
    public IEnumerator<SinglyLinkedListNode<T>> GetEnumerator()
    {
        SinglyLinkedListNode<T> current = head;
        while (current != null)
        {
            yield return current;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}