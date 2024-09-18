namespace Task6
{
    public class WaterTrapCalculator
    {
        public static int CalculateVolume(int[] height)
        {
            if (height == null || height.Length == 0)
                return 0;

            int left = 0, right = height.Length - 1;
            int leftMax = 0, rightMax = 0;
            int totalWater = 0;

            while (left < right)
            {
                if (height[left] < height[right])
                {
                    if (height[left] >= leftMax)
                    {
                        leftMax = height[left];
                    }
                    else
                    {
                        totalWater += leftMax - height[left];
                    }
                    left++;
                }
                else
                {
                    if (height[right] >= rightMax)
                    {
                        rightMax = height[right];
                    }
                    else
                    {
                        totalWater += rightMax - height[right];
                    }
                    right--;
                }
            }

            return totalWater;
        }

        public static void Main(string[] args)
        {
            int[] land1 = { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
            Console.WriteLine($"Input: [{string.Join(",", land1)}]");
            Console.WriteLine($"Output: {CalculateVolume(land1)}");

            int[] land2 = { 4, 2, 0, 3, 2, 5 };
            Console.WriteLine($"Input: [{string.Join(",", land2)}]");
            Console.WriteLine($"Output: {CalculateVolume(land2)}");

            int[] land3 = { 2, 5, 1, 2, 3, 4, 7, 7, 6 };
            Console.WriteLine($"Input: [{string.Join(",", land3)}]");
            Console.WriteLine($"Output: {CalculateVolume(land3)}");
        }
    }
}
