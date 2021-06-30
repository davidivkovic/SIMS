using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TailwindBlazorElectron.State;
using TailwindBlazorElectron.Data;
using TailwindBlazorElectron.Model;
using Microsoft.AspNetCore.Components.Forms;
using System.IO;
using System.Threading.Tasks;

namespace TailwindBlazorElectron.Services
{
	public class LibraryService
	{
		public readonly ApplicationDbContext DbContext;

		public UserState UserState { get; set; }

		public LibraryService(ApplicationDbContext dbContext)
		{
			DbContext = dbContext;

			SeedLibrarian();

			UserState = new UnauthenticatedState(this);
			UserState.Entry();
		}

		public void SeedLibrarian()
		{
			var email = "gorajkora@gmail.com";
			var account = DbContext.Accounts.FirstOrDefault(a => a.Email == email);
			if (account is null)
			{
				SignUp(email, "123", new User()
				{
					FirstName = "David",
					LastName = "Ivkovic",
					SSN = "1706000000000",
					DateOfBirth = new DateTime(2000, 6, 17),
					Sex = Sex.Male,
					Status = Status.Adult,
					Role = Role.Librarian,
					ImageUrl = "placeholderpfp.png"
				},
				new Address()
				{
					ZipCode = 21000,
					City = "Novi Sad",
					Street = "Strazilovska",
					HouseNumber = "30"
				},
				new SubscriptionModel()
				{
					DurationInMonths = 12,
					PriceInUsd = 5.99
				});
			}
		}

		public void SignOut()
		{
			UserState.SignOut();
		}

		public User SignIn(string email, string password)
		{
			UserState.SignIn(email, password);
			return UserState.CurrentUser;
		}

		public bool SignUp(string email, string password, User user, Address address, SubscriptionModel subscription = null)
		{
			var existingAccount = DbContext.Accounts.SingleOrDefault(a => a.Email == email);

			if (existingAccount is not null)
			{
				return false;
			}

			Account account = new()
			{
				Email = email,
				Password = password,
				User = user,
				IsActive = true
			};

			user.Move(address);
			if (subscription is not null)
			{
				user.Subscribe(subscription);
			}
			user.Account = account;

			DbContext.Add(account);
			return DbContext.SaveChanges() != 0;
		}

		public Subscription SubscribeReader(User user, SubscriptionModel model)
		{
			var subscription = user.Subscribe(model);
			DbContext.SaveChanges();

			return subscription;
		}

		public Reservation RequestReservation(Edition edition, User user = null)
		{
			var reservation = edition.Reserve();

			if (reservation is null) return null;

			if (user is null)
			{
				user = UserState.CurrentUser;
				reservation.User = user;
			}
			else
			{
				reservation.User = user;
				AllowReservation(reservation);
				ReservationPickedUp(reservation);
			}

			user.AddReservation(reservation);

			DbContext.Add(reservation);
			DbContext.SaveChanges();

			return reservation;
		}

		public void DeclineReservation(Reservation reservation)
		{
			reservation.Decline();
			reservation.User.Notify(new Notification()
			{
				Content = $"Your reservation of {reservation.Edition.Title} has been declined. The book will not be available any time soon.",
				SentAt = DateTime.Now
			});

			DbContext.SaveChanges();
		}

		public void MarkNotificationAsRead(Notification notification)
		{
			notification.Read(DateTime.Now);
			DbContext.SaveChanges();
		}

		public void AllowReservation(Reservation reservation)
		{
			reservation.Approve();
			reservation.User.Notify(new Notification()
			{
				Content = $"Your reservation of {reservation.Edition.Title} has been approved. You can pick up the book in the next three days.",
				SentAt = DateTime.Now
			});
			DbContext.SaveChanges();
		}

		public User FindUserBySSN(string ssn)
		{
			return DbContext.Users.Include(u => u.Address)
								   .Include(u => u.Account)
								   .FirstOrDefault(u => u.SSN.StartsWith(ssn));
		}

		public void ReservationPickedUp(Reservation reservation)
		{
			reservation.MarkAsPickedUp(DateTime.Now);
			DbContext.SaveChanges();
		}

		public void ReservationReturned(Reservation reservation)
		{
			reservation.MarkAsReturned(DateTime.Now);
			DbContext.SaveChanges();
		}

		public Book GetBookByIdTitle(string IdTitle)
		{
			var book = DbContext.Books.Include(b => b.Author)
									   .Include(b => b.Genres)
									   .Include(b => b.Editions)
										   .ThenInclude(e => e.Authors)
									   .FirstOrDefault(b => b.IdTitle == IdTitle);

			return book;
		}

		public List<SubscriptionModel> GetSubscriptionModels()
		{
			return new List<SubscriptionModel>
			{
				new() { DurationInMonths = 1, PriceInUsd = 0.99 },
				new() { DurationInMonths = 6, PriceInUsd = 3.99 },
				new() { DurationInMonths = 12, PriceInUsd = 5.99 },
				new() { DurationInMonths = 24, PriceInUsd = 9.99 },
			};
		}

		public List<Reservation> GetAllReservations(User user = null)
		{
			if (user is not null)
			{
				return DbContext.Reservations
						.Include(r => r.User)
							.ThenInclude(u => u.Account)
						.Include(r => r.Edition)
							.ThenInclude(e => e.Book)
								.ThenInclude(b => b.Author)
						.Include(r => r.Edition)
							.ThenInclude(e => e.Authors)
							.Where(r => r.User.Id == user.Id)
						.ToList();
			}

			return DbContext.Reservations
					.Include(r => r.User)
						.ThenInclude(u => u.Account)
					.Include(r => r.Edition)
						.ThenInclude(e => e.Book)
							.ThenInclude(b => b.Author)
					.Include(r => r.Edition)
						.ThenInclude(e => e.Authors)
					.ToList();
		}
		public async Task ChangeProfileImage(IBrowserFile file)
		{
			var user = DbContext.Users.Find(UserState.CurrentUser.Id);

			string filename = user.SSN + Path.GetExtension(file.Name);

			await using FileStream fs = new($"wwwroot/assets/{filename}", FileMode.Create);
			await file.OpenReadStream(64000000).CopyToAsync(fs);

			user.ImageUrl = filename;
			DbContext.SaveChanges();
		}

		public void ResetProfileImage() 
		{
			var user = DbContext.Users.Find(UserState.CurrentUser.Id);
			user.ImageUrl = "placeholderpfp.png";
			DbContext.SaveChanges();
		}
	}
}
