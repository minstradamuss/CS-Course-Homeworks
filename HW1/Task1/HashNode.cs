namespace HashNode
{
    public class HashNode<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public HashNode<TKey, TValue> Next { get; set; }

        public HashNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Next = null;
        }
    }
}
