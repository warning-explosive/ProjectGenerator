namespace SpaceEngineers.ProjectGenerator
{
    using Core.CompositionRoot.Abstractions;

    internal interface IMasterInfoProvider : IResolvable
    {
        SolutionInformation GetSolutionInfo(GeneratorCliArgs generatorCliArgs);
    }
}