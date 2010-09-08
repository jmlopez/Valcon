namespace Valcon.Registration
{
    public class ExtensionTypeMatcher : TypeMatcher
    {
        public ExtensionTypeMatcher()
        {
            TypeFilters.Excludes += (type => !typeof(IValidationRegistryExtension).IsAssignableFrom(type));
        }
    }
}