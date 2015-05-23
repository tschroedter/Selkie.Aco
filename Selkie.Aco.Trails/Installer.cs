using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Aco.Trails.Optimizers;
using Selkie.Windsor;

namespace Selkie.Aco.Trails
{
    // ReSharper disable MaximumChainedReferences
    [UsedImplicitly]
    public class Installer : BaseInstaller <Installer>
    {
        protected override void InstallComponents(IWindsorContainer container,
                                                  IConfigurationStore store)
        {
            base.InstallComponents(container,
                                   store);

            container.Register(
                               Classes.FromThisAssembly()
                                      .BasedOn <ITrailBuilder>()
                                      .WithServiceFromInterface(typeof ( ITrailBuilder ))
                                      .Configure(c => c.LifeStyle.Is(LifestyleType.Transient)));

            container.Register(Component.For <IOptimizer>().ImplementedBy(typeof ( TwoOptSimple )).LifestyleTransient());
        }
    }

    // ReSharper restore MaximumChainedReferences
}