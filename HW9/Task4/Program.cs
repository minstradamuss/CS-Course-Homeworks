using System.Globalization;

class Program
{
    private static readonly object fileLock = new();

    static void Main()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string projectDirectory = FindProjectDirectory(baseDirectory, "data");

        if (projectDirectory == null)
        {
            Console.WriteLine("The 'data' folder was not found.");
            return;
        }

        string tablesDirectory = Path.Combine(projectDirectory, "data");
        List<string> filesList = new() { Path.Combine(tablesDirectory, "input1.txt"), Path.Combine(tablesDirectory, "input2.txt"), Path.Combine(tablesDirectory, "input3.txt") };

        RunFileCalculator(4, filesList, tablesDirectory);
    }

    static string FindProjectDirectory(string currentDirectory, string targetFolder)
    {
        DirectoryInfo directory = new DirectoryInfo(currentDirectory);
        while (directory != null && !Directory.Exists(Path.Combine(directory.FullName, targetFolder)))
            directory = directory.Parent;
        return directory?.FullName;
    }

    static Command ReadCommand(string filename) => new(
        int.Parse(File.ReadLines(filename).First()),
        File.ReadLines(filename).Skip(1)
            .First().Split(" ")
            .Select(num => Convert.ToDouble(num, CultureInfo.InvariantCulture))
            .ToList()
    );

    static void WriteResult(string outputPath, string filename, double result)
    {
        lock (fileLock)
        {
            using var writer = File.AppendText(outputPath);
            writer.WriteLine($"Result for file {Path.GetFileName(filename)}: {result}");
        }
    }

    static void ComputeFile(string fileName, string outputPath) =>
        WriteResult(outputPath, fileName, ReadCommand(fileName).Compute());

    static void RunFileCalculator(int threadsCnt, List<string> filesList, string outputDirectory)
    {
        string outputPath = Path.Combine(outputDirectory, "out.dat");
        File.WriteAllText(outputPath, string.Empty);

        var threads = filesList.Chunk((filesList.Count + threadsCnt - 1) / threadsCnt)
            .Select(batch => new Thread(() => batch.ToList().ForEach(file => ComputeFile(file, outputPath))))
            .ToList();

        threads.ForEach(t => { t.Start(); t.Join(); });

        Console.WriteLine("Results have been saved to out.dat");
    }
}

class Command
{
    public int Operation { get; }
    public List<double> Operands { get; }

    public Command(int operation, List<double> operands)
    {
        Operation = operation;
        Operands = operands;
    }

    public double Compute() => Operation switch
    {
        1 => Operands.Sum(),
        2 => Operands.Aggregate((a, b) => a * b),
        3 => Operands.Sum(a => a * a),
        _ => throw new Exception("Unknown operation: " + Operation)
    };
}
