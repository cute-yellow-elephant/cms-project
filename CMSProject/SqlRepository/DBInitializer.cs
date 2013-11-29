using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain;

namespace SqlRepository
{
    public class DBInitializer : CreateDatabaseIfNotExists<DBContextContainer>
    {
        protected override void Seed(DBContextContainer context)
        {
            GetRoles().ForEach(p => context.Roles.Add(p));
        }

        private static List<Role> GetRoles()
        {
            var roles = new List<Role> 
            {
                new Role
                {
                    Name = "Admin",
                },
                new Role
                {
                    Name = "User",
                },
            };
            return roles;
        }
    }
}
