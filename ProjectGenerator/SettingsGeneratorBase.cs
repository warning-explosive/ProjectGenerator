namespace SpaceEngineers.ProjectGenerator
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal abstract class SettingsGeneratorBase : ISettingsGenerator
    {
        public IEnumerable<Task> Generate(SolutionInformation solutionInfo)
        {
            foreach (var projectInfo in solutionInfo.ProjectsInfo)
            {
                yield return GenerateInternal(projectInfo, solutionInfo);
            }
        }

        protected abstract Task GenerateInternal(ProjectInformation projectInfo, SolutionInformation solutionInfo);
    }
}