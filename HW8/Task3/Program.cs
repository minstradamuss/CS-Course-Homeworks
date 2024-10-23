public class FooBar
{
    private int n;

    private AutoResetEvent fooEvent = new AutoResetEvent(true);
    private AutoResetEvent barEvent = new AutoResetEvent(false);

    public FooBar(int n)
    {
        this.n = n;
    }

    public void Foo(Action printFoo)
    {
        for (int i = 0; i < n; i++)
        {
            fooEvent.WaitOne();
            printFoo();
            barEvent.Set();
        }
    }

    public void Bar(Action printBar)
    {
        for (int i = 0; i < n; i++)
        {
            barEvent.WaitOne();
            printBar();
            fooEvent.Set();
        }
    }
}

class Program
{
    static void RunFooBar(int n)
    {
        FooBar fooBar = new FooBar(n);

        Thread fooThread = new Thread(() => fooBar.Foo(() => Console.Write("foo")));
        Thread barThread = new Thread(() => fooBar.Bar(() => Console.Write("bar")));

        fooThread.Start();
        barThread.Start();

        fooThread.Join();
        barThread.Join();
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Output for n = 1:");
        RunFooBar(1);

        Console.WriteLine("\n");

        Console.WriteLine("Output for n = 2:");
        RunFooBar(2);
    }
}