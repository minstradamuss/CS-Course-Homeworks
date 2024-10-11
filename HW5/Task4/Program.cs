class NaturalJoin
{
    static void Main(string[] args)
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string projectDirectory = FindProjectDirectory(baseDirectory, "tables");
        if (projectDirectory == null)
        {
            Console.WriteLine("Папка 'tables' не найдена.");
            return;
        }
        string tablesDirectory = Path.Combine(projectDirectory, "tables");
        string file1 = Path.Combine(tablesDirectory, "file1.txt");
        string file2 = Path.Combine(tablesDirectory, "file2.txt");
        string outputFile = Path.Combine(tablesDirectory, "result.txt");

        if (!File.Exists(file1) || !File.Exists(file2))
        {
            Console.WriteLine("Один или оба входных файла не найдены в директории tables.");
            return;
        }

        var table1 = ReadTable(file1);
        var table2 = ReadTable(file2);

        PerformNaturalJoin(table1, table2, outputFile);

        Console.WriteLine($"Результат записан в файл: {outputFile}");
    }

    static Dictionary<string, List<string[]>> ReadTable(string filePath)
    {
        var table = new Dictionary<string, List<string[]>>();

        foreach (var line in File.ReadLines(filePath))
        {
            var fields = line.Split('\t');
            var key = fields[0];

            if (!table.ContainsKey(key))
                table[key] = new List<string[]>();

            table[key].Add(fields);
        }
        return table;
    }

    static string FindProjectDirectory(string currentDirectory, string targetFolder)
    {
        DirectoryInfo directory = new DirectoryInfo(currentDirectory);
        while (directory != null && !Directory.Exists(Path.Combine(directory.FullName, targetFolder)))
            directory = directory.Parent;
        return directory?.FullName;
    }

    static void PerformNaturalJoin(Dictionary<string, List<string[]>> table1, Dictionary<string, List<string[]>> table2, string outputFile)
    {
        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            foreach (var key in table1.Keys)
            {
                if (table2.ContainsKey(key))
                {
                    foreach (var record1 in table1[key])
                    {
                        foreach (var record2 in table2[key])
                        {
                            writer.WriteLine($"{key}\t{string.Join("\t", record1[1..])}\t{string.Join("\t", record2[1..])}");
                        }
                    }
                }
            }
        }
    }
}
