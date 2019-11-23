namespace SpaceEngineers.ProjectGenerator
{
    using Core.CompositionRoot.Abstractions;

    internal interface ISettingsGenerator : ICollectionResolvable
    {
        void Generate(MasterInformation masterInformation);
    }
}