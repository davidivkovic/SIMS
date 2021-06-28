using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TailwindBlazorElectron.Model
{
	public class Edition
	{
		public Guid Id { get; set; }
		public Book Book { get; set; }
		public string ISBN13 { get; set; }
		public string Title { get; set; }
		public string Publisher { get; set; }
		public string YearPublished { get; set; }
		public int QuantityAvailable { get; set; }
		public ICollection<Author> Authors { get; set; }

		[NotMapped]
		public ICollection<Author> AllAuthors => new List<Author>(Authors).Append(Book.Author).ToList();

		public Reservation Reserve()
		{
			if (QuantityAvailable == 0)
			{
				return null;
			}

			Taken();

			Reservation reservation = new()
			{
				CreatedAt = DateTime.Now,
				Edition = this
			};

			return reservation;
		}

		public void Taken()
        {
			QuantityAvailable--;
        }

		public void Returned() 
		{
			QuantityAvailable++;
		}
	}
}
