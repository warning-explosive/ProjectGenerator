namespace SpaceEngineers.ProjectGenerator
{
    using Core.CompositionRoot.Abstractions;
    using Infos;

    public interface IProjectSettingsProvider : IResolvable
    {
        ProjectSettings GenerateProjectSettings(MasterInfo masterInfo, GeneratorCliArgs generatorCliArgs);
    }
}