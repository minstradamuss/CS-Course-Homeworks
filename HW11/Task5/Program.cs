using System;
using System.Threading;

public class CMyBarrier : IDisposable
{
    private readonly int _participantCount;
    private int _currentCount;
    private readonly object _lock = new object();
    private ManualResetEvent _barrierEvent;
    private bool _disposed;

    public CMyBarrier(int participantCount)
    {
        if (participantCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(participantCount), "Participant count must be greater than zero.");

        _participantCount = participantCount;
        _currentCount = participantCount;
        _barrierEvent = new ManualResetEvent(false);
    }

    public bool SignalAndWait(TimeSpan timeout)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(CMyBarrier));

        lock (_lock)
        {
            if (_currentCount <= 0)
                throw new InvalidOperationException("Barrier has already been triggered.");

            _currentCount--;

            if (_currentCount == 0)
            {
                _barrierEvent.Set();
                return true;
            }
        }

        bool result = _barrierEvent.WaitOne(timeout);
        if (!result)
        {
            lock (_lock)
            {
                if (_currentCount > 0)
                    _currentCount++; 
            }
        }
        return result;
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _barrierEvent.Dispose();
        _disposed = true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        const int participantCount = 3;
        CMyBarrier barrier = new CMyBarrier(participantCount);

        void Worker(int id)
        {
            Console.WriteLine($"Thread {id} is working...");
            Thread.Sleep(new Random().Next(1000, 3000));
            Console.WriteLine($"Thread {id} is waiting at the barrier.");
            if (barrier.SignalAndWait(TimeSpan.FromSeconds(5)))
            {
                Console.WriteLine($"Thread {id} has passed the barrier.");
            }
            else
            {
                Console.WriteLine($"Thread {id} timed out at the barrier.");
            }
        }

        Thread[] threads = new Thread[participantCount];
        for (int i = 0; i < participantCount; i++)
        {
            int localId = i;
            threads[i] = new Thread(() => Worker(localId));
            threads[i].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        barrier.Dispose();
        Console.WriteLine("All threads have completed.");
    }
}
