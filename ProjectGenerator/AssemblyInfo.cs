namespace SpaceEngineers.ProjectGenerator
{
    public class AssemblyInfo
    {
        internal const string SpaceEngineers = nameof(SpaceEngineers);

        internal AssemblyInfo(ProjectInfo projectInfo)
        {
            AssemblyName = projectInfo.ProjectName;
            SolutionName = projectInfo.SolutionName;
        }

        internal string AssemblyName { get; }

        private string SolutionName { get; }

        public override string ToString()
        {
            return $"{SpaceEngineers}.{SolutionName}.{AssemblyName}";
        }
    }
}