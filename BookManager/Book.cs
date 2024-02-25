using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProjects.BookManager
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int ReleaseYear { get; set; }

        public Book(string title, string author, int releaseYear)
        {
            Title = title;
            Author = author;
            ReleaseYear = releaseYear;
        }

        public override string ToString()
        {
            return $"Name: {Title},\nAuthor: {Author},\nRelease Year: {ReleaseYear}";
        }

    }
}
