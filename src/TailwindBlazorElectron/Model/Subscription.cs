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
		public int RemainingTime() { return (EndTime - DateTime.Now).Days; } 

		public double CurrentPercentage() 
		{
			int TotalDays = (EndTime - StartDate).Days;
			return 100 * (TotalDays - RemainingTime()) / TotalDays;
		}
	}
}
