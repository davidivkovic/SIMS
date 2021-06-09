using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TailwindBlazorElectron.Model
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndTime { get; set; }
        public double Price{ get; set; }

        public Subscription() 
        {
            EndTime = DateTime.Now.AddYears(1);
            Price = 500;
        }
    }
}
