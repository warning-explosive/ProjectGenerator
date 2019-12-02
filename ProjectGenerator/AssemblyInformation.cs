namespace SpaceEngineers.ProjectGenerator
{
    internal class AssemblyInformation
    {
        internal const string SpaceEngineers = nameof(SpaceEngineers);

        private readonly string _solutionName;

        private readonly string _projectName;

        internal AssemblyInformation(string projectName, string solutionName)
        {
            _solutionName = solutionName;
            _projectName = projectName;
        }

        public override string ToString()
        {
            return _solutionName == _projectName
                   || _projectName.StartsWith(_solutionName)
                       ? $"{SpaceEngineers}.{_projectName}"
                       : $"{SpaceEngineers}.{_solutionName}.{_projectName}";
        }
    }
}