namespace SpaceEngineers.ProjectGenerator
{
    using System.Collections.Generic;
    using Core.CompositionRoot.Abstractions;

    public interface IMasterInfoProvider : IResolvable
    {
        IEnumerable<MasterInfo> GetMasterInfos(GeneratorCliArgs generatorCliArgs);
    }
}