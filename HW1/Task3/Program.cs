namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            MinStackExample.RunExample();
        }
    }

    public static class MinStackExample
    {
        public static void RunExample()
        {
            MinStack stack = new MinStack();
            stack.Add(3);
            PrintStack(stack);

            stack.Add(2);
            PrintStack(stack);

            stack.Add(1);
            PrintStack(stack);

            stack.Pop();
            PrintStack(stack);
        }

        private static void PrintStack(MinStack stack)
        {
            var stackContents = stack.GetContents();

            string stackString = "[ " + string.Join(", ", stackContents) + " ]";
            
            int minValue = stack.GetMinValue();
            Console.WriteLine($"{stackString} : Min = {minValue}");
        }
    }
}
