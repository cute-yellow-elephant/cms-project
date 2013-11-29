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
                var role = new Role(name);
                _entityRepository.Create(role);
                _logger.WriteProgramWorkflow(String.Format("New role was added: {0}, {1}", role.Name, role.ID));
            }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); throw new Exception(error.Message); }
        }
    }
}
