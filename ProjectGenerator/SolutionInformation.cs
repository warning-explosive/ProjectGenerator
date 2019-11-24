namespace SpaceEngineers.ProjectGenerator
{
    internal class SolutionInformation
    {
        internal SolutionInformation(string solutionFolder,
                                     string solutionName,
                                     ProjectInformation[] projectsInfo,
                                     RepositoryInformation repositoryInfo)
        {
            SolutionFolder = solutionFolder;
            SolutionName = solutionName;
            
            ProjectsInfo = projectsInfo;
            RepositoryInfo = repositoryInfo;
        }

        internal string SolutionFolder { get;  }

        internal string SolutionName { get; }

        internal ProjectInformation[] ProjectsInfo { get; }
        
        internal RepositoryInformation RepositoryInfo { get; }
    }
}