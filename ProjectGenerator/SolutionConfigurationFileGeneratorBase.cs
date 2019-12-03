namespace SpaceEngineers.ProjectGenerator
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Core.Basics;

    internal abstract class SolutionConfigurationFileGeneratorBase : SolutionSettingsGeneratorBase
    {
        protected abstract string FileName { get; }
        
        protected abstract string Content { get; }

        protected override async Task GenerateInternalAsync(SolutionInformation solutionInfo)
        {
            var gitignorePath = Path.Combine(solutionInfo.SolutionFolder, FileName);
            
            Console.WriteLine($"\tGenerate {FileName}");

            if (File.Exists(gitignorePath))
            {
                using (var gitignore = File.OpenWrite(gitignorePath))
                {
                    await gitignore.OverWriteAllAsync(Content, Encoding);
                }
            }
            else
            {
                File.WriteAllText(gitignorePath, Content, Encoding);
            }
        }
    }
}