
using TailwindBlazorElectron.Model;

namespace TailwindBlazorElectron.State
{
	public abstract class UserState
	{
		public User CurrentUser { get; set; }
		public bool HasUser() => CurrentUser is not null;
		public bool UserIsLibrarian() => CurrentUser?.Role == Role.Librarian;
	}

	public class AuthenticatedState : UserState
	{
		public AuthenticatedState(User user)
		{
			CurrentUser = user;
		}
	}

	public class UnauthenticatedState : UserState
	{
		public UnauthenticatedState()
		{
			CurrentUser = null;
		}
	}
}