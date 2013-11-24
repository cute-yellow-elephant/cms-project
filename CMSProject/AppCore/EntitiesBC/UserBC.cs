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
    public class UserBC : BaseEntityBC<User>
    {
        public UserBC(ILogger logger, DBContextContainer dbcontext)
            : base(logger, dbcontext)
        {
            _entityRepository = new SqlRepository.Repositories.UserRepository(dbcontext);
        }

        public void Create(string login, string email, string password,
            DateTime addedDate, DateTime lastVisitDate, bool isVerified, bool isOnline)
        {
            try
            {
                var user = new User(login, email, password, addedDate, lastVisitDate, isVerified, isOnline);
                _entityRepository.Create(user);
                _logger.WriteProgramWorkflow(String.Format("Новый пользователь был зарегистрирован: ID = {0}, Login = {1}, IsVerified={2}", user.ID, user.Login, user,isVerified));
            }
            catch (Exception error) 
            { 
                _logger.WriteIfErrorOccured("AppCore.EntitiesBC.UserBC: "+error.Message); 
                throw new Exception(error.Message);
            }
        }

        public bool IsEmailRegistered(string email)
        {
            var list = this.ReadAll();
            foreach (var x in list)
                if (String.Compare(email, x.Email) == 0)
                    return true;
            return false;
        }

        public User FindByEmail(string email)
        {
            var list = this.ReadAll();
            foreach (var x in list)
                if (String.Compare(email, x.Email) == 0)
                    return x;
            return null;
        }

        public bool ShouldCreateAdmin()
        {
            if (this.ReadAll() == null)
                return true;
            else return false;
        }

        public void VerifyUser(string email)
        {
            this.FindByEmail(email).IsVerified = true;
        }

        public bool IsInRole(User user, Role role)
        {
            if (user.Roles.Contains(role))
                return true;
            else return false;
        }

        public void ChangeOnlineState(string username, bool state)
        {
            var user = Find(username) ?? FindByEmail(username);
            if (user == null)
                throw new Exception("Нельзя изменить online состояние несуществующего пользователя");
            user.IsOnline = state;
        }
    }
}
