class MatrixMultiplication
{
    private static int[,] A, B, C;
    private static int activeThreads;
    static int m, n, k;

    static int i = 0;

    static int currentColumn = 0;
    static object lockk = new object();

    static void Calc()
    {
        int row;
        int col;
        while (true)
        {
            lock (lockk)
            {
                if (i >= m)
                    break;

                row = i;
                col = currentColumn;
                currentColumn++;
                if (currentColumn >= k)
                {
                    i++;
                    currentColumn = 0;
                }
            }

            if (row >= m)
                break;

            C[row, col] = 0;
            for (int l = 0; l < n; l++)
            {
                C[row, col] += A[row, l] * B[l, col];
            }
        }
    }

    static void Main()
    {
        Console.WriteLine("Введите размеры матриц (m, n, k):");
        Console.Write("m = ");
        m = int.Parse(Console.ReadLine());

        Console.Write("n = ");
        n = int.Parse(Console.ReadLine());

        Console.Write("k = ");
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
            threads[i] = new Thread(Calc);
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
}
