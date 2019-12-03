namespace SpaceEngineers.ProjectGenerator
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    internal abstract class SolutionConfigurationFileGeneratorBase : SolutionSettingsGeneratorBase
    {
        protected abstract string FileName { get; }

        protected abstract string Content(SolutionInformation solutionInfo);

        protected override Task GenerateInternalAsync(SolutionInformation solutionInfo)
        {
            var gitignorePath = Path.Combine(solutionInfo.SolutionFolder, FileName);
            
            Console.WriteLine($"\tGenerate {FileName}");

            return Task.Factory.StartNew(() => File.WriteAllText(gitignorePath, Content(solutionInfo), Encoding));
        }
    }
}