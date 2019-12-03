namespace SpaceEngineers.ProjectGenerator
{
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;

    [Lifestyle(EnLifestyle.Singleton)]
    internal class GitignoreGenerator : SolutionConfigurationFileGeneratorBase
    {
        protected override string FileName => ".gitignore";
        
        protected override string Content =>
@"###########
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
    }
}