public class SparseMatrix3D<T>
{
    private Dictionary<(int i, int j, int k), T> data;

    public SparseMatrix3D()
    {
        data = new Dictionary<(int i, int j, int k), T>();
    }

    public T this[int i, int j, int k]
    {
        get
        {
            return data.TryGetValue((i, j, k), out T value) ? value : default(T);
        }
        set
        {
            if (EqualityComparer<T>.Default.Equals(value, default(T)))
            {
                data.Remove((i, j, k));
            }
            else
            {
                data[(i, j, k)] = value;
            }
        }
    }

    public void ForEach(Action<int, int, int, T> action)
    {
        foreach (var kvp in data)
        {
            var (i, j, k) = kvp.Key;
            var value = kvp.Value;
            action(i, j, k, value);
        }
    }
}

class Program
{
    static void Main()
    {
        var matrix = new SparseMatrix3D<string>();

        matrix[1, 2, 3] = "A";
        matrix[4, 5, 6] = "B";
        matrix[7, 8, 9] = "C";

        Console.WriteLine(matrix[1, 2, 3]);
        Console.WriteLine(matrix[4, 5, 6]);
        Console.WriteLine(matrix[7, 8, 9]);
        Console.WriteLine(matrix[0, 0, 0]);  // null

        Console.WriteLine("Non-empty elements:");
        matrix.ForEach((i, j, k, value) =>
        {
            Console.WriteLine($"matrix[{i}, {j}, {k}] = {value}");
        });

        matrix[4, 5, 6] = null;
        Console.WriteLine("\nNon-empty elements:");
        matrix.ForEach((i, j, k, value) =>
        {
            Console.WriteLine($"matrix[{i}, {j}, {k}] = {value}");
        });
    }
}
