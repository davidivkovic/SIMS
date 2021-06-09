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

        public User SignIn(string email, string password)
        {
            var user = _dbContext.Accounts.FirstOrDefault(a => a.Email == email && a.Password == password)?.User;
            return user;
        }

        public User SignUp(string email, string password, User user, Address address)
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
            _dbContext.SaveChanges();

            return user;
        }

        public User GetUser(Guid id)
        {
            return _dbContext.Users.Find(id);
        }

        public Subscription SubscribeReader(User user)
        {
            Subscription subscription = new()
            {
                StartDate = DateTime.Now,
                User = user
            };

            user.Subscribe(subscription);
            _dbContext.SaveChanges();

            return subscription;
        }

        public Reservation RequestEditionReservation(User user, Edition edition)
        {
            var reservation = user.ReserveEdition(edition);
            _dbContext.SaveChanges();

            return reservation;
        }

        public void AllowReservation(Reservation reservation)
        {
            reservation.Allow();
            _dbContext.SaveChanges();
        }


    }
}
