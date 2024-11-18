namespace H2O
{
    public class H2O
    {
        private readonly Semaphore hydrogen = new(2, 2);
        private readonly Semaphore oxygen = new(1, 1);
        private readonly object lockk = new();
        private int parts = 0;

        public void Hydrogen(Action releaseHydrogen)
        {
            hydrogen.WaitOne();
            BuildMolecule(releaseHydrogen);
            hydrogen.Release();
        }

        public void Oxygen(Action releaseOxygen)
        {
            oxygen.WaitOne();
            BuildMolecule(releaseOxygen);
            oxygen.Release();
        }

        private void BuildMolecule(Action releaseMolecule)
        {
            lock (lockk)
            {
                parts++;
                releaseMolecule();
                if (parts == 3)
                {
                    parts = 0;
                    Monitor.PulseAll(lockk);
                }
                else
                {
                    Monitor.Wait(lockk);
                }
            }
        }
    }

    class Program
    {
        static void RunH2O(string h2oString, HashSet<string> validOutputs)
        {
            var h2o = new H2O();
            var threads = new List<Thread>();
            var result = new List<char>();

            foreach (var c in h2oString)
            {
                var thread = c == 'O'
                    ? new Thread(() => h2o.Oxygen(() => { lock (result) { result.Add('O'); } }))
                    : new Thread(() => h2o.Hydrogen(() => { lock (result) { result.Add('H'); } }));
                threads.Add(thread);
            }

            foreach (var thread in threads)
                thread.Start();

            foreach (var thread in threads)
                thread.Join();

            var output = new string(result.ToArray());

            if (validOutputs.Contains(output))
            {
                Console.WriteLine($"Test passed: Input: {h2oString}, Output: {output}");
            }
            else
            {
                Console.WriteLine($"Test failed: Input: {h2oString}, Output: {output}, Expected one of: {string.Join(", ", validOutputs)}");
            }
        }

        static void Main()
        {
            var testCases = new List<(string input, HashSet<string> validOutputs)>
            {
                ("HOH", new HashSet<string> { "HHO", "HOH", "OHH" }),
                ("OOHHHH", new HashSet<string> { "HHOHHO", "HOHHHO", "OHHHHO", "HHOHOH", "HOHHOH", "OHHHOH", "HHOOHH", "HOHOHH", "OHHOHH" }),
            };

            bool allTestsPassed = true;

            foreach (var (input, validOutputs) in testCases)
            {
                try
                {
                    RunH2O(input, validOutputs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Test failed for input {input}: {ex.Message}");
                    allTestsPassed = false;
                }
            }

            if (allTestsPassed)
            {
                Console.WriteLine("All tests passed");
            }
        }
    }
}
