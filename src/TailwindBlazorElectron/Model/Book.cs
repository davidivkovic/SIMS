using System;
using System.Collections.Generic;

namespace TailwindBlazorElectron.Model
{
    public class Book
    {
        public Guid Id { get; set; }
        public string ISBN13 { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfRatings { get; set; }
        public double AverageRating { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public string YearPublished { get; set; }
        public Author Author { get; set; }
    }
}
