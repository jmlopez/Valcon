using Valcon.Conventions;

namespace Valcon.Registration.Dsl
{
    public static class DefaultConventionExtensions
    {
        public static void LookForRegistries(this IAssemblyScanner scanner)
        {
            scanner.Convention<FindRegistriesConvention>();
        }

        public static void InheritValidationRules(this IAssemblyScanner scanner)
        {
            scanner.ModifyGraphAfterScan<InheritValidationRulesModifer>();
        }

        public static void UseValidationAttributes(this IAssemblyScanner scanner)
        {
            scanner.Convention<ValidationAttributeConvention>();
        }
    }
}