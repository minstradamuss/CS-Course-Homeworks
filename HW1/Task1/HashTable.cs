using LinkedList;

namespace HashTable
{
    public class HashTable<T>
    {
        private readonly int capacity;
        private readonly LinkedList.LinkedList<T>[] buckets;

        public HashTable() : this(10)
        {
        }

        public HashTable(int capacity)
        {
            this.capacity = capacity;
            buckets = new LinkedList.LinkedList<T>[capacity];
            for (int i = 0; i < capacity; i++)
            {
                buckets[i] = new LinkedList.LinkedList<T>();
            }
        }

        private int GetBucketIndex(T key)
        {
            int hash = key.GetHashCode();
            int index = hash % capacity;
            return Math.Abs(index);
        }

        public void Add(T value)
        {
            int index = GetBucketIndex(value);
            if (buckets[index].Contains(value))
            {
                throw new ArgumentException("Value already exists.");
            }
            buckets[index].Add(value);
        }

        public bool Remove(T value)
        {
            int index = GetBucketIndex(value);
            return buckets[index].Remove(value);
        }

        public T Get(T value)
        {
            int index = GetBucketIndex(value);
            return buckets[index].Get(value);
        }

        public bool Contains(T value)
        {
            int index = GetBucketIndex(value);
            return buckets[index].Contains(value);
        }
    }
}
