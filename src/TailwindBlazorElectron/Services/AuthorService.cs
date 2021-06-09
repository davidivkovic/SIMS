using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailwindBlazorElectron.Data;
using TailwindBlazorElectron.Model;

namespace TailwindBlazorElectron.Services
{
    public class AuthorService
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthorService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Author> GetPopularAuthors(int amount) 
        {
            if (!_dbContext.Authors.Any())
            {
                SeedAuthors();
            }
            return _dbContext.Authors.Take(amount).ToList();
        }

        public bool SeedAuthors()
        {
            List<Author> authors = new()
            {
                new()
                {
                    Name = "Stephen King",
                    NumberOfBooks = 60,
                    NumberOfReads = 46000,
                    DateOfBirth = new DateTime(1946, 9, 21),
                    ImageUrl = "http://firewireblog.com/wp-content/uploads/2014/01/stephen-king.jpg"
                },
                new()
                {
                    Name = "Joanne Rowling",
                    NumberOfBooks = 12,
                    NumberOfReads = 18000,
                    DateOfBirth = new DateTime(1965, 7, 31),
                    ImageUrl = "https://c.ndtvimg.com/2020-05/ngh0qgoo_jk-rowling_625x300_27_May_20.jpg"
                },
                new()
                {
                    Name = "Ray Bradbury",
                    NumberOfBooks = 16,
                    NumberOfReads = 32000,
                    DateOfBirth = new DateTime(1920, 8, 22),
                    ImageUrl = "https://static01.nyt.com/images/2012/06/07/books/Bradbury/Bradbury-jumbo.jpg?quality=75&auto=webp&disable=upscale"
                }
            };

            _dbContext.Authors.AddRange(authors);
            return _dbContext.SaveChanges() != 0;
        }
    }
}
