namespace SC.LoggingPortal.CastleWindsor.Installers
{
    using Castle.MicroKernel.Registration;
    using SC.LoggingPortal.Data.Entity;
    using SC.LoggingPortal.Data.Repository;

    public class RepositoryInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<IRepository<LogMessage>>()
                .LifestyleSingleton()
                .ImplementedBy<LoggingRepository>());
        }
    }
}
