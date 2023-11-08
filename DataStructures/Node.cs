namespace DataStructuresVisualizer.DataStructures
{
    public class Node<T>
    {
        public T data { get; set; }
      //  public bool highLight { get; set; } 

        public Node (T data)
        {
            this.data = data;
        }
    }

    public class BaseView
    {

        public bool highlight;
        string fancydescripton;
    }

    public class NodeView : BaseView
    {
        
    }
}
