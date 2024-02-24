using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProjects
{
    public class Calculator
    {
        public void Calculate()
        {
            double m = 0, n = 0;
            double result = 0;

            while (true)
            {
                Console.WriteLine("Enter first number: ");
                try
                {
                    m = double.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter a valid first number");
                    continue;
                }
            }
            while (true)
            {
                Console.WriteLine("Enter second number: ");
                try
                {
                    Console.WriteLine("Enter a valid second number");
                    n = double.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    continue;
                }
            }

            char[] availableOperations = { '+', '-', '*', '/' };
            char operation = '0';

            while (true)
            {
                Console.WriteLine("Enter operation: ");
                try
                {
                    operation = char.Parse(Console.ReadLine());
                    if (!availableOperations.Contains(operation))
                    {
                        continue;
                    }
                    else break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Enter a valid operation");
                    continue;
                }
            }

            switch (operation)
            {
                case '+':
                    result = m + n;
                    break;
                case '-':
                    result = m - n;
                    break;
                case '*':
                    result = m * n;
                    break;
                case '/':
                    result = m / n;
                    break;
                default:
                    result = -1;
                    break;
            }

            Console.WriteLine($"{m} {operation} {n} = {result}");
        }
    }
}
