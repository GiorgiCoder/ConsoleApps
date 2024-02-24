using System;
using System.Collections.Generic;
using System.IO;

namespace StepProjects
{
    public class Translator
    {
        public void Translate()
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\StepProjects\\StepProjects\\files\\dictionary.txt";
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            Again:
            

            Console.WriteLine("Pick an option:");
            Console.WriteLine("1. From English to Georgian");
            Console.WriteLine("2. From Georgian to English");

            int option = 0;
            while (true)
            {
                try
                {
                    option = int.Parse(Console.ReadLine());
                    if (option == 1 || option == 2)
                    {
                        break;
                    }
                    else int.Parse("GoToError");
                }
                catch (Exception) { }
                {
                    Console.WriteLine("Enter a valid option");
                }
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine(); // first line is languages
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split(',');
                    if (option == 1)
                    {
                        if (!dictionary.ContainsKey(words[1]))
                            dictionary.Add(words[1], words[0]);
                    }
                    else
                    {
                        if (!dictionary.ContainsKey(words[0]))
                            dictionary.Add(words[0], words[1]);
                    }
                }
            }

            string language = option == 1 ? "English" : "Georgian";
            Console.WriteLine($"Enter a {language} word you want to translate:");
            string word = Console.ReadLine();


            if (dictionary.ContainsKey(word))
            {
                Console.WriteLine($"Translation for '{word}' is '{dictionary[word]}'");
            }
            else
            {
                Console.WriteLine("The word you are looking for was not found. Do you want to add it to the dictionary? Y / N");
                string optionToAdd = "Y";
                while (true)
                {
                    optionToAdd = Console.ReadLine();
                    if (optionToAdd == "Y" || optionToAdd == "N")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{optionToAdd} is not an option. Reply with Y or N");
                        continue;
                    }
                }
                if (optionToAdd == "Y")
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true)) // true means append
                    {
                        Console.WriteLine($"Enter translation for {word}");
                        string translatedWord = Console.ReadLine();

                        if(language == "Georgian")
                        {
                            writer.WriteLine($"{word},{translatedWord}");
                            dictionary.Add(word, translatedWord);
                        }
                        else
                        {
                            writer.WriteLine($"{translatedWord},{word}");
                            dictionary.Add(translatedWord, word);
                        }
                        Console.WriteLine("The word was successfully added!");
                    }
                }
            }

            Console.WriteLine("Do you want to translate again? Y / N");
            string optionToAgain = "Y";
            while (true)
            {
                optionToAgain = Console.ReadLine();
                if (optionToAgain == "Y" || optionToAgain == "N")
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"{optionToAgain} is not an option. Reply with Y or N");
                    continue;
                }
            }
            if (optionToAgain == "Y")
            {
                goto Again;
            }
            else return;
        }
    }
}
