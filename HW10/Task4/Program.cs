using NUnit.Common;
using NUnit.Framework;
using NUnitLite;
using System.Reflection;

namespace MergeSort
{
    public class MergeSort
    {
        private readonly int threads;
        private readonly int delay;

        public MergeSort(int _threads, int _delay = 500)
        {
            threads = _threads;
            delay = _delay;
        }

        private void merge<T>(T[] array, int lefBound, int mid, int rigBound) where T : IComparable<T>
        {
            T[] sortedCopy = new T[rigBound - lefBound + 1];
            int lefPtr = lefBound, rigPtr = mid + 1, total = 0;

            while (total < sortedCopy.Length)
            {
                if (lefPtr <= mid && (rigPtr > rigBound || array[lefPtr].CompareTo(array[rigPtr]) <= 0))
                {
                    sortedCopy[total++] = array[lefPtr++];
                }
                else
                {
                    sortedCopy[total++] = array[rigPtr++];
                }
            }

            Array.Copy(sortedCopy, 0, array, lefBound, sortedCopy.Length);
        }

        private void mergeSort<T>(T[] array, int lefBound, int rigBound) where T : IComparable<T>
        {
            if (lefBound >= rigBound) return;

            int mid = (lefBound + rigBound) / 2;
            mergeSort(array, lefBound, mid);
            mergeSort(array, mid + 1, rigBound);
            merge(array, lefBound, mid, rigBound);
        }

        public void ParallelSortWithIntermediateMerge<T>(T[] array) where T : IComparable<T>
        {
            int len = array.Length;
            int segmentLen = len / threads;
            Queue<int> completedSegments = new();
            object lockObj = new();

            Task[] tasks = new Task[threads];
            for (int i = 0; i < threads; i++)
            {
                int start = i * segmentLen;
                int end = (i == threads - 1) ? len - 1 : (i + 1) * segmentLen - 1;

                tasks[i] = Task.Run(() =>
                {
                    Thread.Sleep(delay);
                    mergeSort(array, start, end);

                    lock (lockObj)
                    {
                        completedSegments.Enqueue(i);
                        Monitor.Pulse(lockObj);
                    }
                });
            }

            int mergedEnd = -1;
            T[] sortedArray = new T[len];

            for (int mergedSegments = 0; mergedSegments < threads;)
            {
                int segmentToMerge;
                lock (lockObj)
                {
                    while (completedSegments.Count == 0)
                    {
                        Monitor.Wait(lockObj);
                    }
                    segmentToMerge = completedSegments.Dequeue();
                }

                int start = segmentToMerge * segmentLen;
                int end = (segmentToMerge == threads - 1) ? len - 1 : (segmentToMerge + 1) * segmentLen - 1;

                int copyStart = mergedEnd + 1;
                int copyLength = end - start + 1;

                if (copyStart + copyLength > sortedArray.Length)
                {
                    throw new ArgumentException("Недостаточно места в sortedArray для копирования данных.");
                }

                Array.Copy(array, start, sortedArray, copyStart, copyLength);

                int oldMergedEnd = mergedEnd;
                mergedEnd += copyLength;
                merge(sortedArray, 0, oldMergedEnd, mergedEnd);

                mergedSegments++;
            }

            Array.Copy(sortedArray, array, len);
        }

    }

    [TestFixture]
    public class MergeSortTest
    {
        [Test]
        public void Test_SingleThreadedSort()
        {
            MergeSort mrg = new(1, 100);
            int[] array = { 3, 1, 4, 1, 5, 9, 2, 6, 5 };
            mrg.ParallelSortWithIntermediateMerge(array);

            int[] expected = { 1, 1, 2, 3, 4, 5, 5, 6, 9 };
            Assert.That(array, Is.EqualTo(expected));
        }

        [Test]
        public void Test_MultiThreadedSort()
        {
            MergeSort mrg = new(4, 200);
            int[] array = { 3, 1, 4, 1, 5, 9, 2, 6, 5 };
            mrg.ParallelSortWithIntermediateMerge(array);

            int[] expected = { 1, 1, 2, 3, 4, 5, 5, 6, 9 };
            Assert.That(array, Is.EqualTo(expected));
        }

        [Test]
        public void Test_LargeArray()
        {
            MergeSort mrg = new(5, 300);
            int[] array = new int[1000];
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(1000);
            }

            int[] expected = (int[])array.Clone();
            Array.Sort(expected);

            mrg.ParallelSortWithIntermediateMerge(array);
            Assert.That(array, Is.EqualTo(expected));
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // НЕ РАБОТАЕТ
            var writer = new ExtendedTextWrapper(Console.Out);
            var result = new AutoRun(Assembly.GetExecutingAssembly()).Execute(args, writer, TextReader.Null);

            Console.WriteLine(writer.ToString());
            if (result == 0)
                Console.WriteLine("All tests passed!");
            else
                Console.WriteLine("Some tests failed.");
        }
    }
}
