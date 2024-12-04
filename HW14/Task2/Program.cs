using System.Runtime.ExceptionServices;

class Program
{
    static ExceptionDispatchInfo? capturedException;

    static void DivideByZero()
    {
        int a = 5;
        int b = 0;
        _ = a / b;
    }

    static void ThrowCapturedException()
    {
        if (capturedException != null)
        {
            capturedException.Throw();
        }
        else
        {
            Console.WriteLine("Нет зарегистрированных исключений.");
        }
    }

    static void Main(string[] args)
    {
        try
        {
            DivideByZero();
        }
        catch (Exception ex)
        {
            capturedException = ExceptionDispatchInfo.Capture(ex);
            Console.WriteLine("Исключение зарегистрировано: " + ex.Message);
        }

        Console.WriteLine("Выполнение продолжается...");
        Console.WriteLine("Выполнение продолжается...");
        Console.WriteLine("Выполнение продолжается...");

        try
        {
            ThrowCapturedException();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Повторно вызвано исключение: " + ex.Message);
            //Console.WriteLine("Стек вызовов:\n" + ex.StackTrace);
        }
    }
}
