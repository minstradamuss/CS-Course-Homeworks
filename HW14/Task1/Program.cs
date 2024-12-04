using System.Text;

class Program
{
    static byte[] SerializeString(string text)
    {
        return Encoding.UTF8.GetBytes(text);
    }

    static string DeserializeString(byte[] data)
    {
        return Encoding.UTF8.GetString(data);
    }

    static void ProcessString(string language, string originalText)
    {
        Console.WriteLine($"\n{language}:");
        Console.WriteLine($"Original: {originalText}");

        byte[] serializedBytes = SerializeString(originalText);
        Console.WriteLine("Serialized: " + BitConverter.ToString(serializedBytes));

        string deserializedText = DeserializeString(serializedBytes);
        Console.WriteLine($"Deserialized: {deserializedText}");
    }

    static void Main()
    {
        string russianText = "Каждый раз нужно прыгать со скалы и отращивать крылья по пути вниз.";
        string germanText = "Möge die Macht mit dir sein.";
        string japaneseText = "風立ちぬ";
        // почему-то у меня в консоли японский только как знаки вопроса отображается,
        // но если скопировать их и куда-то вставить, там японский текст
        // не поняла почему так, но видимо все равно работает

        Console.OutputEncoding = Encoding.UTF8;

        ProcessString("Russian", russianText);
        ProcessString("German", germanText);
        ProcessString("Japanese", japaneseText);
    }
}
