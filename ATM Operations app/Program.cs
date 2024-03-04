using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Collections.Generic;


namespace ATM_Operations_app
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            Logger logger = new Logger(@"../../../logHistory.json");

            Customers loggedInCustomer = null;
            CustomerDataManager.LoadCustomerData();
            logger.LoadLogData(@"../../../logHistory.json");

            Console.WriteLine("Welcome to the ATM Operations app ^^");

            while (true)
            {
                if(loggedInCustomer == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("\t1. Registration");
                    Console.WriteLine("\t2. LogIn");
                    Console.WriteLine("\t3. Exit");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\t1. Check your balance");
                    Console.WriteLine("\t2. Add to balance");
                    Console.WriteLine("\t3. Withdraw from balance");
                    Console.WriteLine("\t4. Log out");
                }

                Console.Write("Select an option: ");
                string option = Console.ReadLine();
                Console.WriteLine();


                if (loggedInCustomer == null)
                {
                    switch (option)
                    {
                        case "1":
                            CustomerDataManager.RegisterNewCustomer();
                            break;
                        case "2":
                            loggedInCustomer = CustomersOperations.LogIn();
                            break;
                        case "3":
                            Console.WriteLine("You exited the app.");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }

                }
                else
                {
                    switch(option)
                    {
                        case "1":
                            CustomersOperations.CheckingBalance(loggedInCustomer);
                            break;
                        case "2":
                            CustomersOperations.AddToBalance(loggedInCustomer);
                            break;
                        case "3":
                            CustomersOperations.WithdrawFromBalance(loggedInCustomer);
                            break;
                        case "4":
                            loggedInCustomer = null;
                            Console.WriteLine("You have logged out!");
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }

            }


        }


    }
}
