namespace HashWithIDisposable
{
    class Cash
    {
        private readonly List<DisposableObject> _objects;
        private readonly int _maxSize;
        private readonly object _lock = new object();
        private readonly int _maxTime;

        public Cash(int maxSize, int maxTime)
        {
            _maxSize = maxSize;
            _maxTime = maxTime;
            _objects = new List<DisposableObject>();
        }

        public void Add()
        {
            lock (_lock)
            {
                if (_objects.Count >= _maxSize)
                {
                    Console.WriteLine("Cache full. Starting cleanup.");
                    Cleanup();
                }

                if (_objects.Count < _maxSize)
                {
                    var newObject = new DisposableObject(_maxTime);
                    _objects.Add(newObject);
                    Console.WriteLine("Object added.");
                }
                else
                {
                    Console.WriteLine("Cannot add more objects. Cleanup insufficient.");
                }
            }
        }

        private void Cleanup()
        {
            lock (_lock)
            {
                Console.WriteLine("Cleaning up old objects.");
                _objects.RemoveAll(obj =>
                {
                    if (obj.IsOld)
                    {
                        obj.Dispose();
                        return true;
                    }
                    return false;
                });
            }
        }

        public void PrintStatus()
        {
            lock (_lock)
            {
                Console.WriteLine($"Cache status: {_objects.Count} objects in cache.");
                foreach (var obj in _objects)
                {
                    Console.WriteLine($"Object last accessed at: {obj.LastAccessTime}, is old: {obj.IsOld}");
                }
            }
        }
    }
}
