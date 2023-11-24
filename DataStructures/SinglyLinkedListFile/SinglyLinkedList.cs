using DataStructuresVisualizer.DataStructures.HashMap;
using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class SinglyLinkedList<TKey, TValue> : IEnumerable<Entry<TKey, TValue>>
{
    public SinglyLinkedListNode<TKey, TValue> Head { get; private set; }

    public void Append(TKey key, TValue value)
    {
        Entry<TKey, TValue> newEntry = new Entry<TKey, TValue>(key, value);
        SinglyLinkedListNode<TKey, TValue> newNode = new SinglyLinkedListNode<TKey, TValue>(newEntry);

        if (Head == null)
        {
            Head = newNode;
            return;
        }

        SinglyLinkedListNode<TKey, TValue> current = Head;
        while (current.Next != null)
        {
            current = current.Next;
        }

        current.Next = newNode;
    }

    public void Prepend(TKey key, TValue value)
    {
        Entry<TKey, TValue> newEntry = new Entry<TKey, TValue>(key, value);
        SinglyLinkedListNode<TKey, TValue> newNode = new SinglyLinkedListNode<TKey, TValue>(newEntry)
        {
            Next = Head
        };
        Head = newNode;
    }

    public void InsertAt(int index, TKey key, TValue value)
    {
        Entry<TKey, TValue> newEntry = new Entry<TKey, TValue>(key, value);
        SinglyLinkedListNode<TKey, TValue> newNode = new SinglyLinkedListNode<TKey, TValue>(newEntry);

        if (index == 0)
        {
            newNode.Next = Head;
            Head = newNode;
            return;
        }

        SinglyLinkedListNode<TKey, TValue> current = Head;
        for (int i = 0; current != null && i < index - 1; i++)
        {
            current = current.Next;
        }

        if (current == null)
        {
            throw new IndexOutOfRangeException("Index out of range.");
        }

        newNode.Next = current.Next;
        current.Next = newNode;
    }

    public bool Remove(TKey key)
    {
        if (Head == null) return false;

        if (EqualityComparer<TKey>.Default.Equals(Head.Data.Key, key))
        {
            Head = Head.Next;
            return true;
        }

        SinglyLinkedListNode<TKey, TValue> current = Head;
        while (current.Next != null && !EqualityComparer<TKey>.Default.Equals(current.Next.Data.Key, key))
        {
            current = current.Next;
        }

        if (current.Next == null) return false;

        current.Next = current.Next.Next;
        return true;
    }

    public Entry<TKey, TValue> Search(TKey key)
    {
        SinglyLinkedListNode<TKey, TValue> current = Head;
        while (current != null)
        {
            if (EqualityComparer<TKey>.Default.Equals(current.Data.Key, key))
            {
                return current.Data;
            }

            current = current.Next;
        }

        return null;
    }

    public bool Contains(TKey key)
    {
        return Search(key) != null;
    }

    public IEnumerator<Entry<TKey, TValue>> GetEnumerator()
    {
        SinglyLinkedListNode<TKey, TValue> current = Head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        var result = new StringBuilder();
        SinglyLinkedListNode<TKey, TValue> current = Head;
        while (current != null)
        {
            result.Append($"[{current.Data.Key}, {current.Data.Value}] -> ");
            current = current.Next;
        }
        result.Append("null");
        return result.ToString();
    }
}

