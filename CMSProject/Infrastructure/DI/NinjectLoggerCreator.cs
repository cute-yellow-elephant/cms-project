using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Infrastructure.Logging;

namespace Infrastructure.DI
{
    public class NinjectLoggerCreator : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ILogger>().To<ProjectLogger>();
        }
    }
}
