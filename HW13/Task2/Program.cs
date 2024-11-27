class Program
{
    static void Main()
    {
        TestPermutations("AB", "AB BA");
        TestPermutations("CD", "CD DC");
        TestPermutations("EF", "EF FE");
        TestPermutations("NOT", "NOT NTO ONT OTN TNO TON");
        TestPermutations("RAM", "AMR ARM MAR MRA RAM RMA");
        TestPermutations("YAW", "AWY AYW WAY WYA YAW YWA");

        Console.WriteLine("All tests passed!");
    }

    static void TestPermutations(string input, string expectedOutput)
    {
        var result = Permutations.SinglePermutations(input);
        var resultString = string.Join(" ", result);

        if (resultString != expectedOutput)
        {
            Console.WriteLine($"Test failed for input: {input}");
            Console.WriteLine($"Expected: {expectedOutput}");
            Console.WriteLine($"Got: {resultString}");
            Environment.Exit(1);
        }
        else
        {
            Console.WriteLine($"Test passed for input: {input}");
        }
    }
}
