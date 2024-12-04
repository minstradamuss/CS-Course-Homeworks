using System.Text;

namespace FibonacciWord
{
    class Program
    {
        static string StringyFib(int number)
        {
            if (number < 2)
            {
                return "invalid";
            }

            var fibSeq = new StringBuilder[number];
            fibSeq[0] = new StringBuilder("b");
            fibSeq[1] = new StringBuilder("a");

            for (int i = 2; i < number; i++)
            {
                fibSeq[i] = new StringBuilder(fibSeq[i - 1].ToString()).Append(fibSeq[i - 2]);
            }

            var result = new StringBuilder();
            for (int i = 0; i < number; i++)
            {
                if (i > 0) result.Append(", ");
                result.Append(fibSeq[i]);
            }

            return result.ToString();
        }

        static void Main(string[] args)
        {
            var testCases = new (int input, string expected)[]
            {
                (1, "invalid"),
                (2, "b, a"),
                (3, "b, a, ab"),
                (5, "b, a, ab, aba, abaab"),
                (7, "b, a, ab, aba, abaab, abaababa, abaababaabaab")
            };

            bool allTestsPassed = true;

            foreach (var (input, expected) in testCases)
            {
                var result = StringyFib(input);
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
