namespace ConcurrentArray
{
    public class ConcurrentArray
    {
        private readonly List<int> array;
        private readonly ReaderWriterLockSlim lockk;

        public ConcurrentArray(List<int> array)
        {
            this.array = array ?? throw new ArgumentNullException(nameof(array));
            lockk = new ReaderWriterLockSlim();
        }

        public void Avg()
        {
            ExecuteReadAction(() =>
            {
                var result = array.Average();
                Console.WriteLine($"\nAverage: {result}");
            });
        }

        public void Min()
        {
            ExecuteReadAction(() =>
            {
                var result = array.Min();
                Console.WriteLine($"\nMin: {result}");
            });          
        }

        public void Swap()
        {
            ExecuteWriteAction(() =>
            {
                Console.WriteLine($"\nArray without swap: [" + string.Join(", ", array) + "]");
                var i = Random.Shared.Next(array.Count);
                var j = Random.Shared.Next(array.Count);
                (array[i], array[j]) = (array[j], array[i]);

                Console.WriteLine($"Swap: array[{i}] = {array[i]} <=> array[{j}] = {array[j]}");
                Console.WriteLine($"Array with swap: [" + string.Join(", ", array) + "]");
            });
        }

        public void Sort()
        {
            ExecuteWriteAction(() =>
            {
                array.Sort();
                Console.WriteLine("\nSort: array = [" + string.Join(", ", array) + "]");
            });
        }

        private void ExecuteReadAction(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            lockk.EnterReadLock();
            try
            {
                action();
            }
            finally
            {
                lockk.ExitReadLock();
            }
        }

        private void ExecuteWriteAction(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            lockk.EnterWriteLock();
            try
            {
                action();
            }
            finally
            {
                lockk.ExitWriteLock();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var array = new List<int> { 7, 20, 5, 10, 12 };
            var concurrentArray = new ConcurrentArray(array);

            var tasks = new List<Task>
            {
                Task.Run(() => concurrentArray.Min()),
                Task.Run(() => concurrentArray.Avg()),
                Task.Run(() => concurrentArray.Sort()),
                Task.Run(() => concurrentArray.Swap())
            };

            Task.WaitAll(tasks.ToArray());
        }
    }
}
