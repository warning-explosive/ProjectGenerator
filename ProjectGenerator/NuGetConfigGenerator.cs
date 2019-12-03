namespace SpaceEngineers.ProjectGenerator
{
    using System.Xml.Linq;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;

    [Lifestyle(EnLifestyle.Singleton)]
    internal class NuGetConfigGenerator : SolutionConfigurationFileGeneratorBase
    {
        protected override string FileName => "NuGet.config";

        protected override string Content(SolutionInformation solutionInfo)
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
                                  new XElement("add",
                                               new XAttribute("key", "disableSourceControlIntegration"),
                                               new XAttribute("value", "true"))),
                     new XElement("packageSources",
                                  new XElement("add",
                                               new XAttribute("key", "nuget.org"),
                                               new XAttribute("value", "https://api.nuget.org/v3/index.json"),
                                               new XAttribute("protocolVersion", "3"))));

            return document.ToString();
        }
    }
}