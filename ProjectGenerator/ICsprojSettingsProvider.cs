namespace SpaceEngineers.ProjectGenerator
{
    using Core.CompositionRoot.Abstractions;

    public interface ICsprojSettingsProvider : IResolvable
    {
        CsprojSettings GenerateProjectSettings(MasterInfo masterInfo);
    }
}