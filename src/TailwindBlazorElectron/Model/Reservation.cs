using System;

namespace TailwindBlazorElectron.Model
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Edition Edition { get; set; }
        public bool IsAllowed { get; private set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnedAt { get; private set; }

        public void Allow() => IsAllowed = true;
        public void MarkAsReturned(DateTime date) => ReturnedAt = date;

        public Reservation()
        {
            TimeSpan holdingDuration = TimeSpan.FromDays(15);
            DueDate = CreatedAt + holdingDuration;
        }
    }
}
