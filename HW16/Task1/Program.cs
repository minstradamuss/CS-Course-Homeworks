using System.Text;

const long totalNumbers = 100_000_000;
const int numberLength = 8;
const string targetFolder = "output";

static string GetOrCreateOutputDirectory(string baseDirectory, string targetFolder)
{
    string directoryPath = Path.Combine(baseDirectory, targetFolder);
    Directory.CreateDirectory(directoryPath);
    return directoryPath;
}

try
{
    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

    string outputDirectory = GetOrCreateOutputDirectory(baseDirectory, targetFolder);

    string fileName = Path.Combine(outputDirectory, "numbers.txt");

    var uniqueNumbers = new HashSet<string>();

    using (var writer = new StreamWriter(fileName, false, Encoding.ASCII))
    {
        for (long i = 0; i < totalNumbers; i++)
        {
            long number = (i * 47907813L + 29763927L) % totalNumbers;
            string formattedNumber = number.ToString($"D{numberLength}");
            if (!uniqueNumbers.Add(formattedNumber))
                throw new InvalidOperationException("Обнаружено повторяющееся число.");
            writer.WriteLine(formattedNumber);
        }
    }

    Console.WriteLine();

    if (uniqueNumbers.Count == totalNumbers)
    {
        Console.WriteLine($"Файл \"{fileName}\" успешно создан.");
        Console.WriteLine($"Количество уникальных чисел: {uniqueNumbers.Count}");
    }
    else
    {
        Console.WriteLine($"Ошибка: количество уникальных чисел ({uniqueNumbers.Count}) не соответствует ожидаемому ({totalNumbers}).");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Произошла ошибка: {ex.Message}");
}
