using DataStructuresVisualizer.DataStructures.SinglyLinkedListFile;
using DataStructuresVisualizer.DataStructures.Enums.SinglyLinkedList;
using DataStructuresVisualizer.DataStructures.Enums.Queue;
using System.Threading;

namespace DataStructuresVisualizer.DataStructures.Stack
{
    public class StackStructure <T> : SinglyLinkedList<T>/*,IEnumerable<T>*/
    {

        /// A delegate for highlighting steps during asynchronous operations.
        /// </summary>
        public Func<Enum, Task> HighlightRequested;

        /// <summary>
        /// Removes and returns the item at the top of the stack.
        /// </summary>
        /// <returns>The item at the top of the stack.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the stack is empty.</exception>
        public async Task PopAsync(SinglyLinkedListNode <T> nodeToPop, CancellationToken cancellationToken)
        {
            DeleteAtInstant(nodeToPop);

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
        /// Returns the item at the top of the stack without removing it.
        /// </summary>
        /// <returns>The item at the top of the stack.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the stack is empty.</exception>
        public async Task Peek(CancellationToken cancellationToken)
        {
            if (Count == 0)
            {
                await HighlightRequested.Invoke(PeekFrontSteps.CheckEmptyReturnNotFound);
                cancellationToken.ThrowIfCancellationRequested();
            }

            await HighlightRequested.Invoke(PeekFrontSteps.ReturnHeadItem);
            cancellationToken.ThrowIfCancellationRequested();
        }

        public async Task PushAsync(SinglyLinkedListNode<T> nodeToPush, CancellationToken cancellationToken)
        {
            await HighlightRequested.Invoke(PrependSteps.CreateVertex);
            cancellationToken.ThrowIfCancellationRequested();

            await HighlightRequested.Invoke(PrependSteps.SetNextPointer);
            cancellationToken.ThrowIfCancellationRequested();
            nodeToPush.Next = Head;

            await HighlightRequested.Invoke(PrependSteps.SetHead);
            cancellationToken.ThrowIfCancellationRequested();
            _head = nodeToPush;

            count++;
        }
    }
}