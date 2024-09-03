using System;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        string password = GeneratePassword();
        Console.WriteLine($"Generated Password: {password}");

        string password2 = GeneratePassword();
        Console.WriteLine($"Generated Password: {password2}");

        string password3 = GeneratePassword();
        Console.WriteLine($"Generated Password: {password3}");

        string password4 = GeneratePassword();
        Console.WriteLine($"Generated Password: {password4}");

        string password5 = GeneratePassword();
        Console.WriteLine($"Generated Password: {password5}");
    }

    static string GeneratePassword()
    {
        Random random = new Random();
        int passwordLength = random.Next(6, 21);

        char[] uppercaseLetters = Enumerable.Range(0, 2).Select(x => (char)random.Next('A', 'Z' + 1)).ToArray();

        char underscore = '_';

        int numDigits = random.Next(1, 6);
        char[] digits = GenerateDigits(numDigits, random);

        int remainingLength = passwordLength - (uppercaseLetters.Length + digits.Length + 1);
        char[] remainingChars = Enumerable.Range(0, remainingLength)
                                          .Select(x => (char)random.Next('a', 'z' + 1))
                                          .ToArray();

        char[] passwordChars = uppercaseLetters.Concat(new[] { underscore })
                                               .Concat(digits)
                                               .Concat(remainingChars)
                                               .OrderBy(x => random.Next())
                                               .ToArray();

        while (!IsValidPassword(passwordChars))
        {
            passwordChars = passwordChars.OrderBy(x => random.Next()).ToArray();
        }

        return new string(passwordChars);
    }

    static char[] GenerateDigits(int count, Random random)
    {
        char[] digits = new char[count];
        for (int i = 0; i < count; i++)
        {
            char digit;
            do
            {
                digit = (char)random.Next('0', '9' + 1);
            }
            while (i > 0 && digits[i - 1] == digit);
            digits[i] = digit;
        }
        return digits;
    }

    static bool IsValidPassword(char[] password)
    {
        string passwordStr = new string(password);

        int uppercaseCount = passwordStr.Count(char.IsUpper);
        if (uppercaseCount < 2)
            return false;

        int underscoreCount = passwordStr.Count(c => c == '_');
        if (underscoreCount != 1)
            return false;

        int digitCount = passwordStr.Count(char.IsDigit);
        if (digitCount > 5)
            return false;

        for (int i = 1; i < passwordStr.Length; i++)
        {
            if (char.IsDigit(passwordStr[i]) && char.IsDigit(passwordStr[i - 1]))
                return false;
        }

        return true;
    }
}
