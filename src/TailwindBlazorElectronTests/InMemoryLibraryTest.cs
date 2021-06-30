using Microsoft.EntityFrameworkCore;
using TailwindBlazorElectron.Data;

namespace TailwindBlazorElectronTests
{
    public class InMemoryLibraryTest : LibraryTest
    {
        public InMemoryLibraryTest() : base(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TestDatabase").Options) {}
    }
}
