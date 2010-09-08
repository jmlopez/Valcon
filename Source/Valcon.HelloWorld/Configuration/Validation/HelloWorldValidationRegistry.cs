using StructureMap;
using Valcon.HelloWorld.Models;

namespace Valcon.HelloWorld.Configuration.Validation
{
    public class HelloWorldValidationRegistry : ValidationRegistry
    {
        public HelloWorldValidationRegistry()
        {
            AppliesTo
                .ToThisAssembly();

            Models
                .IncludeTypes(t => t.Namespace.StartsWith(typeof(ModelMarker).Namespace))
                .Exclude<ModelMarker>();

            this.UseValidationAttributes();
            this.InheritValidationRules();

            Rules
                .IfProperty(p => p.Name.ToLower().Contains("email"), validate => validate.AsEmail())
                .IfProperty(p => p.Name.ToLower().Contains("phone"), validate => validate.AsPhoneNumber())
                .BuildDependenciesWith(ObjectFactory.GetInstance);

            Extensions
                .IncludedExtensionsInNamespaceContaining<HelloWorldValidationRegistry>();
        }
    }
}