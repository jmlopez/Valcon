using Valcon.Registration.Dsl;

namespace Valcon.Tests.Dsl
{
    public class CoreRegistry : ValidationRegistry
    {
        public CoreRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.IncludeNamespaceContainingType<CoreRegistry>();
                x.ExcludeType<CoreRegistry>();
                x.LookForRegistries();
            });
        }
    }

    public class MyValidationRegistry : ValidationRegistry
    {
        public MyValidationRegistry()
        {
            For<ClassToValidate>()
                .Require(c => c.SimpleRequiredField);
        }
    }

    public class MyOtherValidationRegistry : ValidationRegistry
    {
        public MyOtherValidationRegistry()
        {
            For<ClassToValidate>()
                .Require(c => c.AnotherSimpleRequiredField);
        }
    }

    public class MyAttributeValidationRegistry : ValidationRegistry
    {
        public MyAttributeValidationRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.IncludeNamespaceContainingType<ClassToValidate>();
                         x.UseValidationAttributes();
                     });
        }
    }
}