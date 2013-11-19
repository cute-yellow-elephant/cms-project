﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User : EntityBase
    {
        [StringLength(12)]
        public string Login { set; get; }
        [StringLength(50)]
        public string Email { set; get; }
        public int Password { set; get; }
        public DateTime AddedDate { set; get; }
        public DateTime LastVisitDate { set; get; }
        public virtual ICollection<Role> Roles { set; get; }

        public User() { }
        public User(string login, string email, int password,
            DateTime addedDate, DateTime lastVisitDate)
        {
            Login = login;
            Email = email;
            Password = password;
            AddedDate = addedDate;
            LastVisitDate = lastVisitDate;
        }

    }
}
