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

                Console.WriteLine("\nEnter your first number:");
                string strNum1 = Console.ReadLine();
                
                double num1;
                while (double.TryParse(strNum1, out num1) == false)
                {
                    Console.WriteLine("Not valid number. Please, enter valid number:");
                    strNum1 = Console.ReadLine() ;
                }


                Console.WriteLine("Enter second number:");
                string strNum2= Console.ReadLine();

                double num2;
                while (double.TryParse(strNum2, out num2) == false)
                {
                    Console.WriteLine("Not valid number. Please, enter valid number:");
                    strNum2 = Console.ReadLine();
                }



                string oper;
                bool validOperator = false;
                do
                {
                    Console.WriteLine("Enter operation you want to perform (+ for add, - for subtract, / for divide, * for multiply )");
                    oper = Console.ReadLine();

                    switch (oper)
                    {
                        case "+":
                        case "-":
                        case "/":
                        case "*":
                            validOperator = true;
                            break;
                        default:
                            Console.WriteLine("Wrong operator. Please try again.");
                            break;
                    }
                } while (validOperator == false);


                //გადავიტანო თუ არა მოცემული ნაწილი ცალკე ფუნქციაში / კლასში
                double result = 0;

                switch (oper)
                {
                    case "+":
                        result = Math.Round(num1 + num2,2);
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
                            strNum2 = Console.ReadLine();

                            while (double.TryParse(strNum2, out num2) == false)
                            {
                                Console.WriteLine("Not valid number. Please, enter valid number:");
                                strNum2 = Console.ReadLine();
                            }
                        }
                        result = Math.Round(num1 / num2, 2);
                        Console.WriteLine($"{num1} / {num2} = {result}");
                        break;
                    case "*":
                        result = Math.Round(num1 * num2, 2);
                        Console.WriteLine($"{num1} * {num2} = {result}");
                        break;
                }

                Console.WriteLine("\nDo you want to continue using calculator?(y for yes, n for no)");
                next_calculation = Console.ReadLine();
            }
            while (next_calculation == "y");
        }

    }
}
