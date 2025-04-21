using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks {
    class Task7 {
        string msg = "Type {0,8} | .NET {1, 8} |\tSize = {2} \t| Min = {3,30} \t| Max = {4} \t|";
        public Task7() {
            Console.Title = "Type info";
            Console.WriteLine("TYPES INFO:");
            Console.WriteLine("\n\t\t\t\t\tNumeric integer types:");
            Console.WriteLine(msg, "byte", typeof(byte), sizeof(byte), byte.MinValue, byte.MaxValue);
            Console.WriteLine(msg, "sbyte", typeof(sbyte), sizeof(sbyte), sbyte.MinValue, sbyte.MaxValue);
            Console.WriteLine(msg, "short", typeof(short), sizeof(short), short.MinValue, short.MaxValue);
            Console.WriteLine(msg, "ushort", typeof(ushort), sizeof(ushort), ushort.MinValue, ushort.MaxValue);
            Console.WriteLine(msg, "int", typeof(int), sizeof(int), int.MinValue, int.MaxValue);
            Console.WriteLine(msg, "uint", typeof(uint), sizeof(uint), uint.MinValue, uint.MaxValue);
            Console.WriteLine(msg, "long", typeof(long), sizeof(long), long.MinValue, long.MaxValue);
            Console.WriteLine(msg, "ulong", typeof(ulong), sizeof(ulong), ulong.MinValue, ulong.MaxValue);
            Console.WriteLine("\n\t\t\t\t\tNumeric floating-point types:");
            Console.WriteLine(msg, "float", typeof(float), sizeof(float), float.MinValue, float.MaxValue);
            Console.WriteLine(msg, "double", typeof(double), sizeof(double), double.MinValue, double.MaxValue);
            Console.WriteLine(msg, "decimal", typeof(decimal), sizeof(decimal), decimal.MinValue, decimal.MaxValue);
            Console.WriteLine("\n\t\t\t\t\tCharacter types:");
            Console.WriteLine(msg, "char", typeof(char), sizeof(char), char.MinValue, char.MaxValue);
            Console.WriteLine(msg, "string", typeof(string), 0, "null", "null");
            Console.WriteLine("\n\t\t\t\t\tBoolean types:");
            Console.WriteLine(msg, "bool", typeof(bool), sizeof(bool), bool.FalseString, bool.TrueString);
            Console.WriteLine("\n\t\t\t\t\tOther types:");
            Console.WriteLine(msg, "object", typeof(object), 0, "null", "null");
            Console.WriteLine(msg, "dynamic", "null", 0, "null", "null");
            Console.WriteLine(msg, "void", typeof(void), 0, "null", "null");


        }
    }
}
