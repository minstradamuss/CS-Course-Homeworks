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
        long start = threadIndex;
        int sign = (start % 2 == 0) ? 1 : -1;
        long iterations = 0;

        while (!stopFlag)
        {
            for (long i = start; i < start + iterationsPerCheck * threadCount; i += threadCount)
            {
                sum += sign * (1.0 / (2 * i + 1));
                sign = -sign;
            }

            start += iterationsPerCheck * threadCount;

            if (iterations % iterationsPerCheck == 0 && stopFlag)
                break;
        }

        lock (lockObj)
        {
            partialSums[threadIndex] = sum;
        }
    }
}
