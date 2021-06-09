using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TailwindBlazorElectron.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sex Sex { get; set; }
        public Role Role { get; set; }
        public Status Status { get; set; }
        public Account Account { get; set; }
        public Subscription Subscription { get; set; }
        public Address Address { get; private set; }
        public ICollection<Notification> Notifications { get; private set; }
        public ICollection<Reservation> Reservations { get; private set; }

        public void Subscribe(Subscription subscription) 
        {
            Subscription = subscription;
        }

        public void Move(Address adress)
        {
            Address = adress;
        }

        public Reservation ReserveEdition(Edition edition) 
        {
            if(edition.QuantityAvailable == 0)
            {
                return null;
            }

            Reservation reservation = new()
            {
                CreatedAt = DateTime.Now,
                Edition = edition
            };

            Reservations.Add(reservation);
            return reservation;
        }

        public void Notify(Notification notification)
        {
            Notifications.Add(notification);
        }
    }
}
