using HashNode;

namespace LinkedList
{
    public class LinkedList<TKey, TValue>
    {
        private HashNode<TKey, TValue> head;

        public void Add(TKey key, TValue value)
        {
            HashNode<TKey, TValue> newNode = new HashNode<TKey, TValue>(key, value);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                HashNode<TKey, TValue> current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        public bool Remove(TKey key)
        {
            HashNode<TKey, TValue> current = head;
            HashNode<TKey, TValue> previous = null;

            while (current != null)
            {
                if (current.Key.Equals(key))
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

        public TValue Get(TKey key)
        {
            HashNode<TKey, TValue> current = head;
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return current.Value;
                }
                current = current.Next;
            }
            throw new KeyNotFoundException($"Key '{key}' not found.");
        }

        public bool ContainsKey(TKey key)
        {
            HashNode<TKey, TValue> current = head;
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
    }
}
