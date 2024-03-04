using System;
using System.Runtime.ExceptionServices;

namespace NumberGuesser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the number guessing application! ^^");

            string level = SetLevel();


            int randNumber = GenerateRandomNum(level);

            Console.WriteLine("\nGreat, let's start guessing! Remember, you have only 10 attempts.");

            GuessNum(randNumber, level);


        }


        static string SetLevel()
        {
            string level;

            while (true)
            {
                Console.WriteLine("Select the difficulty level (e - Easy, m - Medium, h - Hard):");
                level = Console.ReadLine().ToLower();
                switch (level)
                {
                    case "e":
                    case "m":
                    case "h":
                        return level;
                    default:
                        Console.WriteLine("Non-valid level. Try again, please.");
                        break;
                }
            }
        }

        static int GenerateRandomNum (string level)
        {
            Random rnd = new Random();
            switch (level)
            {
                case "e":
                    return  rnd.Next(1, 26);
                case "m":
                    return  rnd.Next(1, 51);
                case "h":
                    return  rnd.Next(1, 101);
                default:
                    return 0;
            }
        }

        static int GetGuessFromUser(string level)
        {
            int guess;
            int maxRange;

            switch (level)
            {
                case "e":
                    maxRange = 25;
                    break;
                case "m":
                    maxRange = 50;
                    break;
                case "h":
                    maxRange = 100;
                    break;
                default:
                    maxRange = 25;
                    break;
            }


            while (true)
            {
                Console.WriteLine($"\nEnter your guess (0 to {maxRange}):");
                string strGuess = Console.ReadLine();

                if (int.TryParse(strGuess, out guess))
                {
                    if (guess >= 0 && guess <= maxRange)
                    {
                        return guess;
                    }
                    else
                    {
                        Console.WriteLine($"Please enter a number between 0 and {maxRange}.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid number. Please enter a valid integer:");
                    continue;
                }
            }
        }

        static void GuessNum(int randNum, string level)
        {
            int attempt = 1;
            int guess;

            while (attempt <= 10)
            {
                guess = GetGuessFromUser(level);
                if (guess < randNum)
                {
                    Console.WriteLine("Your guess is less than the number");
                    Console.WriteLine($"Attempts left: {10 - attempt}");
                }
                else if (guess > randNum)
                {
                    Console.WriteLine("Your guess is more than the number");
                    Console.WriteLine($"Attempts left: {10 - attempt}");
                }
                else
                {
                    Console.WriteLine("You've guessed it! Good job!");
                    break;
                }
                attempt++;
            }
            if (attempt > 10)
            {
                Console.WriteLine("\n\tSorry, you've used all your attempts. The correct number was: " + randNum);
            }
        }
    }
}
