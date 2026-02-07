// make the node class
public class Node<T>
{
    public T Data;
    public Node<T> Next;
    public Node(T data)
    {
        Data = data;
        Next = null;
    }
}

//single linked list class
public class SingleLinkedList<T>
{
    private Node<T> head;
    private Node<T> tail;

    //make constructor
    public SingleLinkedList()
    {
        head = null;
        tail = null;
    }

    public void AddNode(T data)
    {
        Node<T> newNode = new Node<T>(data);

        newNode.Next = head;
        head = newNode;
        if (tail == null)
        {
            tail = newNode;
        }
    }


    public void PintList()
    {
        Node<T> current = head;
        while (current != null)
        {
            Console.Write(current.Data + " -> ");
            current = current.Next;
        }
        Console.WriteLine("null");
    }
}



class Program
{
    static void Main(string[] args)
    {
        SingleLinkedList<int> list = new SingleLinkedList<int>();
        list.AddNode(10);
        list.AddNode(20);
        // Add nodes and test the list here
        list.PintList();
    }
}