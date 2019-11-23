namespace SpaceEngineers.ProjectGenerator
{
    using System.Collections.Generic;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;

    [Lifestyle(EnLifestyle.Singleton)]
    internal class CsprojSettingsProviderImpl : ICsprojSettingsProvider
    {
        public CsprojSettings GenerateProjectSettings(MasterInfo masterInfo)
        {
            return new CsprojSettings(ProjectWideSettings(masterInfo.AssemblyInfo,
                                                          masterInfo.RepositoryInfo),
                                      DebugSettings(),
                                      ReleaseSettings());
        }

        private IDictionary<string, string?> ProjectWideSettings(AssemblyInfo assemblyInfo, RepositoryInfo repositoryInfo)
        {
            var isLibrary = assemblyInfo.AssemblyName.EndsWith(".Test");

            var dict = new Dictionary<string, string?>
                       {
                           // development
                           ["TargetFramework"] = isLibrary
                                                     ? "netcoreapp3.0"
                                                     : "netstandard2.0",
                           ["LangVersion"] = "latest",
                           ["Nullable"] = "enable",
                           // project identity
                           ["AssemblyName"] = assemblyInfo.ToString(),
                           ["RootNamespace"] = assemblyInfo.ToString(),
                           // nuget
                           ["IsPackable"] = isLibrary
                                                ? "false"
                                                : "true",
                           ["Title"] = assemblyInfo.ToString(),
                           ["Authors"] = AssemblyInfo.SpaceEngineers,
                           ["Company"] = AssemblyInfo.SpaceEngineers,
                           ["PackageDescription"] = assemblyInfo.ToString(),
                           ["RepositoryType"] = repositoryInfo.RepositoryType.ToLowerInvariant(),
                           ["RepositoryUrl"] = repositoryInfo.ToString(),
                           ["Copyright"] = "Copyright (c) 2019",
                           // analysis
                           ["RunAnalyzersDuringBuild"] = "true",
                           ["RunAnalyzersDuringLiveAnalysis"] = "true",
                           ["RunAnalyzers"] = "true",
                           // build
                           ["GenerateAssemblyInfo"] = "false",
                           ["GeneratePackageOnBuild"] = "false",
                           ["TreatWarningsAsErrors"] = "true",
                           ["AutoGenerateBindingRedirects"] = "true",
                           ["GenerateDocumentationFile"] = isLibrary
                                                               ? "false"
                                                               : "true",
                           // run-time
                           ["TieredCompilation"] = "true",
                       };

            if (isLibrary)
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