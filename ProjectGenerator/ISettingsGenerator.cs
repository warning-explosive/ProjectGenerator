namespace SpaceEngineers.ProjectGenerator
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.CompositionRoot.Abstractions;

    internal interface ISettingsGenerator : ICollectionResolvable
    {
        IEnumerable<Task> Generate(SolutionInformation solutionInformation);
    }
}