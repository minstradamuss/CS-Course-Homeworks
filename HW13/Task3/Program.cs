public class Node
{
    public int Stop { get; set; }
    public int Distance { get; set; }
}

public class Solution
{
    public int BFS(int start, int target, Dictionary<int, HashSet<int>> busStops, Dictionary<int, HashSet<int>> stopBuses)
    {
        var queue = new Queue<Node>();
        var visitedStops = new HashSet<int>();

        queue.Enqueue(new Node { Stop = start, Distance = 0 });
        visitedStops.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Stop == target)
                return current.Distance;

            foreach (var bus in stopBuses[current.Stop])
            {
                foreach (var stop in busStops[bus])
                {
                    if (!visitedStops.Contains(stop))
                    {
                        if (stop == target)
                        {
                            return current.Distance + 1;
                        }

                        visitedStops.Add(stop);
                        queue.Enqueue(new Node { Stop = stop, Distance = current.Distance + 1 });
                    }
                }
            }
        }

        return -1;
    }

    public int NumBusesToDestination(int[][] routes, int start, int target)
    {
        if (start == target)
            return 0;

        var busStops = new Dictionary<int, HashSet<int>>();
        var stopBuses = new Dictionary<int, HashSet<int>>();

        for (int bus = 0; bus < routes.Length; bus++)
        {
            busStops[bus] = new HashSet<int>();

            foreach (var stop in routes[bus])
            {
                busStops[bus].Add(stop);

                if (!stopBuses.ContainsKey(stop))
                {
                    stopBuses[stop] = new HashSet<int>();
                }

                stopBuses[stop].Add(bus);
            }
        }

        if (!stopBuses.ContainsKey(start) || !stopBuses.ContainsKey(target))
            return -1;

        return BFS(start, target, busStops, stopBuses);
    }
}

public class Program
{
    public static void Main()
    {
        var solution = new Solution();

        int[][] routes1 = { new[] { 1, 2, 7 }, new[] { 3, 6, 7 } };
        int source1 = 1, target1 = 6;
        int result1 = solution.NumBusesToDestination(routes1, source1, target1);
        Console.WriteLine($"Test 1: {result1}");
        System.Diagnostics.Debug.Assert(result1 == 2);

        int[][] routes2 = { new[] { 7, 12 }, new[] { 4, 5, 15 }, new[] { 6 }, new[] { 15, 19 }, new[] { 9, 12, 13 } };
        int source2 = 15, target2 = 12;
        int result2 = solution.NumBusesToDestination(routes2, source2, target2);
        Console.WriteLine($"Test 2: {result2}");
        System.Diagnostics.Debug.Assert(result2 == -1);

        Console.WriteLine("All tests passed!");
    }
}
