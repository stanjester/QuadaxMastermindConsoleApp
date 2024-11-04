using System;

namespace Quadax.Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            // ------------------------
            // The randomly generated answer should be four (4) digits in length, with each digit ranging from 1 to 6.  

            Random random = new Random();
            List<int> secretAnswer = new List<int>();
            for (int i = 0; i < 4; i++)
                secretAnswer.Add(random.Next(1, 7));

            // Test using a fixed answer - 1234
            secretAnswer[0] = 1;
            secretAnswer[1] = 2;
            secretAnswer[2] = 3;
            secretAnswer[3] = 4;

            Console.WriteLine("Mastermind");
            Console.WriteLine("Guess the 4 digit secret code, with each digit ranging from 1 to 6");
            Console.WriteLine("You have ten (10) attempts to guess the number correctly");

            int attempts = 10;
            bool isGuessedCorrectly = false;

            while (attempts > 0 && !isGuessedCorrectly)
            {
                Console.WriteLine($"Attempts left: {attempts}");
                Console.Write("Enter your guess: ");

                // Player's guess
                string guess = Console.ReadLine();

                // ------------------------
                // Data validation

                List<int> guessList = guess
                    .Select(c => (int)char.GetNumericValue(c))
                    .Where(num => num >= 1 && num <= 6)
                    .ToList();

                if (guessList.Count != 4)
                {
                    Console.WriteLine($"Invalid entry: {guess}. Please enter exactly 4 digits with each digit ranging from 1 to 6");
                    continue;
                }

                // --------------------------------------
                // Check for correct and misplaced digits

                string result = "";
                List<bool> answerChecked = new List<bool> { false, false, false, false };
                List<bool> guessChecked = new List<bool> { false, false, false, false };

                // Check for correct digits in the correct positions 
                for (int i = 0; i < 4; i++)
                {
                    if (guessList[i] == secretAnswer[i])
                    {
                        result += "+";
                        answerChecked[i] = true;
                        guessChecked[i] = true;
                    }
                }

                // Check for correct digits in the wrong positions
                for (int i = 0; i < 4; i++)
                {
                    if (!guessChecked[i])
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (!answerChecked[j] && guessList[i] == secretAnswer[j])
                            {
                                result += "-";
                                answerChecked[j] = true;
                                break;
                            }
                        }
                    }
                }

                // Output the result for this attempt
                Console.WriteLine("Hint: " + result);

                if (result == "++++")
                {
                    isGuessedCorrectly = true;
                    Console.WriteLine("Congratulations! You've guessed the code correctly.");
                }
                else
                {
                    attempts--;
                    if (attempts > 0)
                    {
                        Console.WriteLine($"{attempts} attempts remaining.\n");
                    }
                }
            }
        }
    }
}