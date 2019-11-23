namespace SpaceEngineers.ProjectGenerator
{
    internal class ProjectInfo
    {
        internal ProjectInfo(string solutionName, string projectName, string csprojPath)
        {
            SolutionName = solutionName;
            ProjectName = projectName;
            CsprojPath = csprojPath;
        }

        internal string SolutionName { get; }

        internal string ProjectName { get; }
        
        internal string CsprojPath { get; }
    }
}