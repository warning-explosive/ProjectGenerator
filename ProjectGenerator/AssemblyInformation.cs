namespace SpaceEngineers.ProjectGenerator
{
    internal class AssemblyInformation
    {
        internal const string SpaceEngineers = nameof(SpaceEngineers);

        private readonly string _projectName;

        private readonly string _solutionName;

        internal AssemblyInformation(ProjectInformation projectInformation)
        {
            _projectName = projectInformation.ProjectName;
            _solutionName = projectInformation.SolutionName;
        }

        public override string ToString()
        {
            return $"{SpaceEngineers}.{_solutionName}.{_projectName}";
        }
    }
}