using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailwindBlazorElectron.Data;
using TailwindBlazorElectron.Model;

namespace TailwindBlazorElectron.Services
{
	public class LibraryService
	{
		private readonly ApplicationDbContext _dbContext;

		public LibraryService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public User SignIn(string email, string password)
		{
			var user = _dbContext.Accounts
								 .Include(a => a.User)
								 .FirstOrDefault(a => a.Email == email && a.Password == password)?.User;
			return user;
		}

		public bool SignUp(string email, string password, User user, Address address)
		{
			Account account = new()
			{
				Email = email,
				Password = password,
				User = user,
				IsActive = true
			};

			user.Move(address);
			user.Account = account;

			_dbContext.Add(account);
			return _dbContext.SaveChanges() != 0;
		}

		public User GetUser(Guid id)
		{
			return _dbContext.Users.Find(id);
		}

		public Subscription SubscribeReader(User user, SubscriptionModel model)
		{
			Subscription subscription = new()
			{
				StartDate = DateTime.Now,
				EndTime = DateTime.Now.AddMonths(model.DurationInMonths),
				Price = model.PriceInUsd,
				User = user
			};

			user.Subscribe(subscription);
			_dbContext.SaveChanges();

			return subscription;
		}

		public Reservation RequestEditionReservation(User user, Edition edition)
		{
			var reservation = edition.Reserve();

			if(reservation is null)
            {
				return null;
            }

			reservation.DueIn(GetBookRetentionTime(user));
			user.AddReservation(reservation);

			_dbContext.Add(reservation);
			_dbContext.SaveChanges();

			return reservation;
		}

		public void AllowReservation(Reservation reservation)
		{
			reservation.Allow();
			_dbContext.SaveChanges();
		}

		public List<SubscriptionModel> GetSubscriptionModels()
		{
			return new List<SubscriptionModel>
			{
				new() { DurationInMonths = 1, PriceInUsd = 0.99 },
				new() { DurationInMonths = 3, PriceInUsd = 1.99 },
				new() { DurationInMonths = 6, PriceInUsd = 2.99 },
				new() { DurationInMonths = 12, PriceInUsd = 5.99 },
				new() { DurationInMonths = 24, PriceInUsd = 9.99 },
			};
		}
		public TimeSpan GetBookRetentionTime(User user)
		{
			int maxDays = user.Status switch
			{
				Status.MVP => 30,
				Status.Retiree => 21,
				Status.Child or Status.Student or Status.Adult or _ => 15
			};

			return TimeSpan.FromDays(maxDays);
		}
	}
}
