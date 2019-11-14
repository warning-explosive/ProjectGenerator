namespace SpaceEngineers.ProjectGenerator
{
    using System.Collections.Generic;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;
    using Enumerations;
    using Infos;

    [Lifestyle(EnLifestyle.Singleton)]
    internal class ProjectSettingsProviderImpl : IProjectSettingsProvider
    {
        public ProjectSettings GenerateProjectSettings(MasterInfo masterInfo, GeneratorCliArgs generatorCliArgs)
        {
            return new ProjectSettings(ProjectWideSettings(masterInfo.AssemblyInfo,
                                                           masterInfo.RepositoryInfo,
                                                           generatorCliArgs.GeneratePackageOnBuild),
                                       DebugSettings(),
                                       ReleaseSettings());
        }

        private IDictionary<string, string?> ProjectWideSettings(
            AssemblyInfo assemblyInfo,
            RepositoryInfo repositoryInfo,
            bool generatePackageOnBuild)
        {
            var dict = new Dictionary<string, string?>
                       {
                           ["TargetFramework"] = assemblyInfo.ProjectType == EnProjectType.Library
                                                     ? "netstandard2.0;net472"
                                                     : "netcoreapp3.0;net472",
                           ["LangVersion"] = "latest",
                           // project identity
                           ["AssemblyName"] = assemblyInfo.ToString(),
                           ["RootNamespace"] = assemblyInfo.ToString(),
                           ["Company"] = AssemblyInfo.SpaceEngineers,
                           ["Authors"] = AssemblyInfo.SpaceEngineers,
                           ["RepositoryUrl"] = repositoryInfo.ToString(),
                           ["RepositoryType"] = repositoryInfo.RepositoryType.ToString("G")
                                                              .ToLowerInvariant(),
                           // analysis
                           ["RunAnalyzersDuringBuild"] = "true",
                           ["RunAnalyzersDuringLiveAnalysis"] = "true",
                           ["RunAnalyzers"] = "true",
                           // build
                           ["GeneratePackageOnBuild"] = $"{generatePackageOnBuild}",
                           ["TreatWarningsAsErrors"] = "true",
                           ["AutoGenerateBindingRedirects"] = "true",
                           // nullable-reference
                           ["Nullable"] = "enable",
                           // run-time
                           ["TieredCompilation"] = "true",
                       };

            if (assemblyInfo.ProjectType == EnProjectType.Library)
            {
                dict.Add("Library", null);
            }
            
            return dict;
        }

        private IDictionary<string, string?> DebugSettings()
        {
            return new Dictionary<string, string?>
                   {
                       ["DefineConstants"] = "DEBUG;TRACE",
                   };
        }

        private IDictionary<string, string?> ReleaseSettings()
        {
            return new Dictionary<string, string?>();
        }
    }
}