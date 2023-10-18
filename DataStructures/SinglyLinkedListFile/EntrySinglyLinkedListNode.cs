using DataStructuresVisualizer.DataStructures.HashMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
