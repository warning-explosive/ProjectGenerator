namespace SpaceEngineers.ProjectGenerator
{
    using System;

    internal class RepositoryInfo
    {
        internal static Uri RepositoryUrl = new Uri("https://github.com/warning-explosive");

        internal RepositoryInfo(string repositoryType, string solutionName)
        {
            RepositoryType = repositoryType;
            SolutionName = solutionName;
        }

        internal string RepositoryType { get; }

        private string SolutionName { get; }

        public override string ToString()
        {
            return $"{RepositoryUrl}/{SolutionName}";
        }
    }
}