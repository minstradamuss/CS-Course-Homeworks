namespace HashWithIDisposable
{
    internal class UnmanagedResource
    {
        public UnmanagedResource()
        {
            Console.WriteLine("Allocating unmanaged resource");
        }

        public void Clean()
        {
            int generation = GC.GetGeneration(this);
            Console.WriteLine($"Cleaning unmanaged resource in {generation} generation");
        }

        ~UnmanagedResource()
        {
            Clean();
            System.Diagnostics.Trace.WriteLine("UnmanagedResource finalizer is called.");
        }
    }
}
