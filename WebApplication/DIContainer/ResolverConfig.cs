using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;
using DAL.Interface.Repository;
using DAL.Repository;
using Generator.CustomGenerator.Interface;
using Ninject;

namespace DIContainer
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            
            if (isWeb)
            {
            }
            else
            {
            }
            kernel.Bind<IGenerator>().To<Generator.CustomGenerator.Generator>().InSingletonScope();

            #region repositories 
            kernel.Bind<IRepository<ToDoItem>>().To<DropBoxRepository>();
            kernel.Bind<IQueueRepository>().To<DropBoxQueueRepository>();
            #endregion
        }
    }
}
