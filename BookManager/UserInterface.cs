using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProjects.BookManager
{
    public class UserInterface
    {
        public void Action()
        {
            while (true)
            {
                Console.WriteLine("Choose which service you want to use (enter number):\n1. List all books' details\n2. Get book details\n3. Add a book\n4. Exit");

                int option = 0;
                while (true) // validation for option
                {
                    try
                    {
                        option = int.Parse(Console.ReadLine());
                        if (option == 1 || option == 2 || option == 3 || option == 4)
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

                if (option == 1) // return all books
                {
                    ListAllBooks();
                }
                else if (option == 2) // return 1 book
                {
                    Console.WriteLine("Enter the name of the book you want to view:");
                    string bookTitle = Console.ReadLine();
                    GetBookDetails(bookTitle);
                }
                else if (option == 3) // add book
                {
                    AddBook();
                }
                else return; // option == 4, exit
            }
        }

        private static void ListAllBooks() // just print out all books using our ToString() method
        {
            var books = BookManager.GetAllBooks();
            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
                Console.WriteLine();
            }
        }

        private static void GetBookDetails(string title)
        {
            var book = BookManager.SearchBookByTitle(title);
            if (book == null) // if book doesn't exist, suggest to add it
            {
                Console.WriteLine($"Book '{title}' wasn't found. Do you want to add it? Y / N");
                string optionToAdd = "Y";
                while (true) // validation for option
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
                if (optionToAdd == "Y") // add if Yes
                {
                    AddBook();
                }
                else { return; } // else exit
            }
            else // if exists, return it.
            {
                Console.WriteLine(book.ToString());
            }
        }

        private static void AddBook()
        {
            Console.WriteLine("Enter Title:");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Author:");
            string author = Console.ReadLine();
            Console.WriteLine("Enter release year:");
            int releaseYear;
            while (true) // validation for release year
            {
                try
                {
                    releaseYear = int.Parse(Console.ReadLine());
                    if (releaseYear > DateTime.Now.Year || releaseYear < 0)
                    {
                        int.Parse("GoToError");
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Enter a valid year");
                }
            }

            var added = BookManager.AddBook(title, author, releaseYear); // add book
            if (added) { Console.WriteLine($"The book {title} was successfully added!"); } // check if it was added
            else { Console.WriteLine("The book already exists!"); } // if false, it means book already existed
        }
    }
}
