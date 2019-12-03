namespace SpaceEngineers.ProjectGenerator
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    internal abstract class SolutionSettingsGeneratorBase : ISettingsGenerator
    {
        protected readonly Encoding Encoding = new UTF8Encoding(true);
        
        public IEnumerable<Task> Generate(SolutionInformation solutionInfo)
        {
            yield return GenerateInternalAsync(solutionInfo);
        }

        protected abstract Task GenerateInternalAsync(SolutionInformation solutionInfo);
    }
}