namespace DataStructuresVisualizer.DataStructures.Queue;

using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using System;
using System.Collections;
using System.Collections.Generic;
using DataStructuresVisualizer.DataStructures.Enums.Queue;
using System.Threading;
using System.Reflection;

public class QueueStructure<T> : SinglyLinkedList<T>, IEnumerable<T>
{
    public QueueStructure() : base() { }

    /// A delegate for highlighting steps during asynchronous operations.
    /// </summary>
    public Func<Enum, Task> HighlightRequested;

    /// <summary>
    /// Adds an item to the end of the queue.
    /// </summary>
    /// <param name="value">The item to add to the queue.</param>
    public async Task Enqueue(SinglyLinkedListNode<T> nodeToEnqueue)
    {
        AppendInstant(nodeToEnqueue);
        await HighlightRequested.Invoke(EnqueueSteps.CreateVertex);

        await HighlightRequested.Invoke(EnqueueSteps.UpdateTailNextPointer);

        await HighlightRequested.Invoke(EnqueueSteps.UpdateTail);
    }

    public async Task Dequeue(SinglyLinkedListNode<T> nodeToDequeue, CancellationToken cancellationToken)
    {
        DeleteAtInstant(nodeToDequeue);

        await HighlightRequested.Invoke(DequeueSteps.LoopKTimes); // "for (i = 0; i < K; ++i)"
        cancellationToken.ThrowIfCancellationRequested();

        await HighlightRequested.Invoke(DequeueSteps.SaveHeadInTmp); // "tmp = head"
        cancellationToken.ThrowIfCancellationRequested();

        await HighlightRequested.Invoke(DequeueSteps.MoveHeadToNext); // "head = head.next"
        cancellationToken.ThrowIfCancellationRequested();

        await HighlightRequested.Invoke(DequeueSteps.DeleteTmp); // "delete tmp"
        cancellationToken.ThrowIfCancellationRequested();
    }

    public void DequeueInstant(int numberOfNodesToDequeue)
    {
        for (int i = 0; i < numberOfNodesToDequeue; i++)
        {
            DeleteAtInstant(FindHead());
        }
    }

    /// <summary>
    /// Returns the item at the front of the queue without removing it.
    /// </summary>
    /// <returns>The item at the front of the queue.</returns>
    public async Task PeekFront()
    {
        if (Count == 0)
        {
            await HighlightRequested.Invoke(PeekFrontSteps.CheckEmptyReturnNotFound);
            return;
        }

        await HighlightRequested.Invoke(PeekFrontSteps.ReturnHeadItem);
    }

    /// <summary>
    /// Returns the item at the back of the queue without removing it.
    /// </summary>
    /// <returns>The item at the back of the queue.</returns>
    public async Task PeekBack(CancellationToken cancellationToken)
    {
        if (Count == 0)
        {
            await HighlightRequested.Invoke(PeekBackSteps.CheckEmptyReturnNotFound);
            cancellationToken.ThrowIfCancellationRequested();
        }

        await HighlightRequested.Invoke(PeekBackSteps.ReturnTailItem);
        cancellationToken.ThrowIfCancellationRequested();
    }

    /// <summary>
    /// Checks if the queue is empty.
    /// </summary>
    /// <returns>True if the queue is empty; otherwise, false.</returns>
    public bool IsEmpty()
    {
        return Count == 0;
    }

    /// <summary>
    /// Returns the number of items in the queue.
    /// </summary>
    /// <returns>The number of items in the queue.</returns>
    public int Size()
    {
        return Count;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the queue.
    /// </summary>
    /// <returns>An IEnumerator of type T.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        SinglyLinkedListNode<T> current = Head;
        while (current != null)
        {
            yield return current._data;
            current = current.Next;
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
