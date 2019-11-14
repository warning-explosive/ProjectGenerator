namespace SpaceEngineers.ProjectGenerator.Infos
{
    using System;
    using Enumerations;

    public class RepositoryInfo
    {
        internal static Uri RepositoryUrl = new Uri("https://github.com/warning-explosive");

        public RepositoryInfo(EnRepositoryType repositoryType, string solutionName)
        {
            RepositoryType = repositoryType;
            SolutionName = solutionName;
        }

        internal EnRepositoryType RepositoryType { get; }

        private string SolutionName { get; }

        public override string ToString()
        {
            return $"{RepositoryUrl}/{SolutionName}";
        }
    }
}