class Program
{
    static volatile bool stopFlag = false;
    static object lockObj = new object();
    static double[] partialSums;
    static int iterationsPerCheck = 1000000;

    static void Main(string[] args)
    {
        Console.WriteLine("Введите количество потоков:");
        int threadCount = int.Parse(Console.ReadLine());

        partialSums = new double[threadCount];

        Thread[] threads = new Thread[threadCount];

        for (int i = 0; i < threadCount; i++)
        {
            int threadIndex = i;
            threads[i] = new Thread(() => CalculatePi(threadIndex, threadCount));
            threads[i].Start();
        }

        Console.WriteLine("Введите 'stop' для завершения вычислений.");

        while (Console.ReadLine() != "stop") { }

        stopFlag = true;

        foreach (var thread in threads)
        {
            thread.Join();
        }

        double piApproximation = 0;
        foreach (var sum in partialSums)
        {
            piApproximation += sum;
        }

        piApproximation *= 4;

        Console.WriteLine($"Приближенное значение числа pi: {piApproximation}");
    }

    static void CalculatePi(int threadIndex, int threadCount)
    {
        double sum = 0;
        long index = threadIndex;

        while (!stopFlag)
        {
            for (long i = index; i < index + iterationsPerCheck * threadCount; i += threadCount)
            {
                int sign = (i % 2 == 0) ? 1 : -1;
                sum += sign * (1.0 / (2 * i + 1));
            }

            index += iterationsPerCheck * threadCount;

            if (stopFlag)
                break;
        }

        lock (lockObj)
        {
            partialSums[threadIndex] = sum;
        }
    }
}
