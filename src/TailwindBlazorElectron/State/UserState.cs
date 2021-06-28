
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TailwindBlazorElectron.Model;
using TailwindBlazorElectron.Services;

namespace TailwindBlazorElectron.State
{
	public abstract class UserState
	{
        public virtual void Entry(User user = null) { }
		public User CurrentUser { get; set; }
		public bool HasUser() => CurrentUser is not null;
		public bool UserIsLibrarian() => CurrentUser?.Role == Role.Librarian;
		public virtual void SignIn(string email, string password) { }
		public virtual void SignOut() { }
	}

	public class AuthenticatedState : UserState
	{
		protected LibraryService _context;

		public AuthenticatedState(LibraryService context)
		{
			_context = context;
		}

		public override void Entry(User user)
		{
			CurrentUser = user;
		}

        public override void SignOut()
        {
			_context.UserState = new UnauthenticatedState(_context);
			_context.UserState.Entry();
		}
    }

	public class UnauthenticatedState : UserState
	{
		protected LibraryService _context;

		public UnauthenticatedState(LibraryService context)
		{
			_context = context;
		}

		public override void Entry(User user = null)
		{
			CurrentUser = null;
		}

		public override void SignIn(string email, string password)
        {
			var user = _context.DbContext.Accounts
					 .Include(a => a.User)
						 .ThenInclude(u => u.Address)
					 .Include(a => a.User)
						.ThenInclude(u => u.Notifications)
					.Include(a => a.User)
						.ThenInclude(u => u.Subscription)
					.FirstOrDefault(a => a.Email == email && a.Password == password)?.User;

			if (user is not null)
			{
				_context.UserState = new AuthenticatedState(_context);
				_context.UserState.Entry(user);
			}
		}
	}
}