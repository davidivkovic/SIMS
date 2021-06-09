using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TailwindBlazorElectron.Model
{
    public class Edition
    {
        public Guid Id { get; set; }
        public Book Book { get; set; }
        public int QuantityAvailable { get; set; }
        public DateTime YearPublished { get; set; }
    }
}
