namespace SpaceEngineers.ProjectGenerator
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <inheritdoc />
    internal abstract class SolutionSettingsGeneratorBase : ISettingsGenerator
    {
        public IEnumerable<Task> Generate(SolutionInformation solutionInfo)
        {
            return new[] { GenerateInternal(solutionInfo) };
        }

        protected abstract Task GenerateInternal(SolutionInformation solutionInfo);
    }
}