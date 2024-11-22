class NarrowBridge
{
    private int northCount = 0;
    private int southCount = 0;
    private string? currentDirection = null;
    private readonly object bridgeLock = new object();

    public void CrossBridge(string direction, int carId)
    {
        lock (bridgeLock)
        {
            while ((currentDirection != null && currentDirection != direction) ||
                   (currentDirection == "слева направо" && direction == "справа налево" && northCount > 0) ||
                   (currentDirection == "справа налево" && direction == "слева направо" && southCount > 0))
            {
                Monitor.Wait(bridgeLock);
            }

            currentDirection = direction;
            if (direction == "слева направо")
                northCount++;
            else
                southCount++;

            Console.WriteLine($"Машина {carId} пересекает мост {direction}");
        }

        Thread.Sleep(1000);

        lock (bridgeLock)
        {
            if (direction == "слева направо")
                northCount--;
            else 
                southCount--;

            Console.WriteLine($"Машина {carId} пересекла мост {direction}");

            if ((northCount == 0 && direction == "слева направо") ||
                (southCount == 0 && direction == "справа налево"))
            {
                currentDirection = null;
                Monitor.PulseAll(bridgeLock); 
            }
        }
    }
}
