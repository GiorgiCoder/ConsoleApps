using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProjects
{
    public class Hangman // no special characters and numbers
    {
        private static string[] words = new string[] { "first", "second", "third", "fourth", "fifth" };
        public void PlayHangman()
        {
            Random random = new Random();
            string word = words[random.Next(0, words.Length)];
            char[] wordInChars = word.ToCharArray();
            int triesLeft = 10;
            char[] usersWord = new char[wordInChars.Length]; // for printing out for user
            for (int i = 0; i < usersWord.Length; i++)
            {
                usersWord[i] = '_';
            }

            char[] tempWord = usersWord;
            Console.WriteLine(new string(usersWord));

            while (triesLeft > 0)
            {
                Console.WriteLine("Enter a letter:");
                char c = '0';
                while (true)
                {
                    try
                    {
                        c = char.Parse(Console.ReadLine());
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Enter a valid letter");
                    }
                }

                tempWord = OpenLetters(wordInChars, usersWord, c);

                if (tempWord == usersWord) // I did it by accident, but when user enters letter that he already guessed, tempWord and usersWord are same,
                {                          // but not strictly (not on same memory address, since method returns different instance). It means that this
                    triesLeft--;           // equality is false and user won't lose his try. That's what we want :)
                    if (triesLeft == 0)
                    {
                        Console.WriteLine($"You lost. The word was '{word}'");
                        return;
                    }
                    Console.WriteLine($"Word does not contain {c}. Tries left: {triesLeft}");
                    Console.WriteLine(new string(usersWord));
                }
                else
                {
                    usersWord = tempWord;
                    Console.WriteLine(new string(usersWord));
                }

                if (usersWord.SequenceEqual(wordInChars))
                {
                    Console.WriteLine($"Congratulations! You guessed the word '{word}'");
                    return;
                }
            }
            
        }

        private char[] OpenLetters(char[] word, char[] userWord, char letter)
        {
            char[] result = (char[])userWord.Clone();
            if (!word.Contains(letter))
            {
                return userWord;
            }
            else
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == letter)
                    {
                        result[i] = letter;
                    }
                }
            }

            return result;
        }
    }
}
