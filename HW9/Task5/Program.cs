namespace Program
{
    public class CMyCountdownEvent : IDisposable
    {
        private int count;
        private readonly ManualResetEvent @event;

        public CMyCountdownEvent(int initialCount)
        {
            if (initialCount <= 0)
                throw new ArgumentException("initialCount must be > 0", nameof(initialCount));
            
            count = initialCount;
            @event = new ManualResetEvent(false);
        }

        public void Signal(int cnt = 1)
        {
            if (cnt <= 0)
                throw new ArgumentException("cnt must be > 0");

            else if (cnt > count)
                throw new ArgumentException("invalid signal cnt");

            count -= cnt;

            if (count == 0)
                @event.Set();
        }

        public bool Wait(TimeSpan timeout)
        {
            return @event.WaitOne(timeout);
        }

        public void Dispose()
        {
            @event.Dispose();
        }
    }

    class Program
    {
        private const int TestThreadNumber = 5;
        private static readonly CMyCountdownEvent myCountdownEvent = new CMyCountdownEvent(TestThreadNumber);

        static void Main(string[] args)
        {
            Thread[] threads = new Thread[TestThreadNumber];

            for (int i = 0; i < TestThreadNumber; i++)
            {
                threads[i] = new Thread(() =>
                {
                    Console.WriteLine("Start");
                    myCountdownEvent.Signal();
                    Console.WriteLine("Signal sent");
                });
                threads[i].Start();
            }

            Console.WriteLine("Waiting for all threads to complete...");
           
            if (myCountdownEvent.Wait(TimeSpan.FromMilliseconds(100)))
                Console.WriteLine("All threads have signaled.");
            else
                Console.WriteLine("smth went wrong");

            foreach (var thread in threads)
                thread.Join();
        }
    }
}
