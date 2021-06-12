using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TailwindBlazorElectron.Data;
using TailwindBlazorElectron.Model;

namespace TailwindBlazorElectron.Services
{
    public class BookService
    {
        private readonly ApplicationDbContext _dbContext;

        public BookService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Book> GetBooks(int amount)
        {
            if (!_dbContext.Books.Any())
            {
                SeedBooks();
            }
            return _dbContext.Books.Include(b => b.Author).Take(amount).ToList();
        }

        public void SeedBooks() 
        {
            string json = File.ReadAllText("Data/all_books.json");
            var books = JsonSerializer.Deserialize<List<Book>>(json);
            _dbContext.Books.AddRange(books);
            _dbContext.SaveChanges();
        }
    }
}
