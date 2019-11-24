namespace SpaceEngineers.ProjectGenerator
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;
    using Core.Extensions;

    [Lifestyle(EnLifestyle.Singleton)]
    internal class AssemblyInfoGenerator : SettingsGeneratorBase
    {
        private const string PropertiesFolderName = "Properties";
        private const string AssemblyInfoFile = "AssemblyInfo.cs";
        
        private const string Namespace = "using System.Reflection;";
        private const string AssemblyVersion = nameof(AssemblyVersion);
        private const string AssemblyFileVersion = nameof(AssemblyFileVersion);
        private const string AssemblyInformationalVersion = nameof(AssemblyInformationalVersion);

        private readonly Encoding _encoding = new UTF8Encoding(true);
        
        private readonly Func<string, string> _getReplacement = key => $"{key}(\"1.0.0.0\")";
        private readonly Func<string, string> _getAttribute = replacement => $"[assembly: {replacement}]";

        private string Template =>
$@"{Namespace}

{_getAttribute(_getReplacement(AssemblyVersion))}
{_getAttribute(_getReplacement(AssemblyFileVersion))}
{_getAttribute(_getReplacement(AssemblyInformationalVersion))}";

        private readonly Func<string, Regex> _getPattern = key => new Regex($@"{key}\(\""(\d)+.(\d)+.(\d)+.(.)+""\)");
        
        protected override async Task GenerateInternal(ProjectInformation projectInfo, SolutionInformation solutionInfo)
        {
            var projectPath = Path.GetDirectoryName(projectInfo.CsprojPath) ?? string.Empty;
            var projectPropertiesPath = Path.Combine(projectPath, PropertiesFolderName);

            if (!Directory.Exists(projectPropertiesPath))
            {
                Directory.CreateDirectory(projectPropertiesPath);
            }

            var assemblyInfoPath = Path.Combine(projectPropertiesPath, AssemblyInfoFile);

            if (!File.Exists(assemblyInfoPath))
            {
                Console.WriteLine($"\tCreate new {AssemblyInfoFile}");
                
                File.WriteAllText(assemblyInfoPath, Template, _encoding);
            }
            else
            {
                Console.WriteLine($"\tPatch existing {AssemblyInfoFile}");
                
                using (var file = File.Open(assemblyInfoPath, FileMode.Open, FileAccess.ReadWrite))
                {
                    var text = await file.ReadAllAsync(_encoding);

                    text = PatchExistingAssemblyInfo(text);

                    await file.OverWriteAllAsync(text, _encoding);
                }
            }
        }

        private string PatchExistingAssemblyInfo(string text)
        {
            if (text.Trim(' ', '\n', '\t').Length == 0)
            {
                return Template;
            }

            if (!new Regex(Namespace).IsMatch(text))
            {
                text = new StringBuilder(text).Insert(0, $"{Namespace}\n").ToString();
            }

            text = InsertOrReplace(text, AssemblyVersion);
            text = InsertOrReplace(text, AssemblyFileVersion);
            text = InsertOrReplace(text, AssemblyInformationalVersion);

            return text;
        }

        private string InsertOrReplace(string text, string key)
        {
            var pattern = _getPattern(key);
            var replacement = _getReplacement(key);

            text = pattern.IsMatch(text)
                       ? pattern.Replace(text, replacement)
                       : new StringBuilder(text).Append("\n" + _getAttribute(replacement)).ToString();

            return text;
        }
    }
}