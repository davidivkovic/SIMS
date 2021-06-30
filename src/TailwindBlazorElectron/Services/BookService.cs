using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
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

		public List<Book> GetPopularBooks(int amount)
		{
			if (!_dbContext.Books.Any())
			{
				SeedBooks();
				SeedEditions();
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

		public void SeedEditions()
		{
			var books = _dbContext.Books.Include(b => b.Author).ToList();

			Random rng = new();

			books.ForEach(book =>
			{
				book.Editions = Enumerable.Range(0, 4).Select(x => new Edition()
				{
					Title = book.Title,
					Book = book,
					QuantityAvailable = 2,
					YearPublished = book.YearPublished,
					Authors = _dbContext.Authors.Take(2).ToList(),
					Publisher = "Amazon",
					ISBN13 = rng.NextInt64(9999999999999).ToString("D13")
				}).ToList();

				book.Editions.Skip(1).Take(2).ToList().ForEach(e => e.QuantityAvailable = 0);
			});

			_dbContext.SaveChanges();
		}
	}
}
