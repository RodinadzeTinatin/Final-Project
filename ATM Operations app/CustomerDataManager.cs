using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ATM_Operations_app
{
    public class CustomerDataManager
    {
        public static List<Customers> customers = new List<Customers>();
        private static int lastCustomerId = 0;


        public static void LoadCustomerData()
        {
            string filePath = @"../../../customer.json";

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

        public static void SaveCustomerData()
        {
            string filePath = @"../../../customer.json";

            try
            {
                string json = JsonSerializer.Serialize(CustomerDataManager.customers);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving customer data: {ex.Message}");
            }
        }

        public static void RegisterNewCustomer()
        {
            string name;
            string surname;
            string personalID;
            while (true)
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine();
                if (name == "")
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
                if (surname == "")
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
                else if (!personalID.All(char.IsAsciiDigit))
                {
                    Console.WriteLine("Invalid personal ID. It should contain only numbers.");
                    continue;
                }
                else if (personalID.Length != 11)
                {
                    Console.WriteLine("Invalid personal ID. It should be 11 digits long.");
                    continue;
                }
                else if (customers.Any(c => c.PersonalID == personalID))
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
            string filePath = @"../../../customer.json";

            try
            {
                string json = JsonSerializer.Serialize(customers);
                File.WriteAllText(filePath, json);

                Console.WriteLine("Customer registered successfully!");

                Console.WriteLine("\nNow you will be able to logIn using following data:");
                Console.WriteLine($"Your Personal ID: {personalID}");
                Console.WriteLine($"Your password: {password}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving customer data: {ex.Message}");
            }
        }


        public static string RandomPassword()
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
