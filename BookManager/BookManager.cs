using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProjects.BookManager
{
    public class BookManager
    {
        public static List<Book> books = new List<Book>()
        {
            new Book("To Kill a Mockingbird", "Harper Lee", 1960),
            new Book("One Hundred Years of Solutide", "Gabriel García Márquez", 1967),
            new Book("The Lord of the Rings", "J.R.R Tolkien", 1937),
            new Book("The Alchemist", "Paulo Coelho", 1988)
        };

        public static bool AddBook(string title, string author, int year)
        {
            // check if book already exists
            var bookExists = books.Any(b => b.Title == title && b.Author == author && b.ReleaseYear == year);

            if (bookExists) return false;

            var book = new Book(title, author, year);
            books.Add(book);
            return true;
        }

        public static List<Book> GetAllBooks() // just return 'books' entity
        {
            return books;
        }


        public static Book SearchBookByTitle(string title) // searching with linq
        {
            var book = books.FirstOrDefault(x => x.Title == title);
            return book;
        }
    }
}
