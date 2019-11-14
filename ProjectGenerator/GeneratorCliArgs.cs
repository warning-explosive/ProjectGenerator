namespace SpaceEngineers.ProjectGenerator
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Core.Extensions;

    public class GeneratorCliArgs
    {
        private GeneratorCliArgs(string solutionFolder, bool generatePackageOnBuild)
        {
            SolutionFolder = solutionFolder;
            GeneratePackageOnBuild = generatePackageOnBuild;
        }

        internal string SolutionFolder { get; }

        internal bool GeneratePackageOnBuild { get; }
        
        internal static GeneratorCliArgs FromUnsafe(UnsafeGeneratorCliArgs unsafeArgs)
        {
            unsafeArgs.ToPropertyDictionary()
                      .Where(prop => prop.Value == null)
                      .Each(prop => throw new ArgumentNullException(prop.Key));

            Debug.Assert(unsafeArgs.SolutionFolder != null);
            
            return new GeneratorCliArgs(unsafeArgs.SolutionFolder ?? string.Empty, unsafeArgs.GeneratePackageOnBuild);
        }
    }
}