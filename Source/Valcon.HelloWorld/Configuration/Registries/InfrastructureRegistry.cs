using StructureMap.Configuration.DSL;
using Valcon.HelloWorld.Infrastructure;

namespace Valcon.HelloWorld.Configuration.Registries
{
    public class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.IncludeNamespaceContainingType<IUnitOfWork>();
                         x.WithDefaultConventions();
                     });
        }
    }
}