using System;
using System.Globalization;
using HtnCalc.Core;
using HtnCalc.Core.ExpressionTree;
using HtnCalc.Core.Parsing;
using HtnCalc.Core.Permutation;

namespace HtnCalc.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to HtnCalc!");
            Console.WriteLine("Input an expression for calculation. Input 'q' or 'quit' for termination.");

            ICalculator calculator = GetCalculator();

            while (true)
            {
                Console.Write(">> ");
                string? inputExpression = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(inputExpression))
                    continue;

                var lowerExpression = inputExpression.ToLower(CultureInfo.InvariantCulture);

                if (lowerExpression == "q" || lowerExpression == "quit")
                {
                    Console.Write("Good bye!");
                    break;
                }

                try
                {
                    var calcValue = calculator.Calculate(inputExpression);
                    Console.WriteLine(calcValue);
                }
                catch (ParsingException e)
                {
                    Console.WriteLine($"Parsing error: {e}");
                }
                catch (CalculationException e)
                {
                    Console.WriteLine($"Calculation error: {e}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Internal error: {e.Message}");
                }
            }
        }

        static IExpressionParser GetExpressionParser() => new ExpressionParser();
        static IPermutationsBuilder GetPriorityTermBuilder() => new PermutationsBuilder();
        static IExpressionTreeBuilder GetTermVisitor() => new ExpressionTreeBuilder();

        static ICalculator GetCalculator() =>
            new Calculator(GetExpressionParser(), GetPriorityTermBuilder(), GetTermVisitor());
    }
}