namespace SpaceEngineers.ProjectGenerator
{
    using System.Collections.Generic;
    using Core.CompositionRoot.Abstractions;

    internal interface IMasterInfoProvider : IResolvable
    {
        IEnumerable<MasterInformation> GetMasterInfos(GeneratorCliArgs generatorCliArgs);
    }
}