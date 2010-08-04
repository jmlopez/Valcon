using StructureMap.Configuration.DSL;

namespace Valcon.HelloWorld.Configuration.Registries
{
    public class CoreRegistry : Registry
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
}