namespace SpaceEngineers.ProjectGenerator
{
    using System.Threading.Tasks;
    using Core.CompositionRoot.Abstractions;

    internal interface ISettingsGenerator : ICollectionResolvable
    {
        Task Generate(MasterInformation masterInformation);
    }
}