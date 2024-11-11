using NUnit.Framework;
using System.Collections.Concurrent;
using System.Runtime.ExceptionServices;

class MergeSorter
{
    private readonly Random rng = new();

    private void SubSort(ref List<int> elementsToSort, int l, int r)
    {
        elementsToSort.Sort(l, r - l, Comparer<int>.Default);
        int delay = rng.Next(500, 1000);
        Thread.Sleep(delay);
    }

    private List<int> MergeTask(ref List<int> left, ref List<int> right)
    {
        var result = new List<int>();
        int leftIndex = 0, rightIndex = 0;

        while (leftIndex < left.Count && rightIndex < right.Count)
            result.Add(left[leftIndex] < right[rightIndex] ? left[leftIndex++] : right[rightIndex++]);

        while (leftIndex < left.Count) result.Add(left[leftIndex++]);
        while (rightIndex < right.Count) result.Add(right[rightIndex++]);

        return result;
    }

    public List<int> MergeSort(List<int> elementsToSort, int threads)
    {
        int part_array = (elementsToSort.Count + threads - 1) / threads;
        threads = (elementsToSort.Count + part_array - 1) / part_array;

        var result = new List<int>();
        var resultLock = new object();
        var finishedSubtasks = new ConcurrentQueue<int>();

        var subtasks = new List<Task>();
        for (int i = 0; i < threads; ++i)
        {
            int l = i * part_array;
            int r = Math.Min(elementsToSort.Count, l + part_array);
            var subTask = Task.Run(() => SubSort(ref elementsToSort, l, r));
            subTask.ContinueWith(_ =>
            {
                lock (resultLock)
                {
                    finishedSubtasks.Enqueue(l);
                    Monitor.Pulse(resultLock);
                }
            });
            subtasks.Add(subTask);
        }

        while (result.Count < elementsToSort.Count)
        {
            lock (resultLock)
            {
                while (finishedSubtasks.IsEmpty) Monitor.Wait(resultLock);

                if (finishedSubtasks.TryDequeue(out int l))
                {
                    int r = Math.Min(l + part_array, elementsToSort.Count);
                    var sortedBatch = elementsToSort.GetRange(l, r - l);
                    result = MergeTask(ref result, ref sortedBatch);
                }
            }
        }

        Task.WaitAll(subtasks.ToArray());
        return result;
    }
}

class MergeSortExample
{
    public void RunExample(int threads, int arraySize)
    {
        var random = new Random();
        var unsortedArray = Enumerable.Range(0, arraySize).Select(_ => random.Next(0, 100)).ToList();

        Console.WriteLine("Unsorted Array (random numbers): " + string.Join(" ", unsortedArray));

        var sorter = new MergeSorter();
        var sortedArray = sorter.MergeSort(unsortedArray, threads);

        Console.WriteLine("Sorted Array: " + string.Join(" ", sortedArray));
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter the number of threads (p): ");
        int threads = int.Parse(Console.ReadLine());

        Console.Write("Enter the size of the array (n): ");
        int arraySize = int.Parse(Console.ReadLine());

        var exampleRunner = new MergeSortExample();
        exampleRunner.RunExample(threads, arraySize);
    }
}
