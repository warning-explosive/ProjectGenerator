namespace SpaceEngineers.ProjectGenerator.Infos
{
    using Enumerations;

    public class AssemblyInfo
    {
        internal const string SpaceEngineers = nameof(SpaceEngineers);

        internal AssemblyInfo(ProjectInfo projectInfo, EnProjectType projectType)
        {
            AssemblyName = projectInfo.ProjectName;
            SolutionName = projectInfo.SolutionName;
            ProjectType = projectType;
        }

        internal string AssemblyName { get; }

        internal EnProjectType ProjectType { get;  }

        private string SolutionName { get; }

        public override string ToString()
        {
            return $"{SpaceEngineers}.{SolutionName}.{AssemblyName}";
        }
    }
}