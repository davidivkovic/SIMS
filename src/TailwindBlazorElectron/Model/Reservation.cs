using System;

namespace TailwindBlazorElectron.Model
{
	public enum ReservationStatus
	{
		Pending,
		Approved,
		Declined
	};

	public class Reservation
	{
		public Guid Id { get; set; }
		public Edition Edition { get; set; }
		public ReservationStatus Status { get; private set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ApprovedAt { get; set; }
		public DateTime PickedUpAt { get; set; }
		public DateTime DueDate { get; set; }
		public DateTime ReturnedAt { get; private set; }
		public User User { get; set; }
		public bool IsPending => Status == ReservationStatus.Pending;
		public bool IsApproved => Status == ReservationStatus.Approved;
		public bool IsDeclined => Status == ReservationStatus.Declined;
		public bool HasBeenPickedUp => PickedUpAt != default;
		public bool HasBeenReturned => ReturnedAt != default;
		public void MarkAsPickedUp(DateTime date) => PickedUpAt = date;

		public void MarkAsReturned(DateTime date)
		{
			ReturnedAt = date;
			Edition.Returned();
		}

		public void Decline()
		{
			Status = ReservationStatus.Declined;
		}

		public void Approve()
		{
			Status = ReservationStatus.Approved;
			ApprovedAt = DateTime.Now;
			DueIn(User.BookRetentionTime());
		}

		public void DueIn(TimeSpan retentionTime)
		{
			DueDate = CreatedAt + retentionTime;
		}
	}
}
