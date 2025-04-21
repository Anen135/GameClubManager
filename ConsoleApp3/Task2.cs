#pragma warning disable CS8602 

namespace Tasks
{
    class Task2
    {
        static bool inputParseToDouble(out double a, out double b, out double c)
        {
            return Double.TryParse(System.Console.ReadLine().Replace(',', '.'), out a) &
            Double.TryParse(System.Console.ReadLine().Replace(',', '.'), out b) & Double.TryParse(System.Console.ReadLine().Replace(',', '.'), out c);
        }

        static double sum(params double[] numbers) {
            return numbers.Sum();
        }

        static double multiply(params double[] numbers) {
            return numbers.Aggregate(1.0, (x, y) => x * y);
        }

        public Task2()
        {
            System.Double a = 0;
            System.Double b = 0;
            System.Double c = 0;
            bool iscorrect = inputParseToDouble(out a, out b, out c);
            if (!iscorrect) System.Console.WriteLine("Replace empty input to zero");
            System.Console.WriteLine($"Sum: {sum(a, b, c)}");
            System.Console.WriteLine($"Multiply: {multiply(a, b, c)}");
        }
    }
}
