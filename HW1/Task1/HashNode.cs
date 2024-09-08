namespace HashNode
{
    public class HashNode<T>
    {
        public T Value { get; set; }
        public HashNode<T>? Next { get; set; }

        public HashNode(T value)
        {
            Value = value;
            Next = null;
        }
    }
}
