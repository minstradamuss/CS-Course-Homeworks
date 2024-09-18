namespace Task3
{
    public interface IBase
    {
        void Print();
    }

    public struct BaseStruct : IBase
    {
        public void Print()
        {
            Console.WriteLine("BaseStruct: Print method from base struct.");
        }
    }

    public struct DerivedStruct : IBase
    {
        private BaseStruct baseStruct;

        public void Print()
        {
            baseStruct.Print();
            Console.WriteLine("DerivedStruct: Additional behavior in derived struct.");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            IBase baseInstance = new BaseStruct();
            baseInstance.Print();

            IBase derivedInstance = new DerivedStruct();
            derivedInstance.Print();
        }
    }
}
