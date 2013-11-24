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
    public class ProfileBC : BaseEntityBC<Profile>
    {
        public ProfileBC(ILogger logger, DBContextContainer dbcontext)
            : base(logger, dbcontext)
        {
            _entityRepository = new SqlRepository.Repositories.ProfileRepository(dbcontext);
        }

        public void Create(Guid userID, string firstName, string lastName)
        {
            try
            {
                var profile = new Profile(userID, firstName, lastName);
                _entityRepository.Create(profile);
                _logger.WriteProgramWorkflow(String.Format("New profile of user with Guid {0} was added : first name {1}, last name {2}", profile.UserID, profile.FirstName, profile,lastName));
            }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
        }
    }
}
