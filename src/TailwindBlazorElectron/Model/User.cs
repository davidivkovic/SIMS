using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TailwindBlazorElectron.Model
{
	public class User
	{
		public Guid Id { get; set; }
		public string SSN { get; set; }
		public string ImageUrl { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public Sex Sex { get; set; }
		public Role Role { get; set; }
		public Status Status { get; set; }
		public Account Account { get; set; }
		public Subscription Subscription { get; set; }
		public Address Address { get; private set; }
		public ICollection<Notification> Notifications { get; private set; } = new List<Notification>();
		public ICollection<Reservation> Reservations { get; private set; } = new List<Reservation>();
		public string FullName => $"{FirstName} {LastName}";
		public bool HasUnreadNotifications => Notifications.Any(n => n.ReadAt == default);

		[NotMapped]
		public List<Notification> UnreadNotifications => Notifications.Where(n => n.ReadAt == default).ToList();

		public TimeSpan BookRetentionTime()
		{
			int maxDays = Status switch
			{
				Status.MVP => 30,
				Status.Retiree => 21,
				Status.Child or Status.Student or Status.Adult or _ => 15
			};

			return TimeSpan.FromDays(maxDays);
		}

		public Subscription Subscribe(SubscriptionModel subscription)
		{
			return Subscription = new()
			{
				StartDate = DateTime.Now,
				EndTime = DateTime.Now.AddMonths(subscription.DurationInMonths),
				Price = subscription.PriceInUsd,
				UserId = Id
			};
		}

		public void Move(Address adress)
		{
			Address = adress;
		}

		public void Notify(Notification notification)
		{
			Notifications.Add(notification);
		}

		public void AddReservation(Reservation reservation)
		{
			Reservations.Add(reservation);
		}
	}
}
