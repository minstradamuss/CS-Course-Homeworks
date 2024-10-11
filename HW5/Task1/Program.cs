using System.Collections;

public class Lake : IEnumerable<int>
{
    private List<int> stones;

    public Lake(params int[] stones)
    {
        this.stones = new List<int>(stones);
    }

    public IEnumerator<int> GetEnumerator()
    {
        foreach (var stone in stones)
        {
            if (stone % 2 != 0)
                yield return stone;
        }

        for (int i = stones.Count - 1; i >= 0; i--)
        {
            if (stones[i] % 2 == 0)
                yield return stones[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Lake lake1 = new Lake(1, 2, 3, 4, 5, 6, 7, 8);
        Console.WriteLine(string.Join(", ", lake1));

        Lake lake2 = new Lake(13, 23, 1, -8, 4, 9);
        Console.WriteLine(string.Join(", ", lake2));
    }
}
