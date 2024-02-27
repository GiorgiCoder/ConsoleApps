using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace StepProjects
{
    public class Translator
    {
        public void Translate()
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\StepProjects\\StepProjects\\files\\dictionary.txt";
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            Again: // for goto, if user wants to translate again in the end
            
            Console.WriteLine("Pick an option:");
            Console.WriteLine("1. From English to Georgian");
            Console.WriteLine("2. From Georgian to English");

            int option = 0;
            while (true) // validation for option
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
                while ((line = reader.ReadLine()) != null) // parsing line and adding it into dictionary
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

            string language = option == 1 ? "English" : "Georgian"; // only for printing
            Console.WriteLine($"Enter a {language} word you want to translate:");
            string word = Console.ReadLine();


            if (dictionary.ContainsKey(word)) // if contains, print the translation
            {
                Console.WriteLine($"Translation for '{word}' is '{dictionary[word]}'");
            }
            else // otherwise, suggest to add the word
            {
                Console.WriteLine("The word you are looking for was not found. Do you want to add it to the dictionary? Y / N");
                string optionToAdd = "Y";
                while (true) // validation for input
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
                if (optionToAdd == "Y") // if user chooses to add, we need to use writer
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
            while (true) // validation for input
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
            if (optionToAgain == "Y") // if yes, go to beginning and do again
            {
                goto Again;
            }
            else return;
        }
    }
}
