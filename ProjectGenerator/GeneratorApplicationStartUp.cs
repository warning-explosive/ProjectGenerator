namespace SpaceEngineers.ProjectGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Core.CliArgumentsParser;
    using Core.CompositionRoot;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;
    using Core.Extensions;
    using ExecutableApplication.Abstractions;

    [Lifestyle(EnLifestyle.Singleton)]
    internal class GeneratorApplicationStartUp : IApplicationStartup
    {
        private readonly IMasterInfoProvider _masterInfoProvider;

        private readonly ICollection<ISettingsGenerator> _generators;
        
        public GeneratorApplicationStartUp(IMasterInfoProvider masterInfoProvider,
                                           ICollection<ISettingsGenerator> generators)
        {
            _masterInfoProvider = masterInfoProvider;
            _generators = generators;
        }

        public void Run(string[] args)
        {
            var generatorCliArgs = DependencyContainer.Resolve<ICliArgumentsParser>()
                                                      .Parse<UnsafeGeneratorCliArgs>(args)
                                                      .ToSafe();

            Debug.WriteLine(generatorCliArgs.ShowProperties(BindingFlags.Instance | BindingFlags.NonPublic));

            foreach (var masterInfo in _masterInfoProvider.GetMasterInfos(generatorCliArgs))
            {
                Console.WriteLine($"Generate settings for: {masterInfo.ProjectInfo.ProjectName}");

                Task.WhenAll(_generators.Select(g => g.Generate(masterInfo)))
                    .Wait();
            }
        }
    }
}