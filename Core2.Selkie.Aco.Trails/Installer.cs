using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Optimizers;
using Core2.Selkie.Common;
using Core2.Selkie.Windsor.Internals;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Trails
{
    [UsedImplicitly]
    public class Installer : SelkieInstaller <Installer>
    {
        [UsedImplicitly]
        protected readonly ConsoleLogger Logger = new ConsoleLogger();

        protected override void InstallComponents(IWindsorContainer container,
                                                  IConfigurationStore store)
        {
            base.InstallComponents(container,
                                   store);

            Assembly assembly = Assembly.GetAssembly(typeof( Installer ));

            container.Register(Classes.FromAssembly(assembly)
                                      .BasedOn <ITrailBuilder>()
                                      .WithServiceFromInterface(typeof( ITrailBuilder ))
                                      .Configure(c => c.LifeStyle.Is(LifestyleType.Transient)));

            container.Register(Component.For <IOptimizer>().ImplementedBy(typeof( TwoOptSimple )).LifestyleTransient());
        }
    }
}