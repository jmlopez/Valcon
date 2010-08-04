using StructureMap.Configuration.DSL;
using Valcon.HelloWorld.Repositories;

namespace Valcon.HelloWorld.Configuration.Registries
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.IncludeNamespaceContainingType<IUserRepository>();
                         x.WithDefaultConventions();
                     });
        }
    }
}