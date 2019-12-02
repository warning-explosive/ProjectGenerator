namespace SpaceEngineers.ProjectGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;
    using Core.Basics;

    [Lifestyle(EnLifestyle.Singleton)]
    internal class MasterInfoProviderImpl : IMasterInfoProvider
    {
        public SolutionInformation GetSolutionInfo(GeneratorCliArgs generatorCliArgs)
        {
            var solutionName = GetSolutionName(generatorCliArgs);
            
            var repositoryInfo = new RepositoryInformation(Constants.Git, solutionName);

            var projectInfos = GetProjects(solutionName, generatorCliArgs).ToArray();

            return new SolutionInformation(generatorCliArgs.SolutionFolder,
                                           solutionName,
                                           projectInfos,
                                           repositoryInfo);
        }

        private static string GetSolutionName(GeneratorCliArgs generatorCliArgs)
        {
            var solutionFile = Directory.GetFiles(generatorCliArgs.SolutionFolder, Constants.SlnExtension, SearchOption.TopDirectoryOnly)
                                        .SingleOrDefault();

            Debug.Assert(solutionFile != null, $"Solution file is not found in {generatorCliArgs.SolutionFolder}");

            var solution = Path.GetFileNameWithoutExtension(solutionFile);

            return solution;
        }

        private static IEnumerable<ProjectInformation> GetProjects(string solutionName, GeneratorCliArgs generatorCliArgs)
        {
            var projectFilesPaths = Directory.GetFiles(generatorCliArgs.SolutionFolder, Constants.CsprojExtension, SearchOption.AllDirectories);

            Debug.Assert(projectFilesPaths.Any(), $"Not found any project in {generatorCliArgs.SolutionFolder} or its subdirectories");

            return projectFilesPaths.Select(csprojPath =>
                                            {
                                                var projectName = Path.GetFileNameWithoutExtension(csprojPath);
                                                
                                                return new ProjectInformation(projectName,
                                                                              csprojPath,
                                                                              new AssemblyInformation(projectName, solutionName));
                                            });
        }
        
    }
}