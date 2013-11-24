using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;

namespace SqlRepository.Repositories
{
    public class VerificationRepository : RepositoryBasis<Verification>
    {
        protected override DbSet<Verification> _table { get { return _dbContext.Verifications; } }
        public VerificationRepository(DBContextContainer dbContext) : base(dbContext) { }

        public override void Create(Verification entity)
        {
            var en = this.Find(entity.Email);
            if (en != null)
                throw new Exception(String.Format("Email {0} уже ожидает подтверждения.", entity.Email));
            base.Create(entity);
        }

        public override Verification Find(string email)
        {
            var list = this.ReadAll();
            if (list == null)
                return null;
            foreach (var x in list)
                if (String.Compare(x.Email, email) == 0)
                    return x;
            return null;
        }

        public override void Delete(Guid id)
        {
            this._table.Remove(this._table.Find(id));
        }
    
    }
}
