using System;
using System.Text.RegularExpressions;

namespace programmableCalculator
{
    static class Calculator
    {
        enum OperationsGroup
        {
            MultiplyAndDivide,
            PlusAndMinus
        }

        public static double Calculate(string input)
        {
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

            return Convert.ToDouble(input);
        }

        static string CalculateSubExpression(string input)
        {
            input = CalculateOperations(input, OperationsGroup.MultiplyAndDivide);
            input = CalculateOperations(input, OperationsGroup.PlusAndMinus);

            return input;
        }

        static string CalculateOperations(string input, OperationsGroup group)
        {
            string pattern = "";
            if (group == OperationsGroup.MultiplyAndDivide)
            {
                pattern = @"((\d+[.,]\d+)|\d+)[*/]((\d+[.,]\d+)|\d+)";
            }

            if (group == OperationsGroup.PlusAndMinus)
            {
                pattern = @"((\d+[.,]\d+)|\d+)[+-]((\d+[.,]\d+)|\d+)";
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
