namespace SpaceEngineers.ProjectGenerator
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;

    /// <inheritdoc />
    [Lifestyle(EnLifestyle.Singleton)]
    internal class NuGetConfigGenerator : SolutionSettingsGeneratorBase
    {   
        private readonly Encoding _encoding = new UTF8Encoding(true);

        protected override Task GenerateInternal(SolutionInformation solutionInfo)
        {
            return Task.Factory.StartNew(() => ProcessDocument(solutionInfo));
        }

        private void ProcessDocument(SolutionInformation solutionInfo)
        {
            var root = new XElement("configuration");

            var document = new XDocument(root);

            root.Add(new XElement("config",
                                  new XElement("add",
                                               new XAttribute("key", "dependencyVersion"),
                                               new XAttribute("value", "Highest")),
                                  new XElement("add",
                                               new XAttribute("key", "globalPackagesFolder"),
                                               new XAttribute("value", "packages"))),
                     new XElement("packageRestore",
                                  new XElement("add",
                                               new XAttribute("key", "enabled"),
                                               new XAttribute("value", "true")),
                                  new XElement("add",
                                               new XAttribute("key", "automatic"),
                                               new XAttribute("value", "true"))),
                     new XElement("solution",
                                  new XAttribute("key", "disableSourceControlIntegration"),
                                  new XAttribute("value", "true")),
                     new XElement("packageSources",
                                  new XAttribute("key", "nuget.org"),
                                  new XAttribute("value", "https://api.nuget.org/v3/index.json"),
                                  new XAttribute("protocolVersion", "3")));

            var path = Path.Combine(solutionInfo.SolutionFolder, "NuGet.config");

            if (File.Exists(path))
            {
                using (var nugetConfig = File.OpenWrite(path))
                { 
                    document.Save(nugetConfig, SaveOptions.None);
                }
            }
            else
            {
                File.WriteAllText(path, document.ToString(), _encoding);
            }
        }
    }
}