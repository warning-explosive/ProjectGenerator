namespace SpaceEngineers.ProjectGenerator.Csproj
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;
    using Core.Extensions;

    [Lifestyle(EnLifestyle.Singleton)]
    internal class CsprojGenerator : ISettingsGenerator
    {
        private readonly ICsprojSettingsProvider _csprojSettingsProvider;
        
        public CsprojGenerator(ICsprojSettingsProvider csprojSettingsProvider)
        {
            _csprojSettingsProvider = csprojSettingsProvider;
        }
            
        public void Generate(MasterInformation masterInformation)
        {
            var projectSettings = _csprojSettingsProvider.GenerateProjectSettings(masterInformation);
            
            GenerateInternal(masterInformation, projectSettings);
        }

        private void GenerateInternal(MasterInformation masterInformation, CsprojSettings csprojSettings)
        {
            XDocument? backup = null;
            XDocument? document = null;
            
            try
            {
                backup = ReadDocument(masterInformation.ProjectInfo.CsprojPath);
                document = ReadDocument(masterInformation.ProjectInfo.CsprojPath);

                ClearFile(masterInformation.ProjectInfo.CsprojPath);

                ClearDocument(document);

                FillDocument(document, csprojSettings);

                WriteDocumentToFile(masterInformation.ProjectInfo.CsprojPath, document);
            }
            catch (Exception ex)
            {
                document = null;
                
                Console.WriteLine("Something goes wrong");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                throw;
            }
            finally
            {
                if (backup != null && document == null)
                {
                    WriteDocumentToFile(masterInformation.ProjectInfo.CsprojPath, backup);
                }
            }
        }

        private static XDocument ReadDocument(string csprojPath)
        {
            XDocument document;

            using (var projectFile = File.OpenRead(csprojPath))
            {
                document = XDocument.Load(projectFile);
            }

            return document;
        }

        private void ClearFile(string csprojPath)
        {
            File.WriteAllText(csprojPath, string.Empty);
        }

        private static void ClearDocument(XDocument document)
        {
            if (document.Root == null)
            {
                throw new ArgumentNullException(nameof(document.Root) + " node is not exist");
            }
            
            Debug.Assert(document.Root.Name == Constants.Project, nameof(document.Root) + " node is not <Project/> node");

            var query = document.Root
                                .Nodes()
                                .OfType<XElement>()
                                .Where(z => z.Name == Constants.PropertyGroup);

            while (query.Any())
            {
                query.Each(z =>
                           {
                               z.RemoveNodes();
                               z.RemoveAll();
                               z.Remove();
                           });
            }
        }

        private void FillDocument(XDocument document, CsprojSettings csprojSettings)
        {
            document.Root.AddFirst(csprojSettings.ProjectWideGroup, csprojSettings.DebugGroup, csprojSettings.ReleaseGroup);
        }

        private static void WriteDocumentToFile(string csprojPath, XDocument document)
        {
            using (var projectFile = File.OpenWrite(csprojPath))
            { 
                document.Save(projectFile, SaveOptions.None);
            }
        }
    }
}