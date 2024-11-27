class Permutations
{
    public static List<string> SinglePermutations(string s) =>
        GetPermutations(s)
            .Select(p => string.Join("", p))
            .Distinct()
            .OrderBy(x => x)
            .ToList();
    public static List<List<T>> GetPermutations<T>(IEnumerable<T> items)
    {
        var result = new List<List<T>>();

        if (!items.Any())
        {
            result.Add(new List<T>());
            return result;
        }

        var used = new HashSet<T>();

        foreach (var item in items.Select((it, i) => (it, i)))
        {
            if (used.Contains(item.it))
                continue;

            var remainingPermutations = GetPermutations(items.Take(item.i).Concat(items.Skip(item.i + 1)));

            foreach (var perm in remainingPermutations)
            {
                var newPerm = new List<T> { item.it };
                newPerm.AddRange(perm);
                result.Add(newPerm);
            }

            used.Add(item.it);
        }

        return result;
    }
}