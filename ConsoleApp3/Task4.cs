using System.Globalization;
#pragma warning disable CS8602
namespace Tasks {
    class Task4 {
        public Task4() {
            Console.Write("Введите выражение: ");
            string input = Console.ReadLine().Replace(" ", "").Replace(',', '.');
            if (string.IsNullOrEmpty(input)) { Console.WriteLine("Пустой ввод"); return; }
            try {
                double result = EvaluateRPN(ConvertToRPN(input));
                Console.WriteLine($"Результат: {result}");
            } catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
        }
        static List<string> ConvertToRPN(string input) {
            Stack<string> operators = new Stack<string>();
            List<string> output = new List<string>();
            for (System.Int32 i = 0; i < input.Length; ++i) {
                if (char.IsDigit(input[i]) || input[i] == '.') { 
                    int s = i;
                    while (i < input.Length && (char.IsDigit(input[i]) || input[i] == '.')) i++;
                    output.Add(input[s..i]); i--;
                } else if (char.IsLetter(input[i])) { 
                    int s = i;
                    while (i < input.Length && char.IsLetter(input[i])) i++;
                    string func = input[s..i]; i--;
                    if (!MathFunctions.ContainsKey(func)) throw new Exception($"Неизвестная функция: {func}");
                    operators.Push(func);
                } else if (IsOperator(input[i])) { 
                    string op = input[i].ToString();
                    if (IsUnaryOperator(op, input[i - 1].ToString())) op = "u" + op;
                    else while (operators.Count > 0 && GetPriority(operators.Peek()) >= GetPriority(op)) output.Add(operators.Pop());
                    operators.Push(op);
                } else if (input[i] == '!') { output.Add("!");
                } else if (input[i] == '(') { operators.Push(input[i].ToString());
                } else if (input[i] == ')') {
                    while (operators.Count > 0 && operators.Peek() != "(") output.Add(operators.Pop());
                    operators.Pop(); 
                    if (MathFunctions.ContainsKey(operators.Peek())) output.Add(operators.Pop()); 
                }
            }
            while (operators.Count > 0) output.Add(operators.Pop());
            return output;
        }

        
        static double EvaluateRPN(List<string> rpn) {
            Stack<double> stack = new Stack<double>();

            foreach (var token in rpn) {
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double number)) stack.Push(number);
                else if (IsBinaryOperator(token) && stack.Count >= 2) stack.Push(ApplyOperator(stack.Pop(), stack.Pop(), token));
                else if (IsUnaryOperator(token) && stack.Count >= 1) stack.Push(ApplyUnaryOperator(stack.Pop(), token));
                else if (MathFunctions.ContainsKey(token) && stack.Count >= 1) stack.Push(MathFunctions[token](stack.Pop()));
                else if (token == "!") stack.Push(Factorial(stack.Pop()));
                else throw new Exception("Некорректное выражение");
            }
            return stack.Pop();
        }

        static double Factorial(double n) {
            double[] Precomputed = [1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 479001600, 6227020800, 87178291200, 1307674368000, 20922789888000, 355687428096000, 6402373705728000, 121645100408832000, 2432902008176640000];
            if (n < 0) throw new ArgumentException("Факториал определён только для неотрицательных чисел.");
            if (n < 20) return Precomputed[(int)n];
            long result = 1;
            for (int i = 2; i <= n; i++) result *= i;
            return result;
        }

        static double ApplyOperator(double b, double a, string op) {
            return op switch {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => b != 0 ? a / b : throw new DivideByZeroException("Деление на ноль"),
                "^" => Math.Pow(a, b),
                _ => throw new Exception("Неизвестный оператор")
            };
        }

        static double ApplyUnaryOperator(double a, string op) {
            return op switch {
                "u-" => -a,
                "u+" => +a,
                "!" => Factorial(a),
                _ => throw new Exception("Неизвестный унарный оператор")
            };
        }

        
        static readonly Dictionary<string, Func<double, double>> MathFunctions = new() {
            { "sin", Math.Sin },
            { "cos", Math.Cos },
            { "tan", Math.Tan },
            { "log", Math.Log10 },
            { "ln", Math.Log },
            { "sqrt", Math.Sqrt },
            { "abs", Math.Abs },
            { "exp", Math.Exp }
        };

        static int GetPriority(string op) {
            return op switch {
                "u+" or "u-" or "!" => 4,
                "+" or "-" => 1,
                "*" or "/" => 2,
                "^" => 3,
                _ => 0
            };
        }

        static bool IsOperator(char c) => "+-*/^".Contains(c);
        static bool IsBinaryOperator(string op) => "+-*/^".Contains(op);
        static bool IsUnaryOperator(string op) => op.StartsWith("u") || op == "!";
        static bool IsUnaryOperator(string op, string last) => (op == "+" || op == "-") && ("+-*/(^".Contains(last));
    }
}
