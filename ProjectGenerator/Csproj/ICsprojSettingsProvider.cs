namespace SpaceEngineers.ProjectGenerator.Csproj
{
    using Core.CompositionRoot.Abstractions;

    internal interface ICsprojSettingsProvider : IResolvable
    {
        CsprojSettings GetProjectSettings(ProjectInformation projectInfo, SolutionInformation solutionInfo);
    }
}