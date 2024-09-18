using Immutable;

namespace Task2
{
    public class Program
    {
        public static void Main()
        {
            ImmutablePerson person = new ImmutablePerson("Zhong", "Li", 30);

            ImmutablePerson olderPerson = person.WithAge(31);

            ImmutablePerson namedPerson = person.WithFirstName("Yanfei");

            Console.WriteLine(person);
            Console.WriteLine(olderPerson);
            Console.WriteLine(namedPerson);
        }
    }
}
