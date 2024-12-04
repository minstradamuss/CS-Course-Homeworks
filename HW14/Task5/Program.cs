using System.Text;

namespace StringSorter
{
    class Program
    {
        static string Sorting(string input)
        {
            var sb = new StringBuilder();

            for (var i = 'a'; i <= 'z'; i++)
            {
                foreach (var ch in input)
                {
                    if (ch == i) 
                        sb.Append(ch);
                }

                var capital = char.ToUpper(i);
                foreach (var ch in input)
                {
                    if (ch == capital)
                        sb.Append(ch);
                }
            }

            for (var i = '0'; i <= '9'; i++)
            {
                foreach (var ch in input)
                {
                    if (ch == i)
                        sb.Append(ch);
                }
            }

            return sb.ToString();
        }

        static void Main(string[] args)
        {
            var testCases = new (string input, string expected)[]
            {
                ("eA2a1E", "aAeE12"),
                ("Re4r", "erR4"),
                ("6jnM31Q", "jMnQ136"),
                ("846ZIbo", "bIoZ468"),
                ("", ""),
                ("123", "123"),
                ("abcABC", "aAbBcC"),
                ("1aA2bB", "aAbB12")
            };

            bool allTestsPassed = true;

            foreach (var (input, expected) in testCases)
            {
                var result = Sorting(input);
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
