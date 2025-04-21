#pragma warning disable CS8602 
namespace Tasks
{
    class Task3
    {
        static bool inputParseToDouble(out double a)
        {
            return Double.TryParse(System.Console.ReadLine().Replace(',', '.'), out a);
        }
        static double convert_currency(double a, double k)
        {
            return a * k;
        }
        public Task3()
        {
            System.Double a = 0;
            bool iscorrect = inputParseToDouble(out a);
            if (!iscorrect) System.Console.WriteLine("Replace empty input to zero");
            double coefficient = 3.2;
            System.Console.WriteLine($"Converted: {convert_currency(a, coefficient)}");
        }
    }
}
