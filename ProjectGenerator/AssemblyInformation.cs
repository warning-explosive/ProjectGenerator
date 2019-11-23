namespace SpaceEngineers.ProjectGenerator
{
    internal class AssemblyInformation
    {
        internal const string SpaceEngineers = nameof(SpaceEngineers);

        internal AssemblyInformation(ProjectInformation projectInformation)
        {
            AssemblyName = projectInformation.ProjectName;
            SolutionName = projectInformation.SolutionName;
        }

        internal string AssemblyName { get; }

        private string SolutionName { get; }

        public override string ToString()
        {
            return $"{SpaceEngineers}.{SolutionName}.{AssemblyName}";
        }
    }
}