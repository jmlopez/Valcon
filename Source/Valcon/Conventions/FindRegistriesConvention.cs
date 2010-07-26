using System;
using Valcon.Registration.Dsl;
using Valcon.Registration;

namespace Valcon.Conventions
{
    public class FindRegistriesConvention : IRegistrationConvention
    {
        public void Process(Type type, ValidationRegistry registry)
        {
            if (ValidationRegistry.IsPublicRegistry(type))
            {
                registry.Configure(x => x.ImportRegistry(type));
            }
        }
    }
}