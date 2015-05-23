using System;
using Castle.Windsor;

namespace Selkie.Aco.Example
{
    internal static class Program
    {
        private static void Main()
        {
            var container = new WindsorContainer();
            var installer = new Installer();

            var anthill = new AnthillProgram(container,
                                             installer);

            anthill.Main();

            Environment.Exit(0);
        }
    }
}