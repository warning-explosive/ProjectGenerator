namespace SpaceEngineers.ProjectGenerator
{
    using System;

    internal class RepositoryInformation
    {
        internal static readonly Uri RepositoryUrl = new Uri("https://github.com/warning-explosive");

        private readonly string _solutionName;

        internal RepositoryInformation(string repositoryType, string solutionName)
        {
            RepositoryType = repositoryType;
            _solutionName = solutionName;
        }

        internal string RepositoryType { get; }

        public override string ToString()
        {
            return $"{RepositoryUrl}/{_solutionName}";
        }
    }
}