using Valcon.Registration.Dsl;

namespace Valcon.HelloWorld.Configuration.Validation
{
    public class CoreValidationRegistry : ValidationRegistry
    {
        public CoreValidationRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.IncludeNamespaceContainingType<CoreValidationRegistry>();
                x.ExcludeType<CoreValidationRegistry>();
                x.LookForRegistries();
            });
        }
    }
}