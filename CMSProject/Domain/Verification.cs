using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Verification:EntityBase
    {
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }

        public Verification() { }
        public Verification(string email)
        {
            Email = email;
            IsDeleted = false;
        }
    }
}
