using System.Reflection;
using BlackBox;

class Program
{
    private const string InnerValueFieldName = "innerValue";
    private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic;

    static void Main()
    {
        var blackBoxType = typeof(BlackBox.BlackBox);
        var blackBoxConstructor = blackBoxType.GetConstructor(Flags, null, new[] { typeof(int) }, null) ?? throw new InvalidOperationException("Constructor not found for BlackBox class.");

        var blackBoxEntity = blackBoxConstructor.Invoke(new object[] { 0 });

        while (true)
        {
            try
            {
                Console.Write("Enter operation (e.g., Add(5)): ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Operation cannot be empty. Please try again.");
                    continue;
                }

                var operation = new Operation(input);

                var blackBoxMethod = blackBoxType.GetMethod(operation.MethodName, Flags) ?? throw new ArgumentException($"Invalid method name: {operation.MethodName}");

                blackBoxMethod.Invoke(blackBoxEntity, new object[] { operation.Argument });

                var innerValueField = blackBoxType.GetField(InnerValueFieldName, Flags) ?? throw new InvalidOperationException($"Field '{InnerValueFieldName}' not found.");

                var result = innerValueField.GetValue(blackBoxEntity);
                Console.WriteLine($"Result: {result}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
