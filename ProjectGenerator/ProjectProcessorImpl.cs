namespace SpaceEngineers.ProjectGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Core.CompositionRoot.Attributes;
    using Core.CompositionRoot.Enumerations;
    using Core.Extensions;
    using Infos;

    [Lifestyle(EnLifestyle.Singleton)]
    public class ProjectProcessorImpl : IProjectProcessor
    {
        public void Process(MasterInfo masterInfo, ProjectSettings projectSettings)
        {
            XDocument? backup = null;
            XDocument? document = null;
            
            try
            {
                backup = ReadDocument(masterInfo.ProjectInfo.CsprojPath);
                document = ReadDocument(masterInfo.ProjectInfo.CsprojPath);

                ClearFile(masterInfo.ProjectInfo.CsprojPath);

                ClearDocument(document);

                FillDocument(document, projectSettings);

                WriteDocumentToFile(masterInfo.ProjectInfo.CsprojPath, document);
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
                    WriteDocumentToFile(masterInfo.ProjectInfo.CsprojPath, backup);
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

        private void FillDocument(XDocument document, ProjectSettings projectSettings)
        {
            document.Root.AddFirst(projectSettings.ProjectWideGroup, projectSettings.DebugGroup, projectSettings.ReleaseGroup);
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