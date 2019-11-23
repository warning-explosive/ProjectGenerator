namespace SpaceEngineers.ProjectGenerator
{
    internal class MasterInformation
    {
        internal MasterInformation(ProjectInformation projectInformation,
                                   AssemblyInformation assemblyInformation,
                                   RepositoryInformation repositoryInformation)
        {
            ProjectInformation = projectInformation;
            AssemblyInformation = assemblyInformation;
            RepositoryInformation = repositoryInformation;
        }

        internal ProjectInformation ProjectInformation { get; }

        internal AssemblyInformation AssemblyInformation { get; }
        
        internal RepositoryInformation RepositoryInformation { get; }
    }
}