#pragma warning disable CS8600
#pragma warning disable CS8604 
namespace Tasks
{

    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Enter task number: ");
            System.Console.WriteLine("1 - Avarge of number\n" +
                                     "2 - Arithmetic\n" +
                                     "3 - Currency conversion\n" +
                                     "4 - Simple Calculator\n" +
                                     "5 - Draw a triangle\n" + 
                                     "6 - Draw a cube\n" +
                                     "7 - Types information\n" +
                                     "8 - Array diapason" +
                                     "9 - Async await");
            // object result = TasksFactory.GetTask(byte.TryParse(Console.ReadLine(), out byte value) ? value : (byte)0);
            object result = TasksFactory.GetTask(9);
            if (result == null) System.Console.WriteLine("Is Not Lesson File");
        }
    }


    class TasksFactory
    {
        public static object? GetTask(byte task_number)
        {
            return task_number switch
            {
                1 => new Task1(), // Avarge of number
                2 => new Task2(), // Arithmetic
                3 => new Task3(), // Currency conversion
                4 => new Task4(), // Simple calculator
                5 => new Task5(), // Draw a triangle
                6 => new Task6(), // Draw a cube
                7 => new Task7(), // Types information
                8 => new Task8(), // Array diapason
                9 => new Task9(), // Async await
                _ => null
            };
        }
    }
}
