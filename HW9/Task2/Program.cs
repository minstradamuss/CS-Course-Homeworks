namespace Program
{
    public class Foo
    {
        private readonly AutoResetEvent event1 = new AutoResetEvent(false);
        private readonly AutoResetEvent event2 = new AutoResetEvent(false);

        public void first()
        {
            Console.Write("first");
            event1.Set();
        }

        public void second()
        {
            event1.WaitOne();
            Console.Write("second");
            event2.Set();
        }

        public void third()
        {
            event2.WaitOne();
            Console.Write("third");
        }
    }

    class Program
    {
        static void CreateTreads(int[] ord)
        {
            var foo = new Foo();
            var threads = new Thread[3];

            threads[0] = new Thread(foo.first);
            threads[1] = new Thread(foo.second);
            threads[2] = new Thread(foo.third);

            foreach (var i in ord)
                threads[i - 1].Start();

            foreach (var thread in threads)
                thread.Join();
        }

        static void Main(string[] args)
        {
            CreateTreads(new int[] { 1, 3, 2 });
            Console.WriteLine('\n');
            CreateTreads(new int[] { 1, 2, 3 });
            Console.WriteLine('\n');
            CreateTreads(new int[] { 3, 2, 1 });
        }
    }
}
