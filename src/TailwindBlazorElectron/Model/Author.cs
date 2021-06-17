using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TailwindBlazorElectron.Model
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfBooks { get; set; }
        public int NumberOfReads { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Edition> Editions { get; set; }
    }
}
