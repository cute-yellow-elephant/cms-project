﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;

namespace SqlRepository.Repositories
{
    public abstract class RepositoryBasis<T> : IRepository<T> where T : EntityBase
    {
        protected DBContextContainer _dbContext;
        protected abstract DbSet<T> _table { get; }

        public RepositoryBasis(DBContextContainer dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void Create(T entity)
        {
            _table.Add(entity);
        }

        public virtual T Read(Guid id)
        {
            if (_table.Find(id) == null)
                return null;
            return _table.Find(id);
        }

        public ICollection<T> ReadAll()
        {
            if (_table.Count<T>() == 0)
                return null;
            return _table.ToList<T>();
        }

        public virtual void Update(T entity) { }

        public virtual void Delete(Guid id)
        {
            var entity = this.Read(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                this.Update(entity);
            }
            else throw new Exception(String.Format("Не существует объекта с ID = {0}. Удаление невозможно.", id));
        }

        public virtual T Find(string name) { return null; }

    }
}
