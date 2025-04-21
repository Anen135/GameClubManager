using System.Diagnostics;
using System.Text;

namespace Tasks {
    class Task5 {
        public Task5() {
            System.Int32 height = 50; // Высота треугольника
            // System.Int32 iterations = 1000; // Количество итераций
            TriangleWithNewString.draw_hollow(height);
            TriangleWithNewString.draw_inverted_hollow(height);
            TriangleWithNewString.draw_filled(height);
            TriangleWithNewString.draw_inverted_filled(height);
            TriangleWithNewString.draw_left(height);
            TriangleWithNewString.draw_right(height);
            TriangleWithNewString.draw_down_left(height);
            TriangleWithNewString.draw_down_right(height);

        }
         
        delegate void Functions();
        static long MeasureTime(System.Int32 iterations, Functions func) {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (System.Int32 i = 0; i < iterations; ++i) func();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        static void race() {

        }

    }

    class TriangleWithNewString {
        public static void draw_hollow(System.Int32 h) {
            Console.WriteLine(new string(' ', h - 1) + '#');
            for (System.Int32 i = 2; i < h; ++i) Console.WriteLine(new string(' ', h - i) + "#" + new string(' ', 2 * (i - 1) - 1) + "#");
            Console.WriteLine(new string('#', 2 * h - 1));
        }
        public static void draw_inverted_hollow(System.Int32 h) {
            Console.WriteLine(new string('#', 2 * h - 1));
            for (System.Int32 i = h - 1; i > 1; --i) Console.WriteLine(new string(' ', h - i) + "#" + new string(' ', 2 * (i - 1) - 1) + "#");
            Console.WriteLine(new string(' ', h - 1) + '#');
        }
        public static void draw_filled(System.Int32 h) { for (System.Int32 i = 0; i < h; ++i) Console.WriteLine(new string(' ', h - i - 1) + new string('#', 2 * i + 1)); }
        public static void draw_inverted_filled(System.Int32 h) { for (System.Int32 i = h; i > 0; --i) Console.WriteLine(new string(' ', h - i) + new string('#', 2 * i - 1)); }
        public static void draw_left(System.Int32 h) { for (System.Int32 i = 0; i <= h; ++i) Console.WriteLine(new string('#', i)); }
        public static void draw_right(System.Int32 h) { for (System.Int32 i = 1; i <= h; ++i) Console.WriteLine(new string(' ', h-i) + new string('#', i)); }
        public static void draw_down_left(System.Int32 h) { for (System.Int32 i = h; i > 0; --i) Console.WriteLine(new string('#', i)); }
        public static void draw_down_right(System.Int32 h) { for (System.Int32 i = 1; i <= h; ++i) Console.WriteLine(new string(' ', h - i) + new string('#', i)); }
    }
}