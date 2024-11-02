namespace Program
{
    class Program
    {
        static void FunctionForA(string[] str)
        {
            var threads = new Thread[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                int n = i;
                threads[n] = new Thread(() =>
                {
                    Thread.Sleep(100 * str[n].Length);
                    Console.WriteLine(str[n]);
                });
                threads[n].Start();
            }

            foreach (var thread in threads)
                thread.Join();
        }

        static List<string> sorted = new List<string>();

        static void FunctionForB(string[] str)
        {
            var threads = new Thread[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                int n = i;
                threads[n] = new Thread(() =>
                {
                    Thread.Sleep(100 * str[n].Length);
                    lock (sorted)
                    {
                        sorted.Add(str[n]);
                    }
                });
                threads[n].Start();
            }

            foreach (var thread in threads)
                thread.Join();

            foreach (var words in sorted)
                Console.Write($"{words} ");
        }


        static void Main(string[] args)
        {
            FunctionForA(new string[] { "ab", "ababab", "fg", "asldkj!", "hj", "p" });
            FunctionForB(new string[] { "ab", "ababab", "fg", "asldkj!", "hj", "p" });
        }
    }
}