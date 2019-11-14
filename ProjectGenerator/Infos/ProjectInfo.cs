namespace SpaceEngineers.ProjectGenerator.Infos
{
    public class ProjectInfo
    {
        public ProjectInfo(string solutionName, string projectName, string csprojPath)
        {
            SolutionName = solutionName;
            ProjectName = projectName;
            CsprojPath = csprojPath;
        }

        public string SolutionName { get; }

        public string ProjectName { get; }
        
        public string CsprojPath { get; }
    }
}