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
    public class RoleBC : BaseEntityBC<Role>
    {
        public RoleBC(ILogger logger, DBContextContainer dbcontext)
            : base(logger, dbcontext)
        {
            _entityRepository = new SqlRepository.Repositories.RoleRepository(dbcontext);
        }

        public void Create(string name)
        {
            try
            {
                _entityRepository.Create(new Role(name));
                var c = _entityRepository.Find(name);
                _logger.WriteProgramWorkflow(String.Format("New role was added: {0}, {1}", c.Name, c.ID));
            }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
        }
    }
}
