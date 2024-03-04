using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ATM_Operations_app
{
    public class CustomersOperations
    {

        public static Customers LogIn()
        {

            while (true)
            {
                Console.Write("Enter your Personal ID: ");
                string persID = Console.ReadLine();

                Customers loggedInCustomer = CustomerDataManager.customers.FirstOrDefault(c => c.PersonalID == persID);
                if (loggedInCustomer == null)
                {
                    Console.WriteLine("Invalid Personal ID. Try again or register if you're a new customer.");
                    continue;
                }

                Console.Write("Enter your password: ");
                string password= Console.ReadLine();

                if (loggedInCustomer.Password != password)
                {
                    Console.WriteLine("Invalid password. Please try again.");
                    continue;
                }

                Console.WriteLine("\nYou have successfully logined!");

                return loggedInCustomer;

            }


        }

        public static void CheckingBalance(Customers loggedInCustomer) 
        {
            Logger logger = new Logger(@"../../../logHistory.json");
            logger.Log($"მომხმარებელმა სახელად {loggedInCustomer.Name} {loggedInCustomer.Surname} - შეამოწმა ბალანსი : {DateTime.Now} - ში.");
            
            Console.WriteLine($"Your current balance is: {loggedInCustomer.Balance}");
        }

        public static void WithdrawFromBalance(Customers loggedInCustomer)
        {
            Logger logger = new Logger(@"../../../logHistory.json");

            double amountToWidthraw;

            while(true)
            {
                Console.WriteLine("Enter the amount to withdraw from your balance:");
                if (!double.TryParse(Console.ReadLine(), out amountToWidthraw))
                {
                    Console.WriteLine("Not valid format. Please, enter valid number");
                    continue;
                }
                else if (amountToWidthraw < 0)
                {
                    Console.WriteLine("Please enter valid positive number.");
                    continue;
                }
                else if (loggedInCustomer.Balance < amountToWidthraw)
                {
                    Console.WriteLine($"Insufficient funds. Please, enter amount less or equal to your balance. Your balance is:{loggedInCustomer.Balance}");
                    continue;
                }
                break;
            }

            loggedInCustomer.Balance -= amountToWidthraw;
            CustomerDataManager.SaveCustomerData();

            Console.WriteLine($"You have succesfully withdrawed {amountToWidthraw}GEL from your balance.");
            logger.Log($"მომხმარებელმა სახელად {loggedInCustomer.Name} {loggedInCustomer.Surname} - გაანაღდა {amountToWidthraw} ლარი : {DateTime.Now} - ში. მისი მოქმედი ბალანსი შეადგენს {loggedInCustomer.Balance}");

        }

        public static void AddToBalance(Customers loggedInCustomer)
        {
            Logger logger = new Logger(@"../../../logHistory.json");
            
            double amountToAdd;

            while (true)
            {
                Console.Write("Enter the amount to add to your balance: ");
                if (!double.TryParse(Console.ReadLine(), out amountToAdd))
                {
                    Console.WriteLine("Not valid format. Please, enter valid number");
                    continue;
                }
                else if(amountToAdd < 0)
                {
                    Console.WriteLine("Please enter valid positive number.");
                    continue;
                }
                break;
            }

            loggedInCustomer.Balance += amountToAdd;
            CustomerDataManager.SaveCustomerData();
            Console.WriteLine($"{amountToAdd}GEL has been added to your balance. ");

            logger.Log($"მომხმარებელმა სახელად {loggedInCustomer.Name} {loggedInCustomer.Surname} - შეავსო ბალანსი {amountToAdd} ლარით : {DateTime.Now} - ში. მისი მოქმედი ბალანსი შეადგენს {loggedInCustomer.Balance}");
        }

    }
}
