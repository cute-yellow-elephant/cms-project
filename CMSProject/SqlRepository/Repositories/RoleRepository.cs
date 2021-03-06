﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain;

namespace SqlRepository.Repositories
{
    public class RoleRepository : RepositoryBasis<Role>
    {
        protected override DbSet<Role> _table { get { return _dbContext.Roles; } }
        public RoleRepository(DBContextContainer dbContext) : base(dbContext) { }

        public override void Create(Role entity)
        {
            var en = this.Find(entity.Name);
            if (en != null)
                throw new Exception("The role " + en.Name + ", id " + en.ID + " already exists in the database");
            base.Create(entity);
        }

        public override Role Read(Guid id)
        {
            if (_table.Find(id) == null)
                return null;
            return base.Read(id);
        }

        public override void Update(Role entity)
        {
            var x = this.Find(entity.Name);
            x.Name = entity.Name;
            x.Users = entity.Users;
        }

        public override Role Find(string name)
        {
            var list = this.ReadAll();
            if (list == null)
                return null;
            foreach (var x in list)
                if (String.Compare(x.Name, name) == 0)
                    return x;
            return null;
        }

        public override void Delete(Guid id)
        {
            this._table.Remove(this._table.Find(id));
        }
    }
}
