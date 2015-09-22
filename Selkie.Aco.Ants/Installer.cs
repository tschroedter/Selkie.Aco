using System.Diagnostics.CodeAnalysis;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Windsor;

namespace Selkie.Aco.Ants
{
    // ReSharper disable MaximumChainedReferences
    //ncrunch: no coverage start
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class Installer : BaseInstaller <Installer>
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "Selkie.";
        }

        protected override void InstallComponents(IWindsorContainer container,
                                                  IConfigurationStore store)
        {
            base.InstallComponents(container,
                                   store);

            container.Register(
                               Classes.FromThisAssembly()
                                      .BasedOn <IAnt>()
                                      .WithServiceFromInterface(typeof ( IAnt ))
                                      .Configure(c => c.LifeStyle.Is(LifestyleType.Transient)));
        }
    }
}