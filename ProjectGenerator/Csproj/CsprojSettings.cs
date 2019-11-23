namespace SpaceEngineers.ProjectGenerator.Csproj
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    internal class CsprojSettings
    {
        private const string Mode = "'$(Configuration)|$(Platform)'=='{0}|AnyCPU'";
        
        internal CsprojSettings(IDictionary<string, string?> projectWideSettings,
                                IDictionary<string, string?> debugSettings,
                                IDictionary<string, string?> releaseSettings)
        {
            ProjectWideGroup = CreatePropertyGroup(projectWideSettings);
            
            DebugGroup = CreatePropertyGroup(debugSettings);
            DebugGroup?.Add(new XAttribute(Constants.Condition, string.Format(Mode, Constants.Debug)));
                
            ReleaseGroup = CreatePropertyGroup(releaseSettings);
            ReleaseGroup?.Add(new XAttribute(Constants.Condition, string.Format(Mode, Constants.Release)));
        }

        internal XElement? ProjectWideGroup { get; }
        
        internal XElement? DebugGroup { get; }
        
        internal XElement? ReleaseGroup { get; }

        private XElement? CreatePropertyGroup(IDictionary<string, string?> values)
        {
            return values.Any()
                       ? new XElement(Constants.PropertyGroup, values.Select(z => new XElement(z.Key, z.Value)))
                       : null;
        }
    }
}