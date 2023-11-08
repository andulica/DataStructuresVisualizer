using DataStructuresVisualizer.DataStructures.HashMap;

namespace DataStructuresVisualizer.DataStructures.SinglyLinkedListFile
{
    public class EntrySinglyLinkedListNode
    {
        public Entry data { get; set; }
        public EntrySinglyLinkedListNode Next { get; set; }

        public EntrySinglyLinkedListNode(Entry data)
        {
            this.data = data;
            Next = null;
        }
    }
}
