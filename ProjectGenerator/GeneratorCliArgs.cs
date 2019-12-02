namespace SpaceEngineers.ProjectGenerator
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Core.Basics;

    internal class GeneratorCliArgs
    {
        private GeneratorCliArgs(string solutionFolder)
        {
            SolutionFolder = solutionFolder;
        }

        internal string SolutionFolder { get; }

        internal static GeneratorCliArgs FromUnsafe(UnsafeGeneratorCliArgs unsafeArgs)
        {
            unsafeArgs.ToPropertyDictionary()
                      .Where(prop => prop.Value == null)
                      .Each(prop => throw new ArgumentNullException(prop.Key));

            Debug.Assert(unsafeArgs.SolutionFolder != null);
            
            return new GeneratorCliArgs(unsafeArgs.SolutionFolder ?? string.Empty);
        }
    }
}