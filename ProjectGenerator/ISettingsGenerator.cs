namespace SpaceEngineers.ProjectGenerator
{
    using Core.CompositionRoot.Abstractions;

    public interface ISettingsGenerator : ICollectionResolvable
    {
        void Generate(MasterInfo masterInfo);
    }
}