using HashWithIDisposable;
using System.Collections;

namespace Task1
{
    class Program
    {
        private static bool _carryOn;

        static void TestOverflow()
        {
            var cash = new Cash(10, 130);
            for (int i = 0; i < 15; i++)
            {
                cash.Add();
                Thread.Sleep(50);
            }
            cash.PrintStatus();
            Console.WriteLine("End of TestOverflow");
        }

        static void TestGCNotification()
        {
            var cash = new Cash(5, 1000);
            for (int i = 0; i < 10; i++)
            {
                cash.Add();
                Thread.Sleep(500);
            }

            _carryOn = true;

            GC.RegisterForFullGCNotification(10, 10);
            Thread t = new Thread(CheckTheGC);
            t.Start();

            int secondsPassed = 0;
            ArrayList data = new ArrayList();
            while (_carryOn)
            {
                Console.WriteLine($"{secondsPassed++} seconds passed");

                for (int i = 0; i < 1000; i++)
                {
                    data.Add(new byte[1000]);
                }
                Thread.Sleep(1000);
            }

            Console.WriteLine("End of TestGCNotification");
        }

        static void Main(string[] args)
        {
            Thread overflowTest = new Thread(TestOverflow);
            Thread gcNotificationTest = new Thread(TestGCNotification);

            overflowTest.Start();
            gcNotificationTest.Start();

            overflowTest.Join();
            gcNotificationTest.Join();
        }

        private static void CheckTheGC()
        {
            while (true)
            {
                GCNotificationStatus status = GC.WaitForFullGCApproach();
                if (status == GCNotificationStatus.Succeeded)
                {
                    Console.WriteLine("Full GC Nears");
                    break;
                }
            }

            while (true)
            {
                GCNotificationStatus status = GC.WaitForFullGCComplete();
                if (status == GCNotificationStatus.Succeeded)
                {
                    Console.WriteLine("Full GC Complete");
                    break;
                }
            }

            _carryOn = false;
        }
    }
}
