class Program
{
    public static string FindFile(string filename, string directoryPath = "c:\\Temp\\")
    {
        try
        {
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                if (Path.GetFileName(file).Equals(filename, StringComparison.OrdinalIgnoreCase))
                {
                    return file;
                }
            }

            foreach (var subDir in Directory.GetDirectories(directoryPath))
            {
                try
                {
                    string result = FindFile(filename, subDir);
                    if (!string.IsNullOrEmpty(result))
                    {
                        return result;
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine($"Access denied: {e.Message}");
                }
            }
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine($"Access denied: {e.Message}");
        }

        return null;
    }

    public static void Main()
    {
        string basePath = "c:\\Temp\\TestEnvironment\\";
        Directory.CreateDirectory(basePath);

        try
        {
            Directory.CreateDirectory(Path.Combine(basePath, "SubDir1"));
            Directory.CreateDirectory(Path.Combine(basePath, "SubDir2"));

            File.WriteAllText(Path.Combine(basePath, "file1.txt"), "Test content 1");
            File.WriteAllText(Path.Combine(basePath, "SubDir1", "MyFile.txt"), "Test content 2");
            File.WriteAllText(Path.Combine(basePath, "SubDir2", "file3.txt"), "Test content 3");

            Console.WriteLine("Running tests...");

            string foundFile = FindFile("MyFile.txt", basePath);
            Console.WriteLine(foundFile != null && foundFile.Contains("MyFile.txt") ? "Test 1 passed" : "Test 1 failed");

            string notFoundFile = FindFile("nonexistent.txt", basePath);
            Console.WriteLine(notFoundFile == null ? "Test 2 passed" : "Test 2 failed");
        }
        finally
        {
            if (Directory.Exists(basePath))
            {
                Directory.Delete(basePath, true);
            }
            Console.WriteLine("Test environment cleaned up.");
        }
    }
}
