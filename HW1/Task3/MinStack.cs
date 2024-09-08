namespace Task3
{
    internal class MinStack
    {
        private LinkedList<int> stack;
        private LinkedList<int> minStack;

        public MinStack()
        {
            stack = new LinkedList<int>();
            minStack = new LinkedList<int>();
        }

        public void Add(int value)
        {
            stack.AddLast(value);
            if (minStack.Last == null)
                minStack.AddLast(value);
            else
                minStack.AddLast(Math.Min(minStack.Last.Value, value));
        }

        public void Pop()
        {
            minStack.RemoveLast();
            stack.RemoveLast();
        }

        public int GetMinValue()
        {
            if (minStack.Last == null)
                throw new InvalidOperationException("Stack is empty.");
            return minStack.Last.Value;
        }

        public IEnumerable<int> GetContents()
        {
            return stack;
        }
    }
}
