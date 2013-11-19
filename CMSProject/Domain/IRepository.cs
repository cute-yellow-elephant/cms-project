using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<T> where T: EntityBase
    {
        void Create(T entity);
        T Read(Guid id);
        ICollection<T> ReadAll();
        void Update(T entity);
        void Delete(Guid id);
        T Find(string name);
    }
}
