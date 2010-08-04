using Valcon.HelloWorld.Models;
using Valcon.Registration.Dsl;

namespace Valcon.HelloWorld.Configuration.Validation
{
    public class BasicValidationRegistry : ValidationRegistry
    {
        public BasicValidationRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.IncludeNamespaceContainingType<ModelMarker>();
                         x.UseValidationAttributes();
                         x.InheritValidationRules();

                         x.ByDefault
                             .IfProperty(p => p.Name.ToLower().Contains("email"))
                             .IsEmail();

                         x.ByDefault
                             .IfProperty(p => p.Name.ToLower().Contains("phone"))
                             .IsPhoneNumber();
                     });
        }
    }
}