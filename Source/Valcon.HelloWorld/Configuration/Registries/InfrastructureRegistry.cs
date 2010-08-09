using StructureMap.Configuration.DSL;
using Valcon.HelloWorld.Infrastructure;

namespace Valcon.HelloWorld.Configuration.Registries
{
    public class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            For<IValidationProvider>().Use(() => Validator.ValidationProvider);
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.IncludeNamespaceContainingType<IUnitOfWork>();
                         x.WithDefaultConventions();
                     });
        }
    }
}