using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Collections.Generic;


namespace ATM_Operations_app
{
    public class Program
    {

        private static int lastCustomerId = 0;
        public static List<Customers> customers = new List<Customers>();
        
        static void Main(string[] args)
        {
            Logger logger = new Logger("D:\\C#, IT Step\\Final Project\\ATM Operations app\\logHistory.json");

            Customers loggedInCustomer = null;
            LoadCustomerData();

            while (true)
            {
                if(loggedInCustomer == null)
                {
                    Console.WriteLine("\t1. Registration");
                    Console.WriteLine("\t2. LogIn");
                    Console.WriteLine("\t3. Exit");
                    Console.WriteLine("\t4. Clear Log Data");
                }
                else
                {
                    Console.WriteLine("\t1. Check your balance");
                    Console.WriteLine("\t2. Add to balance");
                    Console.WriteLine("\t3. Withdraw from balance");
                    Console.WriteLine("\t4. Log out");
                }

                Console.Write("Select an option: ");
                string option = Console.ReadLine();


                if (loggedInCustomer == null)
                {
                    switch (option)
                    {
                        case "1":
                            RegisterNewCustomer();
                            break;
                        case "2":
                            loggedInCustomer = CustomersOperations.LogIn();
                            break;
                        case "3":
                            Console.WriteLine("You exited the app.");
                            return;
                        case "4":
                            logger.ClearLog();
                            break;
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

        static void LoadCustomerData()
        {
            string filePath = "D:\\C#, IT Step\\Final Project\\ATM Operations app\\customer.json";

            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    customers = JsonSerializer.Deserialize<List<Customers>>(json);
                    lastCustomerId = customers.Count > 0 ? customers.Max(c => c.ID) : 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading customer data: {ex.Message}");
                }
            }
            else
            {
                try
                {
                    File.WriteAllText(filePath, "[]");
                    customers = new List<Customers>();
                    lastCustomerId = 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating customer data file: {ex.Message}");
                }
                
            }
        }
        
        static void RegisterNewCustomer()
        {
            string name;
            string surname;
            string personalID;
            while (true)
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine();
                if(name == "")
                {
                    Console.WriteLine("Your name can not be empty.");
                    continue;
                }
                break;
            }

            while (true)
            {
                Console.Write("Enter your surname: ");
                surname = Console.ReadLine();
                if(surname == "")
                {
                    Console.WriteLine("Your surname can not be empty.");
                    continue;
                }
                break;
            }

            while (true)
            {

                Console.Write("Enter your personal ID: ");
                personalID = Console.ReadLine();
                if (personalID == "")
                {
                    Console.WriteLine("Personal ID can not be empty.");
                    continue;
                }
                else if(!personalID.All(char.IsAsciiDigit))
                {
                    Console.WriteLine("Invalid personal ID. It should contain only numbers.");
                    continue;
                }
                else if (personalID.Length != 11)
                {
                    Console.WriteLine("Invalid personal ID. It should be 11 digits long.");
                    continue;
                }
                else if(customers.Any(c => c.PersonalID == personalID))
                {
                    Console.WriteLine("User with this personal ID already exists. Enter a different one of log into your account.");
                    continue;
                }
                break;
            }

            string password = RandomPassword();

            Customers newCustomer = new Customers
            {
                ID = lastCustomerId + 1,
                Name = name,
                Surname = surname,
                PersonalID = personalID,
                Password = password,
                Balance = 0
            };

            customers.Add(newCustomer);
            string filePath = "D:\\C#, IT Step\\Final Project\\ATM Operations app\\customer.json";

            try
            {
                string json = JsonSerializer.Serialize(customers);
                File.WriteAllText(filePath, json);

                Console.WriteLine("Customer registered successfully!"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving customer data: {ex.Message}");
            }
        }


        static string RandomPassword()
        {
            Random rnd = new Random();
            string password;

            do
            {
                password = rnd.Next(1000, 10000).ToString();
            }
            while (customers.Any(c => c.Password == password));

            return password;
        }
    }
}
