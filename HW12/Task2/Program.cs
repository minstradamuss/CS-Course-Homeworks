class Shower
{
    private static object _lock = new object();
    private static int _currentUsers = 0;
    private static int _currentGender = -1; // -1 = никто, 0 = мужчины, 1 = женщины

    public void EnterShower(int gender)
    {
        lock (_lock)
        {
            while (_currentUsers > 0 && _currentGender != gender)
            {
                Monitor.Wait(_lock);
            }

            _currentUsers++;
            _currentGender = gender;
        }
    }

    public void LeaveShower()
    {
        lock (_lock)
        {
            _currentUsers--;

            if (_currentUsers == 0)
            {
                _currentGender = -1;
                Monitor.PulseAll(_lock);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Shower shower = new Shower();

        void UseShower(int gender, int id)
        {
            Console.WriteLine($"{(gender == 0 ? "Man" : "Woman")} {id} wants to enter.");
            shower.EnterShower(gender);
            Console.WriteLine($"{(gender == 0 ? "Man" : "Woman")} {id} is taking a shower.");
            Thread.Sleep(new Random().Next(1000, 3000));
            shower.LeaveShower();
            Console.WriteLine($"{(gender == 0 ? "Man" : "Woman")} {id} has left the shower.");
        }

        Thread[] threads = new Thread[10];
        Random rnd = new Random();

        for (int i = 0; i < threads.Length; i++)
        {
            int gender = rnd.Next(0, 2);
            int id = i + 1;

            threads[i] = new Thread(() => UseShower(gender, id));
            threads[i].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
    }
}
