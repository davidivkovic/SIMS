using Microsoft.EntityFrameworkCore;
using TailwindBlazorElectron.Data;
using TailwindBlazorElectron.Model;
using TailwindBlazorElectron.Services;
using System.Linq;
using Xunit;

namespace TailwindBlazorElectronTests
{
    public abstract class LibraryTest
    {
        //protected DbContextOptions<ApplicationDbContext> ContextOptions { get; }
        protected LibraryService LibraryService { get; set; }
        protected AuthorService AuthorService { get; set; }
        protected BookService BookService { get; set; }

        protected ApplicationDbContext DbContext;

        protected LibraryTest(DbContextOptions<ApplicationDbContext> contextOptions)
        {
            DbContext = new ApplicationDbContext(contextOptions);
            Setup();
            Seed();
        }

        public void Setup()
        {
            LibraryService = new(DbContext);
            AuthorService = new(DbContext);
            BookService = new(DbContext);
        }

        public void Seed()
        {
            if (!DbContext.Books.Any())
            {
                BookService.SeedBooks();
                BookService.SeedEditions();
            }
            AuthorService.SeedAuthors();
            LibraryService.SeedLibrarian();
        }

        [Fact]
        public void CanSignIn()
        {
            var user = LibraryService.SignIn("gorajkora@gmail.com", "123");
            Assert.NotNull(user);
        }

        [Fact]
        public void CannotSignIn()
        {
            var user = LibraryService.SignIn("test", "test");
            Assert.Null(user);
        }

        [Fact]
        public void CanSignUp()
        {
            var success = LibraryService.SignUp("test@gmail.com", "pass", new(), new(), new());
            Assert.True(success);
        }

        [Fact]
        public void CannotSignUp()
        {
            var success = LibraryService.SignUp("gorajkora@gmail.com", "123", new(), new(), new());
            Assert.False(success);
        }

        [Fact]
        public void CanSignOut()
        {
            var user = LibraryService.SignIn("gorajkora@gmail.com", "123");
            Assert.NotNull(user);
            LibraryService.SignOut();
            Assert.Null(LibraryService.UserState.CurrentUser);
        }

        [Fact]
        public void CanSubscribe()
        {
            var user = LibraryService.FindUserBySSN("1706000000000");
            Assert.NotNull(user);
            LibraryService.SubscribeReader(user, new());
            user = LibraryService.FindUserBySSN("1706000000000");
            Assert.NotNull(user.Subscription);
        }

        [Fact]
        public void CanReserveEdition()
        {
            var user = LibraryService.FindUserBySSN("1706000000000");
            var book = LibraryService.GetBookByIdTitle("11870085-the-fault-in-our-stars");
            Assert.NotNull(user);
            Assert.NotNull(book);
            var reservation = LibraryService.RequestReservation(book.Editions.FirstOrDefault(e => e.QuantityAvailable > 0), user);
            Assert.NotNull(reservation);
        }

        [Fact]
        public void CannotReserveEdition()
        {
            var user = LibraryService.FindUserBySSN("1706000000000");
            var book = LibraryService.GetBookByIdTitle("11870085-the-fault-in-our-stars");
            Assert.NotNull(user);
            Assert.NotNull(book);
            var reservation = LibraryService.RequestReservation(book.Editions.FirstOrDefault(e => e.QuantityAvailable == 0), user);
            Assert.Null(reservation);
        }

        [Fact]
        public void CanAllowReservation()
        {
            var user = LibraryService.FindUserBySSN("1706000000000");
            var book = LibraryService.GetBookByIdTitle("11870085-the-fault-in-our-stars");
            Assert.NotNull(user);
            Assert.NotNull(book);
            var reservation = LibraryService.RequestReservation(book.Editions.FirstOrDefault(e => e.QuantityAvailable > 0), user);
            Assert.NotNull(reservation);

            reservation = LibraryService.GetAllReservations().First(r => r.Id == reservation.Id);
            Assert.NotNull(reservation);

            LibraryService.AllowReservation(reservation);

            var allowedReservation = LibraryService.GetAllReservations().First(r => r.Id == reservation.Id);
            Assert.Equal(ReservationStatus.Approved, allowedReservation.Status);
        }
    }
}
