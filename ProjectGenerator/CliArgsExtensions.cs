namespace SpaceEngineers.ProjectGenerator
{
    internal static class CliArgsExtensions
    {
        internal static GeneratorCliArgs ToSafe(this UnsafeGeneratorCliArgs unsafeCliArgs)
        {
            return GeneratorCliArgs.FromUnsafe(unsafeCliArgs);
        }
    }
}