namespace SpaceEngineers.ProjectGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Core.CliArgumentsParser;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;
    using Core.Basics;
    using ExecutableApplication.Abstractions;

    [Lifestyle(EnLifestyle.Singleton)]
    internal class GeneratorApplicationStartup : IApplicationStartup
    {
        private readonly ICliArgumentsParser _cliArgumentsParser;

        private readonly IMasterInfoProvider _masterInfoProvider;

        private readonly ICollection<ISettingsGenerator> _generators;

        public GeneratorApplicationStartup(ICliArgumentsParser cliArgumentsParser,
                                           IMasterInfoProvider masterInfoProvider,
                                           ICollection<ISettingsGenerator> generators)
        {
            _cliArgumentsParser = cliArgumentsParser;
            _masterInfoProvider = masterInfoProvider;
            _generators = generators;
        }

        public void Run(string[] args)
        {
            var generatorCliArgs = _cliArgumentsParser
                                  .Parse<UnsafeGeneratorCliArgs>(args)
                                  .ToSafe();

            Debug.WriteLine(generatorCliArgs.ShowProperties(BindingFlags.Instance | BindingFlags.NonPublic));

            var solutionInfo = _masterInfoProvider.GetSolutionInfo(generatorCliArgs);
            
            Console.WriteLine($"\nGenerate settings for '{solutionInfo.SolutionName}.sln'");
            
            Task.WhenAll(_generators.SelectMany(g => g.Generate(solutionInfo))).Wait();
        }
    }
}