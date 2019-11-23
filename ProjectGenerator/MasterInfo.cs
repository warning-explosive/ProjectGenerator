namespace SpaceEngineers.ProjectGenerator
{
    public class MasterInfo
    {
        public MasterInfo(ProjectInfo projectInfo, AssemblyInfo assemblyInfo, RepositoryInfo repositoryInfo)
        {
            ProjectInfo = projectInfo;
            AssemblyInfo = assemblyInfo;
            RepositoryInfo = repositoryInfo;
        }

        public ProjectInfo ProjectInfo { get; }

        public AssemblyInfo AssemblyInfo { get; }
        
        public RepositoryInfo RepositoryInfo { get; }
    }
}