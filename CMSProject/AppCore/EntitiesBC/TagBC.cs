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
    public class TagBC : BaseEntityBC<Tag>
    {
        public TagBC(ILogger logger, DBContextContainer dbcontext)
            : base(logger, dbcontext)
        {
            _entityRepository = new SqlRepository.Repositories.TagRepository(dbcontext);
        }

        public void Create(string name)
        {
            try
            {
                var tag = new Tag(name);
                _entityRepository.Create(tag);
                _logger.WriteProgramWorkflow(String.Format("New tag was added: {0}, {1}", tag.Name, tag.ID));
            }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
        }
    }
}
