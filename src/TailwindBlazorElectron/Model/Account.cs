using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TailwindBlazorElectron.Model
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User User { get; set; }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

    }
}
