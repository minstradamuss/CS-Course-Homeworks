using LinkedList;

namespace HashTable
{
    public class HashTable<TKey, TValue>
    {
        private readonly int capacity;
        private readonly LinkedList<TKey, TValue>[] buckets;

        public HashTable(int capacity)
        {
            this.capacity = capacity;
            buckets = new LinkedList<TKey, TValue>[capacity];
            for (int i = 0; i < capacity; i++)
            {
                buckets[i] = new LinkedList<TKey, TValue>();
            }
        }

        private int GetBucketIndex(TKey key)
        {
            int hash = key.GetHashCode();
            int index = hash % capacity;
            return Math.Abs(index);
        }

        public void Add(TKey key, TValue value)
        {
            int index = GetBucketIndex(key);
            if (buckets[index].ContainsKey(key))
            {
                throw new ArgumentException("Key already exists.");
            }
            buckets[index].Add(key, value);
        }

        public bool Remove(TKey key)
        {
            int index = GetBucketIndex(key);
            return buckets[index].Remove(key);
        }

        public TValue Get(TKey key)
        {
            int index = GetBucketIndex(key);
            return buckets[index].Get(key);
        }

        public bool ContainsKey(TKey key)
        {
            int index = GetBucketIndex(key);
            return buckets[index].ContainsKey(key);
        }
    }
}
