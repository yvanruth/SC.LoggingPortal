namespace SC.LoggingPortal.CastleWindsor.Installers
{
    using Castle.MicroKernel.Registration;
    using SC.LoggingPortal.Logic.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<ILoggingService>().ImplementedBy<LoggingService>());
        }
    }
}
