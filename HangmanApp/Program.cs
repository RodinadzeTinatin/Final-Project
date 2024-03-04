using System.Text.RegularExpressions;

namespace HangmanApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Hangman!");
            Console.WriteLine("You have 6 attempts to guess the letters in a word.");

            List<string> words = new List<string>
            {
                "apple", "banana", "orange", "grape", "kiwi",
                "strawberry", "pineapple", "blueberry", "peach", "watermelon"
            };

            Regex letterValidationRegex = new Regex("[a-z]");

            Random rnd = new Random();
            string wordToGuess = words[rnd.Next(1, words.Count)];

            int rightGuess = 0;
            int attempts = 0;
            int lettersToGuess = 0;
            var guessedLetters = new List<string>();
            while (attempts < 6)
            {

                #region Displaying Letters
                lettersToGuess = 0;
                foreach (char c in wordToGuess)
                {
                    string letter = c.ToString();

                    if (guessedLetters.Contains(letter))
                    {
                        Console.Write(letter+" ");
                    }
                    else
                    {
                        Console.Write("_ ");
                        lettersToGuess++;
                    }
                }

                if (lettersToGuess == 0)
                {
                    break;
                }
                Console.WriteLine();

                #endregion


                #region InputValidation
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

                #endregion



                #region LetterGuess
                guessedLetters.Add(guessedCharacter);
                
                
                if (!wordToGuess.Contains(guessedCharacter))
                {
                    attempts++;
                    Console.WriteLine($"The word doesn't contain letter {guessedCharacter}. You have {6-attempts} attempts left.");
                    //return true;
                }
                else
                {
                    rightGuess++;
                    attempts++;
                    Console.WriteLine($"The word contains letter {guessedCharacter}. You have {6 - attempts} attempts left.");
                    //return false; 
                }
                #endregion

            }

            #region GameResults
            Console.WriteLine();

            if (rightGuess== 0)
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
                #region WordGuess
                if (guessedWord == wordToGuess)
                {
                    Console.WriteLine("\n\t\tCongrats! You won!");
                }
                else
                {
                    Console.WriteLine($"Sorry, you lost. The word was: {wordToGuess}. ");
                }
                #endregion
            }
            #endregion 
        }


    }
}
