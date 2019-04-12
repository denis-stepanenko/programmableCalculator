using System;

namespace programmableCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isContinue = false;
            do
            {
                Console.WriteLine(
@"Доступны операции: *, /, +, - 
Доступно использование скобок.
Введите математическое выражение:");
                string expression = Console.ReadLine();

                string result = Calculator.Calculate(expression);
                Console.WriteLine($"Result = {result}");

                Console.Write("Хотите продолжить? (y/n): ");
                string answer = Console.ReadLine();
                isContinue = answer == "y" ? true : false;
            }
            while (isContinue);
        }
    }
}
