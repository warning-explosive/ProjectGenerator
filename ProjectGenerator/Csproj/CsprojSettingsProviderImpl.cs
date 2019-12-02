namespace SpaceEngineers.ProjectGenerator.Csproj
{
    using System.Collections.Generic;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;

    [Lifestyle(EnLifestyle.Singleton)]
    internal class CsprojSettingsProviderImpl : ICsprojSettingsProvider
    {
        public CsprojSettings GetProjectSettings(ProjectInformation projectInfo, SolutionInformation solutionInfo)
        {
            return new CsprojSettings(ProjectWideSettings(projectInfo, solutionInfo),
                                      DebugSettings(),
                                      ReleaseSettings());
        }

        private IDictionary<string, string?> ProjectWideSettings(ProjectInformation projectInfo, SolutionInformation solutionInfo)
        {
            var isExecutable = projectInfo.ProjectName.EndsWith(".Executable");
            var isTest = projectInfo.ProjectName.EndsWith(".Test");
            var isApp = isExecutable || isTest;

            var dict = new Dictionary<string, string?>
                       {
                           // development
                           ["TargetFramework"] = isApp
                                                     ? "netcoreapp3.0"
                                                     : "netstandard2.0",
                           ["LangVersion"] = "latest",
                           ["Nullable"] = "enable",
                           // project identity
                           ["AssemblyName"] = projectInfo.AssemblyInfo.ToString(),
                           ["RootNamespace"] = projectInfo.AssemblyInfo.ToString(),
                           // nuget
                           ["IsPackable"] = isApp
                                                ? "false"
                                                : "true",
                           ["Title"] = projectInfo.AssemblyInfo.ToString(),
                           ["Authors"] = AssemblyInformation.SpaceEngineers,
                           ["Company"] = AssemblyInformation.SpaceEngineers,
                           ["PackageDescription"] = projectInfo.AssemblyInfo.ToString(),
                           ["RepositoryType"] = solutionInfo.RepositoryInfo.RepositoryType.ToLowerInvariant(),
                           ["RepositoryUrl"] = solutionInfo.RepositoryInfo.ToString(),
                           ["Copyright"] = "Copyright (c) 2019",
                           // analysis
                           ["RunAnalyzersDuringBuild"] = "true",
                           ["RunAnalyzersDuringLiveAnalysis"] = "true",
                           ["RunAnalyzers"] = "true",
                           // build
                           ["CopyLocalLockFileAssemblies"] = "false",
                           ["GenerateAssemblyInfo"] = "false",
                           ["GeneratePackageOnBuild"] = "false",
                           ["TreatWarningsAsErrors"] = "true",
                           ["AutoGenerateBindingRedirects"] = "true",
                           ["GenerateDocumentationFile"] = isTest
                                                               ? "false"
                                                               : "true",
                           // run-time
                           ["TieredCompilation"] = "true",
                       };

            if (!isApp)
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