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
