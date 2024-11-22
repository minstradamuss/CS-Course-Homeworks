public class FizzBuzz
{
    private int n;
    private string whichThread = "Number";
    static readonly object _locker = new object();

    public FizzBuzz(int n)
    {
        this.n = n;
    }

    public void Fizz(Action printFizz)
    {
        int total;
        lock (_locker)
        {
            total = numOfFizz(n);
        }

        for (int i = 1; i <= total; i++)
        {

            lock (_locker)
            {
                while (!(whichThread == "Fizz"))
                    Monitor.Wait(_locker);
            }

            printFizz();

            lock (_locker)
            {
                whichThread = "Number";
                Monitor.PulseAll(_locker);
            }
        }
    }

    public void Buzz(Action printBuzz)
    {
        int total;
        lock (_locker)
        {
            total = numOfBuzz(n);
        }

        for (int i = 1; i <= total; i++)
        {
            lock (_locker)
            {
                while (!(whichThread == "Buzz"))
                    Monitor.Wait(_locker);
            }

            printBuzz();

            lock (_locker)
            {
                whichThread = "Number";
                Monitor.PulseAll(_locker);
            }
        }
    }

    public void Fizzbuzz(Action printFizzBuzz)
    {
        int total;
        lock (_locker)
        {
            total = numOfFizzBuzz(n);
        }

        for (int i = 1; i <= total; i++)
        {
            lock (_locker)
            {
                while (!(whichThread == "FizzBuzz"))
                    Monitor.Wait(_locker);
            }

            printFizzBuzz();

            lock (_locker)
            {
                whichThread = "Number";
                Monitor.PulseAll(_locker);
            }
        }
    }

    public void Number(Action<int> printNumber)
    {

        int total = 0;
        lock (_locker)
        {
            total = n;
        }

        for (int i = 1; i <= total; i++)
        {
            lock (_locker)
            {
                whichThread = whoseTurnCalculation(i);

                if (!(whichThread == "Number"))
                {

                    while (!(whichThread == "Number"))
                    {
                        Monitor.PulseAll(_locker);
                        Monitor.Wait(_locker);
                    }
                    continue;
                }

            }
            printNumber(i);
        }
    }


    int numOfFizz(int n)
    {
        int count = 0;
        for (int i = 1; i <= n; i++)
        {
            bool divisibleBy3 = (i % 3 == 0);
            bool divisibleBy5 = (i % 5 == 0);
            if (divisibleBy3 && !divisibleBy5)
                count++;
        }
        return count;
    }

    int numOfBuzz(int n)
    {
        int count = 0;
        for (int i = 1; i <= n; i++)
        {
            bool divisibleBy3 = (i % 3 == 0);
            bool divisibleBy5 = (i % 5 == 0);
            if (!divisibleBy3 && divisibleBy5)
                count++;
        }
        return count;
    }

    int numOfFizzBuzz(int n)
    {
        int count = 0;
        for (int i = 1; i <= n; i++)
        {
            bool divisibleBy3 = (i % 3 == 0);
            bool divisibleBy5 = (i % 5 == 0);
            if (divisibleBy3 && divisibleBy5)
                count++;
        }
        return count;
    }

    string whoseTurnCalculation(int i)
    {
        bool divisibleBy3 = (i % 3 == 0);
        bool divisibleBy5 = (i % 5 == 0);

        bool fizzTurn = (divisibleBy3 && !divisibleBy5);
        bool buzzTurn = (!divisibleBy3 && divisibleBy5);
        bool fizzBuzzTurn = (divisibleBy3 && divisibleBy5);
        bool numberTurn = (!divisibleBy3 && !divisibleBy5);

        if (numberTurn)
            return "Number";
        else if (fizzTurn)
            return "Fizz";
        else if (buzzTurn)
            return "Buzz";
        else
            return "FizzBuzz";
    }
}
