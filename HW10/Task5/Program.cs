using NUnit.Framework;

namespace MyWaitAll
{
    public class CMyWaitAll : IDisposable
    {
        private readonly AutoResetEvent event_m = new(false);
        private readonly object lck = new();
        private readonly List<bool> flag;
        private readonly int AtomsNumber;
        private int OperatedCnt;

        public CMyWaitAll(int atomsNumber)
        {
            if (atomsNumber <= 0)
            {
                throw new ArgumentException("atomsNumber should be > 0", nameof(atomsNumber));
            }

            AtomsNumber = atomsNumber;
            OperatedCnt = 0;

            flag = new List<bool>(AtomsNumber + 1);
            for (var i = 0; i <= atomsNumber; i++)
            {
                flag.Add(false);
            }
        }

        public void SetAtomSignaled(int atomId)
        {
            if (atomId < 0 || atomId > AtomsNumber)
            {
                throw new ArgumentOutOfRangeException(nameof(atomId), "atomId must be between 0 and atomsNumber inclusive");
            }

            lock (lck)
            {
                if (flag[atomId])
                    return;

                flag[atomId] = true;
                OperatedCnt++;

                if (OperatedCnt == AtomsNumber + 1)
                {
                    event_m.Set();
                }
            }
        }

        public bool Wait(TimeSpan timeout)
        {
            return event_m.WaitOne(timeout);
        }

        public void Dispose()
        {
            event_m.Dispose();
        }
    }


    public class CMyWaitAllTests
    {
        public static void Test_WaitWithTimeout()
        {
            var atomsNumber = 67;
            var waitAll = new CMyWaitAll(atomsNumber);

            var result = waitAll.Wait(TimeSpan.FromMilliseconds(500));
            Assert.That(!result, "Wait should have timed out");

            for (int i = 0; i <= atomsNumber; i++)
                waitAll.SetAtomSignaled(i);

            result = waitAll.Wait(TimeSpan.FromMilliseconds(500));
            Assert.That(result, "Wait should have completed successfully");

            waitAll.Dispose();
        }
    }

    public static class Program
    {
        public static int Main(string[] args)
        {
            CMyWaitAllTests.Test_WaitWithTimeout();
            Console.WriteLine("Passed!");
            return 0;
        }
    }
}
