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
    public class PostBC : BaseEntityBC<Post>
    {
        public PostBC(ILogger logger, DBContextContainer dbcontext)
            : base(logger, dbcontext)
        {
            _entityRepository = new SqlRepository.Repositories.PostRepository(dbcontext);
        }

        public void Create(string title, string content)
        {
            try
            {
                var post = new Post(title, content);
                _entityRepository.Create(post);
                _logger.WriteProgramWorkflow(String.Format("New post was added: {0}, {1}", post.Title, post.ID));
            }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
        }
    }
}
