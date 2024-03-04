using System.Runtime.ExceptionServices;

namespace NumberGuesser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the number guessing application! ^^");

            string level;
            bool validLevel = false;
            do
            {
                Console.WriteLine("Select the difficulty level (e - Easy, m - Medium, h - Hard):");
                level = Console.ReadLine().ToLower();
                switch (level)
                {
                    case "e":
                    case "m":
                    case "h":
                        validLevel = true;
                        break;
                    default:
                        Console.WriteLine("Non-valid level. Try again, please.");
                        break;
                }
            }
            while (validLevel == false);



            Random rnd = new Random();
            int randNumber=0;
            switch (level)
            {
                case "e":
                    randNumber = rnd.Next(1,26);
                    break;
                case "m":
                    randNumber = rnd.Next(1,51);
                    break;
                case "h":
                    randNumber = rnd.Next(1,101);
                    break;
            }

            Console.WriteLine("\nGreat, let's start guessing! Remember, you have only 10 attemps.");
            Console.WriteLine("Enter your guess:");
            string strGuess = Console.ReadLine();

            int guess;
            while (!int.TryParse(strGuess, out guess))
            {
                Console.WriteLine("Not a valid number. Please enter a valid integer:");
                strGuess = Console.ReadLine();
            }


            int attempt = 1;

            while (attempt < 10)
            {
                if (guess < randNumber)
                {
                    Console.WriteLine("Your guess is less than the number");
                    Console.WriteLine($"Attempts left: {10-attempt}");
                }
                else if (guess > randNumber)
                {
                    Console.WriteLine("Your guess is more than the number");
                    Console.WriteLine($"Attempts left: {10-attempt}");
                }
                else
                {
                    Console.WriteLine("You've guessed it! Good job!");
                    break;
                }
                attempt++;
                Console.WriteLine("\nEnter your guess:");
                strGuess = Console.ReadLine();

                while (!int.TryParse(strGuess, out guess))
                {
                    Console.WriteLine("Not a valid number. Please enter a valid integer:");
                    strGuess = Console.ReadLine();
                }
            }
            if (attempt == 10)
            {
            Console.WriteLine("Sorry, you've used all your attempts. The correct number was: " + randNumber);
            }

        }
    }
}
