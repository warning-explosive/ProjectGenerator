namespace SpaceEngineers.ProjectGenerator
{
    internal class ProjectInformation
    {
        internal ProjectInformation(string projectName,
                                    string csprojPath, 
                                    AssemblyInformation assemblyInfo)
        {
            ProjectName = projectName;
            CsprojPath = csprojPath;
            AssemblyInfo = assemblyInfo;
        }

        internal string ProjectName { get; }
        
        internal string CsprojPath { get; }

        internal AssemblyInformation AssemblyInfo { get; }
    }
}