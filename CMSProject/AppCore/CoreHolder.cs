using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Logging;
using Infrastructure.DI;
using Domain;
using SqlRepository;
using SqlRepository.Repositories;
using System.Data.Entity;
using Ninject;
using AppCore.EntitiesBC;

namespace AppCore
{
    public class CoreHolder
    {
        private ILogger _logger;
        private DBContextContainer _dbcontext;
        public UserBC UserRepository { get; set; }
        public RoleBC RoleRepository { get; set; }
        public ProfileBC ProfileRepository { get; set; }
        public VerificationBC VerificationRepository { get; set; }

        public void Submit()
        {
            _dbcontext.SaveChanges();
        }

        private void InitializeRepositories()
        {
            UserRepository = new UserBC(_logger, _dbcontext);
            RoleRepository = new RoleBC(_logger, _dbcontext);
            ProfileRepository = new ProfileBC(_logger, _dbcontext);
            VerificationRepository = new VerificationBC(_logger, _dbcontext);

        }

        public CoreHolder()
        {
            var serviceLocator = new StandardKernel(new NinjectLoggerCreator());
            _logger = serviceLocator.Get<ILogger>();
            InitializeDbIfNotExists();
            _dbcontext = new DBContextContainer();
            InitializeRepositories();
        }

        public static void InitializeDbIfNotExists()
        {
            Database.SetInitializer<DBContextContainer>(new DBInitializer());
        }

    }
}
