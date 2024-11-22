namespace Task1
{
    internal class Program
    {
        static void Main()
        {
            FizzBuzz foo = new FizzBuzz(15);
            void printFizz()
            {
                Console.WriteLine("Fizz");
            }

            void printBuzz()
            {
                Console.WriteLine("Buzz");
            }

            void printFizzBuzz()
            {
                Console.WriteLine("FizzBuzz");
            }

            void printNumber(int i)
            {
                Console.WriteLine(i);
            }

            Thread threadA = new Thread(() => foo.Fizz(printFizz));
            Thread threadB = new Thread(() => foo.Buzz(printBuzz));
            Thread threadC = new Thread(() => foo.Fizzbuzz(printFizzBuzz));
            Thread threadD = new Thread(() => foo.Number(printNumber));

            threadA.Start();
            threadB.Start();
            threadC.Start();
            threadD.Start();
        }
    }
}
