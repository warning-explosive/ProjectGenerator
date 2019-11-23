namespace SpaceEngineers.ProjectGenerator.Csproj
{
    using Core.CompositionRoot.Abstractions;

    internal interface ICsprojSettingsProvider : IResolvable
    {
        CsprojSettings GenerateProjectSettings(MasterInfo masterInfo);
    }
}