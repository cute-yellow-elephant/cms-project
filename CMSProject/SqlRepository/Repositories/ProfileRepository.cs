using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;

namespace SqlRepository.Repositories
{
    public class ProfileRepository : RepositoryBasis<Profile>
    {
        protected override DbSet<Profile> _table { get { return _dbContext.Profiles; } }
        public ProfileRepository(DBContextContainer dbContext) : base(dbContext) { }

        public override void Create(Profile entity)
        {
            var en = this.Read(entity.UserID);
            if (en != null)
                throw new Exception("The profile of user with ID " + entity.UserID + " already exists in the database");
            base.Create(entity);
        }

        public override void Update(Profile entity)
        {
            var x = this.Read(entity.ID);
            x.FirstName = entity.FirstName;
            x.LastName = entity.LastName;
            x.IsDeleted = entity.IsDeleted;
        }

        public override Profile Read(Guid userID)
        {
            var list = this.ReadAll();
            if (list == null)
                return null;
            foreach (var x in list)
                if (x.UserID == userID)
                    return x;
            return null;
        }
    }
}
