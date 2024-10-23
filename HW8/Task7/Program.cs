class ListComparer : IEqualityComparer<List<int>>
{
    public bool Equals(List<int> x, List<int> y)
    {
        return x.SequenceEqual(y);
    }

    public int GetHashCode(List<int> obj)
    {
        return obj.Aggregate(17, (hash, item) => hash * 23 + item.GetHashCode());
    }
}

class Program
{
    static List<List<int>> ThreeSum(int[] nums)
    {
        if (nums.Length < 3)
        {
            return new List<List<int>>();
        }

        return (from i in Enumerable.Range(0, nums.Length - 2)
                from j in Enumerable.Range(i + 1, nums.Length - (i + 1))
                from k in Enumerable.Range(j + 1, nums.Length - (j + 1))
                where nums[i] + nums[j] + nums[k] == 0
                orderby nums[i], nums[j], nums[k]
                select new List<int> { nums[i], nums[j], nums[k] })
               .Distinct(new ListComparer())
               .ToList();
    }

    static void Main()
    {
        var result1 = ThreeSum(new int[] { 0, 1, -1, -1, 2 });
        var result2 = ThreeSum(new int[] { 0, 0, 0, 5, -5 });
        var result3 = ThreeSum(new int[] { 1, 2, 3 });
        var result4 = ThreeSum(new int[1]);

        PrintResult(result1);
        PrintResult(result2);
        PrintResult(result3);
        PrintResult(result4);
    }

    static void PrintResult(List<List<int>> result)
    {
        Console.Write("{");
        for (int i = 0; i < result.Count; i++)
        {
            Console.Write($"{{ {string.Join(", ", result[i])} }}");
            if (i < result.Count - 1)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine("}");
    }
}
