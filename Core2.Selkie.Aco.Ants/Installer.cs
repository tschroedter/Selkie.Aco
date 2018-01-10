using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Common;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Ants
{
    // ReSharper disable MaximumChainedReferences
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class Installer : SelkieInstaller <Installer>
    {
        protected override void InstallComponents(IWindsorContainer container,
                                                  IConfigurationStore store)
        {
            base.InstallComponents(container,
                                   store);

            Assembly assembly = Assembly.GetAssembly(typeof( Installer ));

            container.Register(Classes.FromAssembly(assembly)
                                      .BasedOn <IAnt>()
                                      .WithServiceFromInterface(typeof( IAnt ))
                                      .Configure(c => c.LifeStyle.Is(LifestyleType.Transient)));
        }
    }
}