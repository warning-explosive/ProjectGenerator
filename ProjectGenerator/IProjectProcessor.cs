namespace SpaceEngineers.ProjectGenerator
{
    using Core.CompositionRoot.Abstractions;
    using Infos;

    public interface IProjectProcessor : IResolvable
    {
        void Process(MasterInfo masterInfo, ProjectSettings projectSettings);
    }
}