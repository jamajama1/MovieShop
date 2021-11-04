using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? HashedPassword { get; set; }
        public string? Salt { get; set; }
        public string? PhoneNumber { get; set; }
        public int? TwoFactorEnabled { get; set; }
        public DateTime? LockOutEndDate { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public int? IsLocked { get; set; }
        public int? AccessFailedCount { get; set; }

        //Navigation property
        public ICollection<Review> Reviews { get; set; }
    }
}
