namespace SpaceEngineers.ProjectGenerator
{
    using System.Collections.Generic;

    public class ProjectSettings
    {
        public ProjectSettings(IDictionary<string, string?> projectWideSettings,
                     IDictionary<string, string?> debugSettings,
                     IDictionary<string, string?> releaseSettings)
        {
            ProjectWideSettings = projectWideSettings;
            DebugSettings = debugSettings;
            ReleaseSettings = releaseSettings;
        }

        private IDictionary<string, string?> ProjectWideSettings { get; }
        
        private IDictionary<string, string?> DebugSettings { get; }
        
        private IDictionary<string, string?> ReleaseSettings { get; }
    }
}