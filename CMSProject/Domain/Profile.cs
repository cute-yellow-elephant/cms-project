using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Profile:EntityBase
    {
        public Guid UserID { get; set; }
        public virtual User User { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Profile() { }
        public Profile(Guid userID, string firstName, string lastName)
        {
            UserID = userID;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
