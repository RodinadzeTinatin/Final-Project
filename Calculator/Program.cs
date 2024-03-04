namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the console calculator app! ^^ ");
            string next_calculation;

            do
            {

                double num1 = GetUserInput("Enter your first number: ");
                double num2 = GetUserInput("Enter your second number: ");

                string oper = GetOperator();
                

                PerformOperation(num1, num2, oper);


                do
                {
                    Console.WriteLine("\nDo you want to continue using calculator? (y for yes, n for no)");
                    next_calculation = Console.ReadLine();
                } 
                while (next_calculation != "y" && next_calculation != "n");
            }
            while (next_calculation == "y");
        }


        static double GetUserInput(string message)
        {
            double num;
            string strNum;

            Console.WriteLine(message);
            strNum = Console.ReadLine();

            while(!double.TryParse(strNum, out num))
            {
                Console.WriteLine("Not valid number. Please, enter valid number:");
                strNum = Console.ReadLine();
            }

            return num;
        }

        static string GetOperator()
        {
            string oper;

            while (true)
            {
                Console.WriteLine("Enter operation you want to perform (+ for add, - for subtract, / for divide, * for multiply )");
                oper = Console.ReadLine();
                if (oper != "+" && oper != "-" && oper != "/" && oper != "*")
                {
                    Console.WriteLine("Wrong operator. Please try again.");
                    continue;
                }
                break;
            }
            return oper;
        }

        static void PerformOperation(double num1, double num2, string oper)
        {
            double result = 0;
            switch (oper)
            {
                case "+":
                    result = Math.Round(num1 + num2, 2);
                    Console.WriteLine($"{num1} + {num2} = {result}");
                    break;
                case "-":
                    result = Math.Round(num1 - num2, 2);
                    Console.WriteLine($"{num1} - {num2} = {result}");
                    break;
                case "/":
                    while (num2 == 0)
                    {
                        Console.WriteLine("You can't divide by zero. Please enter non-zero second number:");
                        num2 = GetUserInput("Enter your second number:");
                    }
                    result = Math.Round(num1 / num2, 2);
                    Console.WriteLine($"{num1} / {num2} = {result}");
                    break;
                case "*":
                    result = Math.Round(num1 * num2, 2);
                    Console.WriteLine($"{num1} * {num2} = {result}");
                    break;
            }
        }
    }
}
