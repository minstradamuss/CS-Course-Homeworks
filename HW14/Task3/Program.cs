class Program
{
    static bool IsOneReplacement(string s1, string s2)
    {
        bool foundDifference = false;

        for (int i = 0; i < s1.Length; i++)
        {
            if (s1[i] != s2[i])
            {
                if (foundDifference)
                    return false;

                foundDifference = true;
            }
        }
        return true;
    }

    static bool IsOneInsertOrDelete(string longer, string shorter)
    {
        int i = 0, j = 0;
        bool foundDifference = false;

        while (i < longer.Length && j < shorter.Length)
        {
            if (longer[i] != shorter[j])
            {
                if (foundDifference)
                    return false;

                foundDifference = true;
                i++;
            }
            else
            {
                i++;
                j++;
            }
        }
        return true;
    }

    static bool AreStringsOneEditApart(string s1, string s2)
    {
        int len1 = s1.Length;
        int len2 = s2.Length;

        if (Math.Abs(len1 - len2) > 1)
            return false;

        if (len1 == len2)
        {
            return IsOneReplacement(s1, s2);
        }
        else if (len1 > len2)
        {
            return IsOneInsertOrDelete(s1, s2);
        }
        else
        {
            return IsOneInsertOrDelete(s2, s1);
        }
    }

    static void Main(string[] args)
    {
        var testCases = new (string s1, string s2, bool expected)[]
        {
            ("test", "tent", true),
            ("tet", "tent", true), 
            ("tentt", "tent", true),
            ("tentto", "tent", false),    
            ("cat", "cut", true),      
            ("cat", "dog", false),     
            ("", "a", true),           
            ("", "aa", false),         
        };

        foreach (var (s1, s2, expected) in testCases)
        {
            bool result = AreStringsOneEditApart(s1, s2);
            Console.WriteLine($"s1: \"{s1}\", s2: \"{s2}\" -> Результат: {result}");
        }
    }
}
