namespace Tasks
#pragma warning disable CS8602 

{
    class Task1
    {
        static bool inputParseToDouble(out double a, out double b)
        {
            return Double.TryParse(System.Console.ReadLine().Replace(',', '.'), out a) &
            Double.TryParse(System.Console.ReadLine().Replace(',', '.'), out b);
        }

        static double average(double a, double b)
        {
            return (a + b) / 2;
        }

        public Task1()
        {
            System.Double a = 0;
            System.Double b = 0;
            bool iscorrect = inputParseToDouble(out a, out b);
            if (!iscorrect) System.Console.WriteLine("Replace empty input to zero");
            Enumerable.Average(new double[] { a, b });
            System.Console.WriteLine($"Average: {average(a, b)}");
        }
    }
}
