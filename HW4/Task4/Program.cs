class Program
{
    static int SunLoungers(string beach)
    {
        char[] beachArray = beach.ToCharArray();
        int maxNewPeople = 0;
        for (int i = 0; i < beachArray.Length; i++)
        {
            if (beachArray[i] == '0' &&
                (i == 0 || beachArray[i - 1] == '0') &&
                (i == beachArray.Length - 1 || beachArray[i + 1] == '0'))
            {
                beachArray[i] = '1';
                maxNewPeople++;
            }
        }
        return maxNewPeople;
    }

    static void Main(string[] args)
    {
        Console.WriteLine(SunLoungers("10001"));
        Console.WriteLine(SunLoungers("00101"));
        Console.WriteLine(SunLoungers("0"));
        Console.WriteLine(SunLoungers("000"));
    }
}
