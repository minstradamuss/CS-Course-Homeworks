namespace HT
{
    internal class HashTable
    {
        // просто захотелось с такой штукой попробовать
        private const double LoadFactor = 0.75;
        private int capacity;
        private int size;
        private object[] hashTable;
        private object tombstone = new object();

        public HashTable(int initialCapacity = 1000)
        {
            capacity = initialCapacity;
            size = 0;
            hashTable = new object[capacity];
        }

        public bool Add(object item)
        {
            if ((double)size / capacity >= LoadFactor)
            {
                Resize();
            }

            int attempt = 0;
            int hash = GetHash(item);

            while (attempt < capacity)
            {
                int currentIndex = (hash + attempt) % capacity;

                if (hashTable[currentIndex] == null || hashTable[currentIndex] == tombstone)
                {
                    hashTable[currentIndex] = item;
                    size++;
                    return true;
                }

                attempt++;
            }

            return false;
        }

        public bool Remove(object item)
        {
            int attempt = 0;
            int hash = GetHash(item);

            while (attempt < capacity)
            {
                int currentIndex = (hash + attempt) % capacity;

                if (hashTable[currentIndex] == null)
                {
                    return false;
                }
                else if (hashTable[currentIndex].Equals(item))
                {
                    hashTable[currentIndex] = tombstone;
                    size--;
                    return true;
                }

                attempt++;
            }

            return false;
        }

        public bool Contains(object item)
        {
            int attempt = 0;
            int hash = GetHash(item);

            while (attempt < capacity)
            {
                int currentIndex = (hash + attempt) % capacity;

                if (hashTable[currentIndex] == null)
                {
                    return false;
                }
                else if (hashTable[currentIndex].Equals(item))
                {
                    return true;
                }

                attempt++;
            }

            return false;
        }

        public int Size => size;
        public int Capacity => capacity;

        private void Resize()
        {
            int newCapacity = capacity * 2;
            object[] newTable = new object[newCapacity];

            for (int i = 0; i < capacity; i++)
            {
                if (hashTable[i] != null && hashTable[i] != tombstone)
                {
                    int hash = GetHash(hashTable[i], newCapacity);
                    int attempt = 0;

                    while (attempt < newCapacity)
                    {
                        int currentIndex = (hash + attempt) % newCapacity;

                        if (newTable[currentIndex] == null)
                        {
                            newTable[currentIndex] = hashTable[i];
                            break;
                        }

                        attempt++;
                    }
                }
            }

            capacity = newCapacity;
            hashTable = newTable;
        }

        private int GetHash(object item, int modCapacity = -1)
        {
            int hash = item.GetHashCode();
            hash = (hash == int.MinValue) ? 0 : Math.Abs(hash);
            return hash % (modCapacity > 0 ? modCapacity : capacity);
        }
    }
}
