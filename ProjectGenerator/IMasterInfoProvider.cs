namespace SpaceEngineers.ProjectGenerator
{
    using System.Collections.Generic;
    using Core.CompositionRoot.Abstractions;

    internal interface IMasterInfoProvider : IResolvable
    {
        IEnumerable<MasterInfo> GetMasterInfos(GeneratorCliArgs generatorCliArgs);
    }
}