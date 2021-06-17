
using TailwindBlazorElectron.Model;

namespace TailwindBlazorElectron.Store
{
	public static class UserStore
	{
		public static User CurrentUser { get; set; }
		public static void SetCurrentUser(User user) => CurrentUser = user;
		public static bool HasUser() => CurrentUser is not null;
		public static bool UserIsLibrarian() => CurrentUser?.Role == Role.Librarian;
		public static void RemoveUser() => CurrentUser = null;
	}
}