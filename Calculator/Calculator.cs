using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Calculator
    {
        public static float Calculate(float num1, float num2, string oper)
        {
            float result = 0;

            switch (oper)
                {
                    case "+":
                        result = num1 + num2;
                        Console.WriteLine($"{num1} + {num2} = {result}");
                        break;
                    case "-":
                        result = num1 - num2;
                        Console.WriteLine($"{num1} - {num2} = {result}");
                        break;
                    case "/":
                        while (num2 == 0)
                        {
                            Console.WriteLine("You can't divide by zero. Please enter non-zero divisor:");
                            num2 = float.Parse(Console.ReadLine());
                        }
                        result = num1 / num2;
                        Console.WriteLine($"{num1} / {num2} = {result}");
                        break;
                    case "*":
                        result = num1 * num2;
                        Console.WriteLine($"{num1} * {num2} = {result}");
                        break;
                }

            return result;
        }


    }
}
