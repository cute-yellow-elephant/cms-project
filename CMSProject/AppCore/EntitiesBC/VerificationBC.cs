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
    public class VerificationBC:BaseEntityBC<Verification>
    {
        public VerificationBC(ILogger logger, DBContextContainer dbcontext)
            : base(logger, dbcontext)
        {
            _entityRepository = new SqlRepository.Repositories.VerificationRepository(dbcontext);
        }

        public void Create(string email)
        {
            try
            {
                var verification = new Verification(email);
                _entityRepository.Create(verification);
                _logger.WriteProgramWorkflow(String.Format("New verification guid {0} has been sent to {1}. Added to the db.", verification.ID, verification.Email));
            }
            catch (Exception error) { _logger.WriteIfErrorOccured(String.Format("AppCore.EntitiesBC.VerificationBC.Create: {0}",error.Message) ); }
        }

        public bool IsEmailWithSuchGuidRegistered(string email, Guid id)
        {
            var verification = this._entityRepository.Find(email);
            if (verification != null)
                if (verification.ID == id) return true;
            return false;
        }
    }
}
