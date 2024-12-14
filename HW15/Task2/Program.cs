using System.Reflection;

namespace CustomAttributesDemo
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    class CustomAttribute : Attribute
    {
        public string Author { get; }
        public int RevisionNumber { get; }
        public string Description { get; }
        public string[] Reviewers { get; }

        public CustomAttribute(string author, int revisionNumber, string description, params string[] reviewers)
        {
            Author = author;
            RevisionNumber = revisionNumber;
            Description = description;
            Reviewers = reviewers;
        }
    }

    [Custom("Joe", 2, "Class to work with health data.", "Arnold", "Bernard")]
    public class HealthScore
    {
        [Custom("Andrew", 3, "Method to collect health data.", "Sam", "Alex")]
        public static long CalcScoreData()
        {
            return 87;
        }

        public void GeneralMethod()
        {
            Console.WriteLine("General method with no attribute.");
        }

        [Custom("Egor", 16, "nothing", "nobody")]
        private void InternalMethod(int value)
        {
            Console.WriteLine($"Processing value: {value}");
        }
    }

    public class AttributeTracker
    {
        private void PrintAttributeInfo(CustomAttribute attribute, string memberName)
        {
            Console.WriteLine($"{memberName}:");
            Console.WriteLine($"  Author: {attribute.Author}");
            Console.WriteLine($"  Revision Number: {attribute.RevisionNumber}");
            Console.WriteLine($"  Description: {attribute.Description}");
            Console.WriteLine($"  Reviewers: {string.Join(", ", attribute.Reviewers)}");
            Console.WriteLine();
        }

        private void PrintAttributes(MemberInfo member)
        {
            var attributes = member.GetCustomAttributes<CustomAttribute>(false);
            foreach (var attribute in attributes)
            {
                PrintAttributeInfo(attribute, member.Name);
            }
        }

        public void DisplayClassInfo<T>()
        {
            Type classType = typeof(T);

            PrintAttributes(classType);

            var methods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                PrintAttributes(method);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tracker = new AttributeTracker();
            tracker.DisplayClassInfo<HealthScore>();
        }
    }
}
