using JetBrains.Annotations;
using Selkie.Windsor;

namespace Selkie.Aco.Common
{
    [UsedImplicitly]
    public class Installer : BaseInstaller <Installer>
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "Selkie.";
        }
    }
}