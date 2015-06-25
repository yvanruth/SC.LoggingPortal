namespace SC.LoggingPortal.CastleWindsor.Installers
{
    using Castle.MicroKernel.Registration;
    using SC.LoggingPortal.Data.Persistence;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DbContextInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<IMongoDbContext>().ImplementedBy<MongoDbContext>()
                .DependsOn(new Hashtable { { "connectionStringOrName", "mongoCollectionLocation" } }));
        }
    }
}
