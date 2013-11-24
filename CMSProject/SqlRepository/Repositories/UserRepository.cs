using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;

namespace SqlRepository.Repositories
{
    public class UserRepository : RepositoryBasis<User>
    {
        protected override DbSet<User> _table { get { return _dbContext.Users; } }
        public UserRepository(DBContextContainer dbContext) : base(dbContext) { }

        public override void Create(User entity)
        {
            var en = this.Find(entity.Login);
            if (en != null)
                throw new Exception(String.Format("Пользователь с логином {0} уже зарегистрирован в системе", entity.Login));
            base.Create(entity);
        }

        public override void Update(User entity)
        {
            var x = this.Read(entity.ID);
            x.AddedDate = entity.AddedDate;
            x.Email = entity.Email;
            x.LastVisitDate = entity.LastVisitDate;
            x.Login = entity.Login;
            x.Password = entity.Password;
            x.Roles = entity.Roles;
        }

        public override User Find(string name)
        {
            var list = this.ReadAll();
            if (list == null)
                return null;
            foreach (var x in list)
                if (String.Compare(x.Login, name) == 0)
                    return x;
            return null;
        }
    }
}
