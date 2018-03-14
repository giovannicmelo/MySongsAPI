using MySongsApi.Repositories.Impl;
using MySongsApi.Repositories.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySongsApi.Dependency.Resolvers
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAlbumRepository>().To<AlbumsRepository>();
            Bind<ISongRepository>().To<SongsRepository>();
            Bind<IBandRepository>().To<BandsRepository>();

            //Bind<IAlbumRepository>().To<AlbumsRepository>().WithConstructorArgument("context", new MySongsDbContext());
        }
    }
}