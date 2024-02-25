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
                while (true)
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

                if (option == 1)
                {
                    ListAllBooks();
                }
                else if (option == 2)
                {
                    Console.WriteLine("Enter the name of the book you want to view:");
                    string bookTitle = Console.ReadLine();
                    GetBookDetails(bookTitle);
                }
                else if (option == 3)
                {
                    AddBook();
                }
                else return;
            }
        }

        private static void ListAllBooks()
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
            if (book == null)
            {
                Console.WriteLine($"Book '{title}' wasn't found. Do you want to add it? Y / N");
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
                    AddBook();
                }
                else { return; }
            }
            else
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
            while (true)
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

            var added = BookManager.AddBook(title, author, releaseYear);
            if (added) { Console.WriteLine($"The book {title} was successfully added!"); }
            else { Console.WriteLine("The book already exists!"); }
        }
    }
}
