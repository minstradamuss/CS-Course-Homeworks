public class ListOfStacks<T>
{
    private readonly List<Stack<T>> stacks;
    private readonly int stackCapacity;
    public ListOfStacks(int stackCapacity)
    {
        if (stackCapacity <= 0)
            throw new ArgumentException("Размер стека должен быть > 0");
        this.stackCapacity = stackCapacity;
        stacks = new List<Stack<T>>();
        stacks.Add(new Stack<T>());
    }

    public void Push(T value)
    {
        Stack<T> currentStack = stacks[stacks.Count - 1];
        if (currentStack.Count >= stackCapacity)
        {
            currentStack = new Stack<T>();
            stacks.Add(currentStack);
        }
        currentStack.Push(value);
    }

    public T Pop()
    {
        if (stacks.Count == 0 || (stacks.Count == 1 && stacks[0].Count == 0))
            throw new InvalidOperationException("Пустой стек");
        Stack<T> currentStack = stacks[stacks.Count - 1];
        T value = currentStack.Pop();
        if (currentStack.Count == 0 && stacks.Count > 1)
            stacks.RemoveAt(stacks.Count - 1);
        return value;
    }

    public int GetNumberOfStacks()
    {
        return stacks.Count;
    }
}

class Program
{
    static void Main(string[] args)
    {
        ListOfStacks<int> listOfStacks = new ListOfStacks<int>(3);
        listOfStacks.Push(1);
        listOfStacks.Push(2);
        listOfStacks.Push(3); 
        listOfStacks.Push(4);
        listOfStacks.Push(5);
        Console.WriteLine($"Кол-во стеков: {listOfStacks.GetNumberOfStacks()}");
        Console.WriteLine($"Pop: {listOfStacks.Pop()}");
        Console.WriteLine($"Pop: {listOfStacks.Pop()}");
        Console.WriteLine($"Кол-во стеков: {listOfStacks.GetNumberOfStacks()}");
    }
}
