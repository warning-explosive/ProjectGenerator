namespace SpaceEngineers.ProjectGenerator
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class CsprojSettings
    {
        private const string Mode = "'$(Configuration)|$(Platform)'=='{0}|AnyCPU'";
        
        public CsprojSettings(IDictionary<string, string?> projectWideSettings,
                              IDictionary<string, string?> debugSettings,
                              IDictionary<string, string?> releaseSettings)
        {
            ProjectWideGroup = CreatePropertyGroup(projectWideSettings);
            
            DebugGroup = CreatePropertyGroup(debugSettings);
            DebugGroup?.Add(new XAttribute(Constants.Condition, string.Format(Mode, Constants.Debug)));
                
            ReleaseGroup = CreatePropertyGroup(releaseSettings);
            ReleaseGroup?.Add(new XAttribute(Constants.Condition, string.Format(Mode, Constants.Release)));
        }

        public XElement? ProjectWideGroup { get; }
        
        public XElement? DebugGroup { get; }
        
        public XElement? ReleaseGroup { get; }

        private XElement? CreatePropertyGroup(IDictionary<string, string?> values)
        {
            return values.Any()
                       ? new XElement(Constants.PropertyGroup, values.Select(z => new XElement(z.Key, z.Value)))
                       : null;
        }
    }
}