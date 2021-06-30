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
            SeedAuthors();
            return _dbContext.Authors.Take(amount).ToList();
        }

        public bool SeedAuthors()
        {
            if (_dbContext.Authors.Any()) return false;

            List<Author> authors = new()
            {
                new()
                {
                    Name = "Stephen King",
                    NumberOfBooks = 60,
                    NumberOfReads = 46000,
                    DateOfBirth = new DateTime(1946, 9, 21),
                    ImageUrl = "stephen-king.jpg"
                },
                new()
                {
                    Name = "Joanne Rowling",
                    NumberOfBooks = 12,
                    NumberOfReads = 18000,
                    DateOfBirth = new DateTime(1965, 7, 31),
                    ImageUrl = "ngh0qgoo_jk-rowling_625x300_27_May_20.webp"
                },
                new()
                {
                    Name = "Ray Bradbury",
                    NumberOfBooks = 16,
                    NumberOfReads = 32000,
                    DateOfBirth = new DateTime(1920, 8, 22),
                    ImageUrl = "Bradbury-jumbo.jpg"
                }
            };

            _dbContext.Authors.AddRange(authors);
            return _dbContext.SaveChanges() != 0;
        }
    }
}
