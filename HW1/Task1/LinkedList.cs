using HashNode;

namespace LinkedList
{
    public class LinkedList<T>
    {
        private HashNode<T>? head;

        public void Add(T value)
        {
            HashNode<T> newNode = new HashNode<T>(value);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                HashNode<T> current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        public bool Remove(T value)
        {
            HashNode<T>? current = head;
            HashNode<T>? previous = null;

            while (current != null)
            {
                if (current.Value.Equals(value))
                {
                    if (previous == null)
                    {
                        head = current.Next;
                    }
                    else
                    {
                        previous.Next = current.Next;
                    }
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }

        public T Get(T value)
        {
            HashNode<T>? current = head;
            while (current != null)
            {
                if (current.Value.Equals(value))
                {
                    return current.Value;
                }
                current = current.Next;
            }
            throw new KeyNotFoundException($"Value '{value}' not found.");
        }

        public bool Contains(T value)
        {
            HashNode<T>? current = head;
            while (current != null)
            {
                if (current.Value.Equals(value))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
    }
}
