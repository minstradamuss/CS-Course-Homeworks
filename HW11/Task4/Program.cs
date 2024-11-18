using System;
using System.Threading;

class Program
{
    static int[,] A;
    static int[,] B;
    static int[,] C;
    static int m, n, k;
    static object lockObj = new object();
    static int nextRow = 0, nextCol = 0;
    static int activeThreads;

    static void Main(string[] args)
    {
        Console.WriteLine("Введите размеры матриц (m, n, k):");
        m = int.Parse(Console.ReadLine());
        n = int.Parse(Console.ReadLine());
        k = int.Parse(Console.ReadLine());

        A = new int[m, n];
        B = new int[n, k];
        C = new int[m, k];

        Random rand = new Random();

        Console.WriteLine("Матрица A:");
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                A[i, j] = rand.Next(1, 10);
                Console.Write(A[i, j] + " ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("Матрица B:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < k; j++)
            {
                B[i, j] = rand.Next(1, 10);
                Console.Write(B[i, j] + " ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("Введите количество потоков:");
        int p = int.Parse(Console.ReadLine());

        activeThreads = p;

        Thread[] threads = new Thread[p];
        for (int i = 0; i < p; i++)
        {
            threads[i] = new Thread(CalculateElement);
            threads[i].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine("Результирующая матрица C:");
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < k; j++)
            {
                Console.Write(C[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static void CalculateElement()
    {
        while (true)
        {
            int row, col;

            lock (lockObj)
            {
                if (nextRow >= m)
                {
                    activeThreads--;
                    if (activeThreads == 0)
                    {
                        Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} завершил работу.");
                    }
                    return;
                }

                row = nextRow;
                col = nextCol;

                nextCol++;
                if (nextCol >= k)
                {
                    nextCol = 0;
                    nextRow++;
                }
            }

            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += A[row, i] * B[i, col];
            }

            Thread.Sleep(100);

            C[row, col] = sum;

            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} вычислил элемент C[{row},{col}] = {sum}");
        }
    }
}
