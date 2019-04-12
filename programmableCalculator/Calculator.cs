using System;
using System.Text.RegularExpressions;

namespace programmableCalculator
{
    static class Calculator
    {
        // группы операций для соблюдения приоритета
        enum OperationsGroup
        {
            MultiplyAndDivide,
            PlusAndMinus
        }

        // разбивает выражение на подвыражения 
        // и вычиляет подвыражения
        public static string Calculate(string input)
        {
            input = input.Replace('.', ',');

            var expression = new Regex(@"(\(|^)[^()]*(\)|$)");

            while (expression.IsMatch(input))
            {
                if (Regex.IsMatch(input, @"^[^*/+-]*$"))
                {
                    break;
                }

                var match = expression.Match(input);
                string subExpression = match.Value;

                if (match.Value[0] == '(' && match.Value[match.Value.Length - 1] == ')')
                {
                    subExpression = match.Value.Substring(1, match.Value.Length - 2);
                }

                input = expression.Replace(input, CalculateSubExpression(subExpression));
            }

            return input;
        }

        // вычисляет подвыражение
        static string CalculateSubExpression(string input)
        {
            input = CalculateOperations(input, OperationsGroup.MultiplyAndDivide);
            input = CalculateOperations(input, OperationsGroup.PlusAndMinus);

            return input;
        }

        // вычисляет группу операций в подвыражении
        static string CalculateOperations(string input, OperationsGroup group)
        {
            string pattern = "";
            if (group == OperationsGroup.MultiplyAndDivide)
            {
                pattern = @"((\d+\,\d+)|\d+)\s*[*/]\s*((\d+\,\d+)|\d+)";
            }

            if (group == OperationsGroup.PlusAndMinus)
            {
                pattern = @"((\d+\,\d+)|\d+)\s*[+-]\s*((\d+\,\d+)|\d+)";
            }

            var expression = new Regex(pattern);

            while (expression.IsMatch(input))
            {
                var match = expression.Match(input);

                string result = CalculateOperation(match.Value);
                input = expression.Replace(input, result, 1);
            }

            return input;
        }

        // возвращает результат вычисления триплета [число] [операция] [число]
        static string CalculateOperation(string input)
        {
            int separatorIndex;
            string result = "";

            separatorIndex = input.IndexOf('*');

            if (separatorIndex > -1)
            {
                double a = Convert.ToDouble(input.Substring(0, separatorIndex));
                double b = Convert.ToDouble(input.Substring(separatorIndex + 1, input.Length - separatorIndex - 1));
                result = (a * b).ToString();
            }

            separatorIndex = input.IndexOf('/');

            if (separatorIndex > -1)
            {
                double a = Convert.ToDouble(input.Substring(0, separatorIndex));
                double b = Convert.ToDouble(input.Substring(separatorIndex + 1, input.Length - separatorIndex - 1));
                result = (a / b).ToString();
            }

            separatorIndex = input.IndexOf('+');

            if (separatorIndex > -1)
            {
                double a = Convert.ToDouble(input.Substring(0, separatorIndex));
                double b = Convert.ToDouble(input.Substring(separatorIndex + 1, input.Length - separatorIndex - 1));
                result = (a + b).ToString();
            }

            separatorIndex = input.IndexOf('-');

            if (separatorIndex > -1)
            {
                double a = Convert.ToDouble(input.Substring(0, separatorIndex));
                double b = Convert.ToDouble(input.Substring(separatorIndex + 1, input.Length - separatorIndex - 1));
                result = (a - b).ToString();
            }

            return result;
        }
    }
}
