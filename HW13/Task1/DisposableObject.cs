namespace HashWithIDisposable
{
    class DisposableObject : IDisposable
    {
        private readonly UnmanagedResource _resource;
        public DateTime LastAccessTime { get; private set; }
        public bool IsOld => (DateTime.Now - LastAccessTime).TotalMilliseconds >= _maxTime;
        private readonly int _maxTime;

        public DisposableObject(int maxTime)
        {
            _maxTime = maxTime;
            _resource = new UnmanagedResource();
            LastAccessTime = DateTime.Now;
        }

        public void Access()
        {
            LastAccessTime = DateTime.Now;
        }

        public void Dispose()
        {
            _resource.Clean();
            GC.SuppressFinalize(this);
        }

        ~DisposableObject()
        {
            Dispose();
            System.Diagnostics.Trace.WriteLine("DisposableObject finalizer is called.");
        }
    }
}
