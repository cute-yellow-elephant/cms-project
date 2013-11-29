namespace SqlRepository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Domain;
    using System.Collections.Generic;

    public class Configuration : DbMigrationsConfiguration<SqlRepository.DBContextContainer>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "SqlRepository.DBContextContainer";
        }

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
