namespace SpaceEngineers.ProjectGenerator.AssemblyInfo
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
    internal class AssemblyInfoGenerator : ISettingsGenerator
    {
        private readonly Encoding _encoding = new UTF8Encoding(true);

        private const string AssemblyVersion = nameof(AssemblyVersion);
        private const string AssemblyFileVersion = nameof(AssemblyFileVersion);
        private const string AssemblyInformationalVersion = nameof(AssemblyInformationalVersion);
        
        private readonly string _template = 
$@"using System.Reflection;

[assembly: {AssemblyVersion}(""1.0.0.0"")]
[assembly: {AssemblyFileVersion}(""1.0.0.0"")]
[assembly: {AssemblyInformationalVersion}(""1.0.0.0"")]";
        
        private readonly Func<string, Regex> _getPattern = key => new Regex($@"{key}\(\""(\d)+.(\d)+.(\d)+.(.)+""\)");
        private readonly Func<string, string> _getReplacement = key => $"{key}(\"1.0.0.0\")";
        
        public async Task Generate(MasterInformation masterInformation)
        {
            var projectPath = Path.GetDirectoryName(masterInformation.ProjectInfo.CsprojPath) ?? string.Empty;
            var projectPropertiesPath = Path.Combine(projectPath, "Properties");

            if (!Directory.Exists(projectPropertiesPath))
            {
                Directory.CreateDirectory(projectPropertiesPath);
            }

            var assemblyInfoPath = Path.Combine(projectPropertiesPath, "AssemblyInfo.cs");

            if (!File.Exists(assemblyInfoPath))
            {
                Console.WriteLine("Create new AssemblyInfo.cs");
                
                File.WriteAllText(assemblyInfoPath, _template, _encoding);
            }
            else
            {
                Console.WriteLine("Patch existing AssemblyInfo.cs");
                
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
            text = Replace(text, AssemblyVersion);
            text = Replace(text, AssemblyFileVersion);
            text = Replace(text, AssemblyInformationalVersion);

            return text;
        }

        private string Replace(string text, string key)
        {
            return _getPattern(key).Replace(text, _getReplacement(key));
        }
    }
}