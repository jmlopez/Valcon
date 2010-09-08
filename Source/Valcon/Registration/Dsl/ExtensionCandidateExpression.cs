namespace Valcon.Registration.Dsl
{
    public class ExtensionCandidateExpression
    {
        private readonly TypePool _types;
        private readonly ITypeMatcher _matcher;

        public ExtensionCandidateExpression(TypePool types, ITypeMatcher matcher)
        {
            _types = types;
            _matcher = matcher;

            _matcher.TypeFilters.Excludes += (type => !typeof (IValidationRegistryExtension).IsAssignableFrom(type));
        }

        public ExtensionCandidateExpression IncludedExtensionsInNamespaceContaining<T>()
        {
            _matcher.TypeFilters.Includes += (type => type.Namespace == typeof(T).Namespace);
            return this;
        }
    }
}