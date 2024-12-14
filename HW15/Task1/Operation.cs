namespace BlackBox
{
    public struct Operation
    {
        public string MethodName { get; }
        public int Argument { get; }

        public Operation(string operation)
        {
            if (string.IsNullOrWhiteSpace(operation))
            {
                throw new ArgumentException("Operation cannot be null or empty!");
            }

            var openingBracketIndex = operation.IndexOf('(');
            var closingBracketIndex = operation.IndexOf(')');

            if (openingBracketIndex == -1 || closingBracketIndex == -1 || closingBracketIndex <= openingBracketIndex + 1)
            {
                throw new ArgumentException("Invalid operation syntax!");
            }

            MethodName = operation.Substring(0, openingBracketIndex).Trim();
            if (string.IsNullOrWhiteSpace(MethodName))
            {
                throw new ArgumentException("Method name cannot be empty!");
            }

            var argumentString = operation.Substring(openingBracketIndex + 1, closingBracketIndex - openingBracketIndex - 1).Trim();

            if (!int.TryParse(argumentString, out var parsedArgument))
            {
                throw new ArgumentException("Invalid argument format!");
            }

            Argument = parsedArgument;
            if (operation != $"{MethodName}({Argument})")
            {
                throw new ArgumentException("Operation string format mismatch!");
            }
        }
    }
}
