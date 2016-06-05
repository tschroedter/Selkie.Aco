﻿using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Selkie.Windsor;

namespace Selkie.Aco.Example
{
    [ExcludeFromCodeCoverage]
    public class Installer
        : BasicConsoleInstaller,
          IWindsorInstaller
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "Selkie.";
        }
    }
}