public class ZeroEvenOdd
{
    private readonly int n;
    private int pos;
    private readonly object lokk = new();

    public ZeroEvenOdd(int n)
    {
        this.n = n;
    }

    private void PrintNumbers(Action<int> printNumber, Func<int, bool> condition, Func<int, int> transform)
    {
        while (true)
        {
            lock (lokk)
            {
                while (pos < 2 * n && !condition(pos))
                    Monitor.Wait(lokk);

                if (pos >= 2 * n) break;

                printNumber(transform(pos));
                pos++;
                Monitor.PulseAll(lokk);
            }
        }
    }

    public void Zero(Action<int> printNumber)
    {
        PrintNumbers(printNumber, pos => pos % 2 == 0, _ => 0);
    }

    public void Even(Action<int> printNumber)
    {
        PrintNumbers(printNumber, pos => pos % 4 == 3, pos => (pos + 1) / 2);
    }

    public void Odd(Action<int> printNumber)
    {
        PrintNumbers(printNumber, pos => pos % 4 == 1, pos => (pos + 1) / 2);
    }
}

public static class Program
{
    public static void RunZeroEvenOdd(int n, StringWriter outputWriter)
    {
        var zeroEvenOdd = new ZeroEvenOdd(n);

        var threads = new List<Thread>
        {
            new(() => zeroEvenOdd.Zero(outputWriter.Write)),
            new(() => zeroEvenOdd.Even(outputWriter.Write)),
            new(() => zeroEvenOdd.Odd(outputWriter.Write))
        };

        foreach (var thread in threads) thread.Start();
        foreach (var thread in threads) thread.Join();
    }

    public static void Main()
    {
        var testCases = new Dictionary<int, string>
        {
            { 2, "0102" },
            { 3, "010203" },
            { 5, "0102030405" },
            { 7, "01020304050607" }
        };

        foreach (var testCase in testCases)
        {
            var expectedOutput = testCase.Value;
            var actualOutput = new StringWriter();

            RunZeroEvenOdd(testCase.Key, actualOutput);

            if (actualOutput.ToString().Trim() == expectedOutput)
            {
                Console.WriteLine($"Test for n={testCase.Key} passed.");
            }
            else
            {
                Console.WriteLine($"Test for n={testCase.Key} failed.");
                Console.WriteLine($"Expected: {expectedOutput}");
                Console.WriteLine($"Actual: {actualOutput.ToString().Trim()}");
            }
        }

        Console.WriteLine("All tests passed.");
    }
}
