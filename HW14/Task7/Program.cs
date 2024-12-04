namespace LongestSubstring
{
    class Program
    {
        static string LongestRepetitiveSubstring(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "";

            string longest = "";

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = i + 1; j < s.Length; j++)
                {
                    int length = 0;
                    while (j + length < s.Length && s[i + length] == s[j + length])
                    {
                        length++;
                    }

                    if (length > longest.Length)
                    {
                        longest = s.Substring(i, length);
                    }
                }
            }

            return longest;
        }

        static void Main()
        {
            var testCases = new (string input, string expected)[]
            {
                ("", ""),
                ("a", ""),
                ("abcd", ""),
                ("abacaba", "aba"),
                ("mask4cask", "ask"),
                ("sdwoerkjlswoerwoettq", "woer"),
                ("aaaaa", "aaaa"),
                ("abacababacbac", "abac"),
            };

            bool allTestsPassed = true;

            foreach (var (input, expected) in testCases)
            {
                string result = LongestRepetitiveSubstring(input);
                if (result != expected)
                {
                    Console.WriteLine($"Test failed for input \"{input}\". Expected: \"{expected}\", Got: \"{result}\"");
                    allTestsPassed = false;
                }
            }

            if (allTestsPassed)
            {
                Console.WriteLine("All test cases passed!");
            }
        }
    }
}
