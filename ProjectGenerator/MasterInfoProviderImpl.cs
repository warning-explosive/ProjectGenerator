namespace SpaceEngineers.ProjectGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;
    using Core.Extensions;

    [Lifestyle(EnLifestyle.Singleton)]
    public class MasterInfoProviderImpl : IMasterInfoProvider
    {
        public IEnumerable<MasterInfo> GetMasterInfos(GeneratorCliArgs generatorCliArgs)
        {
            var solutionName = GetSolutionName(generatorCliArgs);
            Console.WriteLine(solutionName.ShowVariable(nameof(solutionName)));

            var repositoryInfo = new RepositoryInfo(Constants.Git, solutionName);

            var projectInfos = GetProjects(solutionName, generatorCliArgs).ToArray();
            projectInfos.Each(p => Console.WriteLine(p.ProjectName.ShowVariable(nameof(p.ProjectName))));

            return projectInfos.Select(projectInfo => new MasterInfo(projectInfo,
                                                                     new AssemblyInfo(projectInfo),
                                                                     repositoryInfo));
        }

        private static string GetSolutionName(GeneratorCliArgs generatorCliArgs)
        {
            var solutionFile = Directory.GetFiles(generatorCliArgs.SolutionFolder, Constants.SlnExtension, SearchOption.TopDirectoryOnly)
                                        .SingleOrDefault();

            Debug.Assert(solutionFile != null, $"Solution file is not found in {generatorCliArgs.SolutionFolder}");

            var solution = Path.GetFileNameWithoutExtension(solutionFile);

            return solution;
        }

        private static IEnumerable<ProjectInfo> GetProjects(string solutionName, GeneratorCliArgs generatorCliArgs)
        {
            var projectFilesPaths = Directory.GetFiles(generatorCliArgs.SolutionFolder, Constants.CsprojExtension, SearchOption.AllDirectories);

            Debug.Assert(projectFilesPaths.Any(), $"Not found any project in {generatorCliArgs.SolutionFolder} or its subdirectories");

            return projectFilesPaths.Select(csprojPath => new ProjectInfo(solutionName, Path.GetFileNameWithoutExtension(csprojPath), csprojPath));
        }
        
    }
}