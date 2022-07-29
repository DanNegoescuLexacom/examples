namespace copilot
{
    public static class Math
    {
        // method to calculate the average of a list of numbers
        public static double Average(List<double> numbers)
        {
            double sum = 0;
            foreach (double number in numbers)
            {
                sum += number;
            }
            return sum / numbers.Count;
        }

        // method to add two integers
        public static int Add(int a, int b)
        {
            return a + b;
        }

        // method to subtract two integers
        public static int Subtract(int a, int b)
        {
            return a - b;
        }
    }
}