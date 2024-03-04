using System.Text.RegularExpressions;

namespace HangmanApp
{
    internal class Program
    {

        static List<string> words = new List<string>
        {
            "apple", "banana", "orange", "grape", "kiwi",
            "strawberry", "pineapple", "blueberry", "peach", "watermelon"
        };

        static Regex letterValidationRegex = new Regex("[a-z]");
        static Random rnd = new Random();
        static string wordToGuess = words[rnd.Next(0, words.Count)];

        static List<string> guessedLetters = new List<string>();
        static int rightGuess = 0;
        static int attempts = 0;
        static int lettersToGuess = 0;
        static string guessedCharacter;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Hangman!");
            Console.WriteLine("You have 6 attempts to guess the letters in a word.");

            while (attempts < 6)
            {

                DisplayingWords(wordToGuess, guessedLetters, out lettersToGuess);
                if (lettersToGuess == 0)
                {
                    break;
                }

                guessedCharacter = InputValidation(letterValidationRegex, guessedLetters);

                LetterGuess();


            }

            GameResults();
        }


        static void DisplayingWords(string wordToGuess, List<string> guessedLetters, out int lettersToGuess)
        {
            lettersToGuess = 0;
            foreach (char c in wordToGuess)
            {
                string letter = c.ToString();

                if (guessedLetters.Contains(letter))
                {
                    Console.Write(letter + " ");
                }
                else
                {
                    Console.Write("_ ");
                    lettersToGuess++;
                }
            }

            Console.WriteLine();

        }

        static string InputValidation(Regex letterValidationRegex, List<string> guessedLetters)
        {
            string guessedCharacter;

            while (true)
            {
                Console.WriteLine("\nEnter your letter:");
                guessedCharacter = Console.ReadLine().ToLower();

                if (!letterValidationRegex.IsMatch(guessedCharacter))
                {
                    Console.WriteLine("The input is not valid. Please, enter valid letter.");
                }
                else if (guessedCharacter.Length > 1)
                {
                    Console.WriteLine("Please, enter single letter at a time.");
                }
                else if (guessedLetters.Contains(guessedCharacter))
                {
                    Console.WriteLine("You have already guessed this letter. Please, enter another one");
                }
                else { break; }

            }


            return guessedCharacter;
        }


        static void LetterGuess()
        {
            guessedLetters.Add(guessedCharacter);

            if (!wordToGuess.Contains(guessedCharacter))
            {
                attempts++;
                Console.WriteLine($"The word doesn't contain letter {guessedCharacter}. You have {6 - attempts} attempts left.");
            }
            else
            {
                rightGuess++;
                attempts++;
                Console.WriteLine($"The word contains letter {guessedCharacter}. You have {6 - attempts} attempts left.");
                if (attempts > 5)
                {
                    DisplayingWords(wordToGuess, guessedLetters, out lettersToGuess);
                }
            }
        }


        static void WordGuess(string guessedWord)
        {
            if (guessedWord == wordToGuess)
            {
                Console.WriteLine("\n\t\tCongrats! You won!");
            }
            else
            {
                Console.WriteLine($"Sorry, you lost. The word was: {wordToGuess}. ");
            }
        }

        static void GameResults()
        {
            Console.WriteLine();
            if (rightGuess == 0)
            {
                Console.WriteLine("Sorry, you lost, you haven't guessed any letters.");
            }
            else
            {
                if (lettersToGuess == 0)
                {
                    Console.WriteLine("\n\t\tCongrats! You won!");
                }
                Console.WriteLine("You ran out of attempts to guess the letter. It's time to guess the word!");
                string guessedWord = Console.ReadLine();
                WordGuess(guessedWord);
            }
        }

    }
}
