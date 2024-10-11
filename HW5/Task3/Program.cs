using System.Collections;

public class LinkedList<T> : IEnumerable<T>
{
    private class Node
    {
        public T Value;
        public Node Next;

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }

    private Node head;
    private Node tail;
    public int Count { get; private set; }

    public LinkedList()
    {
        head = null;
        tail = null;
        Count = 0;
    }

    public void Add(T value)
    {
        Node newNode = new Node(value);
        if (tail == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            tail = newNode;
        }
        Count++;
    }

    public bool Remove(T value)
    {
        Node current = head;
        Node previous = null;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, value))
            {
                if (previous == null)
                    head = current.Next;
                else
                    previous.Next = current.Next;

                if (current.Next == null)
                    tail = previous;

                Count--;
                return true;
            }

            previous = current;
            current = current.Next;
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node current = head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main()
    {
        LinkedList<int> list = new LinkedList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        Console.WriteLine("Элементы списка после добавления:");
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("Количество элементов: " + list.Count);

        bool removed = list.Remove(2);
        Console.WriteLine("\nПопытка удалить элемент 2: " + (removed ? "успешно" : "этого элемента нет"));

        Console.WriteLine("Элементы списка после удаления:");
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("Количество элементов: " + list.Count);

        removed = list.Remove(5);
        Console.WriteLine("\nПопытка удалить элемент 5: " + (removed ? "успешно" : "этого элемента нет"));
    }
}
