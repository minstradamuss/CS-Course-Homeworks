public class Solution
{
    public int Envelopes(int[][] envelopes)
    {
        var arr = envelopes.OrderBy(x => x[0]).ThenByDescending(x => x[1]).ToArray();
        var ls = new List<int>();

        for (int i = 0; i < arr.Length; i++)
        {
            int bx = arr[i][1];
            if (ls.Count == 0)
                ls.Add(bx);
            else if (ls[ls.Count - 1] < bx)
                ls.Add(bx);
            else
            {
                int start = 0;
                int end = ls.Count - 1;
                int ind = -1;
                while (start <= end)
                {
                    int mid = start + (end - start) / 2;
                    if (ls[mid] >= bx)
                    {
                        ind = mid;
                        end = mid - 1;
                    }
                    else
                        start = mid + 1;
                }
                ls[ind] = bx;
            }
        }
        return ls.Count;
    }

    static void Main(string[] args)
    {
        var testCases = new (int[][] envelopes, int expected)[]
        {
            (new int[][] { new int[] { 5, 4 }, new int[] { 6, 4 }, new int[] { 6, 7 }, new int[] { 2, 3 } }, 3),
            (new int[][] { new int[] { 1, 1 }, new int[] { 1, 1 }, new int[] { 1, 1 } }, 1),
            (new int[][] { new int[] { 4, 5 }, new int[] { 4, 6 }, new int[] { 6, 7 }, new int[] { 2, 3 }, new int[] { 1, 1 } }, 4),
            (new int[][] { new int[] { 1, 8 }, new int[] { 2, 3 }, new int[] { 4, 5 }, new int[] { 3, 4 } }, 3),
            (new int[][] { new int[] { 5, 5 }, new int[] { 6, 6 }, new int[] { 7, 7 }, new int[] { 8, 8 } }, 4),
            (new int[][] { new int[] { 10, 20 }, new int[] { 30, 40 }, new int[] { 30, 60 } }, 2),
            (new int[][] { new int[] { 1, 1 } }, 1)
        };

        bool allTestsPassed = true;

        var solution = new Solution();

        foreach (var (envelopes, expected) in testCases)
        {
            int result = solution.Envelopes(envelopes);
            if (result != expected)
            {
                Console.WriteLine($"Test failed for input {string.Join(", ", envelopes.Select(e => $"[{e[0]}, {e[1]}]"))}. Expected: {expected}, Got: {result}");
                allTestsPassed = false;
            }
        }

        if (allTestsPassed)
        {
            Console.WriteLine("All test cases passed!");
        }
    }
}
