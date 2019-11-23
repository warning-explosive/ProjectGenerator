namespace SpaceEngineers.ProjectGenerator
{
    internal class MasterInfo
    {
        internal MasterInfo(ProjectInfo projectInfo, AssemblyInfo assemblyInfo, RepositoryInfo repositoryInfo)
        {
            ProjectInfo = projectInfo;
            AssemblyInfo = assemblyInfo;
            RepositoryInfo = repositoryInfo;
        }

        internal ProjectInfo ProjectInfo { get; }

        internal AssemblyInfo AssemblyInfo { get; }
        
        internal RepositoryInfo RepositoryInfo { get; }
    }
}