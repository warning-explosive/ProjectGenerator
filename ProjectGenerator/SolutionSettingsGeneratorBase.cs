namespace SpaceEngineers.ProjectGenerator
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <inheritdoc />
    internal abstract class SolutionSettingsGeneratorBase : ISettingsGenerator
    {
        /// <summary> Encoding </summary>
        protected readonly Encoding Encoding = new UTF8Encoding(true);
        
        /// <inheritdoc />
        public IEnumerable<Task> Generate(SolutionInformation solutionInfo)
        {
            yield return GenerateInternalAsync(solutionInfo);
        }

        /// <summary> GenerateInternal </summary>
        /// <param name="solutionInfo">SolutionInformation</param>
        /// <returns>Async operation with configuration file</returns>
        protected abstract Task GenerateInternalAsync(SolutionInformation solutionInfo);
    }
}