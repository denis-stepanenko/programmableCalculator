using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace programmableCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string input = "(5*(8/(2+2)*(5+5)))";

            var matches = Regex.Matches(input, @"\([^()]*\)");

            foreach(Match match in matches)
            {
                Console.WriteLine($"{match.Value.Substring(1, match.Value.Length-2)} {match.Index}");
            }*/
            /*
            string input = "(4*(2+2)*(8/(2+2))/(9+1))";

            string pattern = @"\([^()]*\)";

            while (!Regex.IsMatch(input, @"^((\d*(\.|,)\d*)|\d*)$"))
            {
                var evaluator = new MatchEvaluator((x) =>
                {
                    Console.WriteLine(x.Value);
                    return CalculateSubExpression(x.Value.Substring(1, x.Value.Length - 2)).ToString();
                });
                input = Regex.Replace(input, pattern, evaluator);
            }

            */
            //Console.WriteLine(input);

            Console.WriteLine(CalculateSubExpression("4*4*2/10"));
            
            Console.ReadLine();
        }



        static double CalculateSubExpression(string input)
        {
            string multiplyDividePattern = @"((\d*(\.|,)\d*)|\d+)[*/]((\d*(\.|,)\d*)|\d+)";
            var multiplyDivideEvaluator = new MatchEvaluator((x) => 
            {
                string expression = x.Value;
                expression = expression.Replace('.', ',');
                string result = "";

                Console.WriteLine(x.Value);

                if (expression.IndexOf('*') > -1)
                {
                    int separatorIndex = expression.IndexOf('*');
                    double a = Convert.ToDouble(expression.Substring(0, separatorIndex));
                    double b = Convert.ToDouble(expression.Substring(separatorIndex + 1, expression.Length - separatorIndex - 1));
                    result = (a * b).ToString();
                }

                if (expression.IndexOf('/') > -1)
                {
                    int separatorIndex = expression.IndexOf('/');
                    double a = Convert.ToDouble(expression.Substring(0, separatorIndex));
                    double b = Convert.ToDouble(expression.Substring(separatorIndex + 1, expression.Length - separatorIndex - 1));
                    result = (a / b).ToString();
                }
                
                return result;
            });
            input = Regex.Replace(input, multiplyDividePattern, multiplyDivideEvaluator);

            string plusMinusPattern = @"((\d*(\.|,)\d*)|\d*)[+-]((\d*(\.|,)\d*)|\d*)";
            var plusMinusEvaluator = new MatchEvaluator((x) =>
            {
                string expression = x.Value;
                expression = expression.Replace('.', ',');
                string result = "";

                if (expression.IndexOf('+') > -1)
                {
                    int separatorIndex = expression.IndexOf('+');
                    double a = Convert.ToDouble(expression.Substring(0, separatorIndex));
                    double b = Convert.ToDouble(expression.Substring(separatorIndex + 1, expression.Length - separatorIndex - 1));
                    result = (a + b).ToString();
                }

                if (expression.IndexOf('-') > -1)
                {
                    int separatorIndex = expression.IndexOf('-');
                    double a = Convert.ToDouble(expression.Substring(0, separatorIndex));
                    double b = Convert.ToDouble(expression.Substring(separatorIndex + 1, expression.Length - separatorIndex - 1));
                    result = (a - b).ToString();
                }

                return result;
            });
            input = Regex.Replace(input, plusMinusPattern, plusMinusEvaluator);

            return Convert.ToDouble(input);
        }
    }
}
