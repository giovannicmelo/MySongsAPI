using MySongsApi.API.DependenciesResolver;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySongsApi.API.App_Start
{
    public class NinjectConfig
    {
        public static IKernel Kernel { get; private set; }

        public static void CreateKernel()
        {
            Kernel = new StandardKernel();
            var modules = new List<INinjectModule>
            {
                new RepositoryModule()
            };
            Kernel.Load(modules);
        }
    }
}