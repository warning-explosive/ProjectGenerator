namespace SpaceEngineers.ProjectGenerator
{
    internal class MasterInformation
    {
        internal MasterInformation(ProjectInformation projectInfo,
                                   AssemblyInformation assemblyInfo,
                                   RepositoryInformation repositoryInfo)
        {
            ProjectInfo = projectInfo;
            AssemblyInfo = assemblyInfo;
            RepositoryInfo = repositoryInfo;
        }

        internal ProjectInformation ProjectInfo { get; }

        internal AssemblyInformation AssemblyInfo { get; }
        
        internal RepositoryInformation RepositoryInfo { get; }
    }
}