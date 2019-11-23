namespace SpaceEngineers.ProjectGenerator.Csproj
{
    using System.Collections.Generic;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;

    [Lifestyle(EnLifestyle.Singleton)]
    internal class CsprojSettingsProviderImpl : ICsprojSettingsProvider
    {
        public CsprojSettings GenerateProjectSettings(MasterInformation masterInformation)
        {
            return new CsprojSettings(ProjectWideSettings(masterInformation.AssemblyInformation, masterInformation.RepositoryInformation),
                                      DebugSettings(),
                                      ReleaseSettings());
        }

        private IDictionary<string, string?> ProjectWideSettings(AssemblyInformation assemblyInformation,
                                                                 RepositoryInformation repositoryInformation)
        {
            var isLibrary = assemblyInformation.AssemblyName.EndsWith(".Test");

            var dict = new Dictionary<string, string?>
                       {
                           // development
                           ["TargetFramework"] = isLibrary
                                                     ? "netcoreapp3.0"
                                                     : "netstandard2.0",
                           ["LangVersion"] = "latest",
                           ["Nullable"] = "enable",
                           // project identity
                           ["AssemblyName"] = assemblyInformation.ToString(),
                           ["RootNamespace"] = assemblyInformation.ToString(),
                           // nuget
                           ["IsPackable"] = isLibrary
                                                ? "false"
                                                : "true",
                           ["Title"] = assemblyInformation.ToString(),
                           ["Authors"] = AssemblyInformation.SpaceEngineers,
                           ["Company"] = AssemblyInformation.SpaceEngineers,
                           ["PackageDescription"] = assemblyInformation.ToString(),
                           ["RepositoryType"] = repositoryInformation.RepositoryType.ToLowerInvariant(),
                           ["RepositoryUrl"] = repositoryInformation.ToString(),
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