using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TailwindBlazorElectron.Model
{
	public enum NotificationType
	{
		ReservationDeclined,
		ReservationApproved,
		SubscriptionEnded,
		BookOverdue
	}

	public class Notification
	{
		public Guid Id { get; set; }
		public string Content { get; set; }
		public DateTime SentAt { get; set; }
		public DateTime ReadAt { get; private set; }
		public NotificationType Type { get; set; }
		public string ImageUrl { get; set; }
		public void Read(DateTime date) => ReadAt = date;
	}
}
