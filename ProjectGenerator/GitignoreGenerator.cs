namespace SpaceEngineers.ProjectGenerator
{
    using System;
    using System.IO;
    using System.Text;
    using Core.Basics;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;

    /// <inheritdoc />
    [Lifestyle(EnLifestyle.Singleton)]
    internal class GitignoreGenerator : SolutionSettingsGeneratorBase
    {
        private const string FileName = ".gitignore";
        
        private const string Content = 
@"
###########
# Folders #
###########
.hg/
.vs/
.idea/
[Bb]in/
[Oo]bj/
[Pp]ackages/
*.Caches/

##############
# Extensions #
##############

*.user
*.orig
";
        
        /// <inheritdoc />
        protected override async void GenerateInternal(SolutionInformation solutionInfo)
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