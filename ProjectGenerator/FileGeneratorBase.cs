namespace SpaceEngineers.ProjectGenerator
{
    using System;
    using System.Threading.Tasks;

    internal abstract class FileGeneratorBase : SettingsGeneratorBase
    {
        protected override Task GenerateInternal(ProjectInformation projectInfo, SolutionInformation solutionInfo)
        {
            throw new NotImplementedException();
        }
    }
}