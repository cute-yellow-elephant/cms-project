using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Ninject;
using Infrastructure.Logging;
using SqlRepository;

namespace AppCore.EntitiesBC
{
    public class BaseEntityBC<T> where T : EntityBase
    {
        protected IRepository<T> _entityRepository;
        protected ILogger _logger;

        public BaseEntityBC(ILogger logger, DBContextContainer dbcontext)
        {
            _logger = logger;
        }

        public T Read(Guid id)
        {
            try { return _entityRepository.Read(id); }
            catch (Exception error)
            {
                _logger.WriteIfErrorOccured(error.Message);
                return null;
            }
        }

        public ICollection<T> ReadAll()
        {
            try { return _entityRepository.ReadAll(); }
            catch (Exception error)
            {
                _logger.WriteIfErrorOccured(error.Message);
                return null;
            }
        }

        public T Find(string name)
        {
            try { return _entityRepository.Find(name); }
            catch (Exception error)
            {
                _logger.WriteIfErrorOccured(error.Message);
                return null;
            }
        }

        public void Update(T ent)
        {
            try { _entityRepository.Update(ent); }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
        }

        public void Delete(Guid id)
        {
            try { _entityRepository.Delete(id); }
            catch (Exception error) { _logger.WriteIfErrorOccured("AppCore.EntitiesBC.BaseEntityBC: "+error.Message); throw new Exception(error.Message); }
        }
    }
}
