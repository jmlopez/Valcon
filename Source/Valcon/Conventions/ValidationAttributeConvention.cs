using System;
using Valcon.Registration.Dsl;
using Valcon.Registration.Graph;

namespace Valcon.Conventions
{
    public class ValidationAttributeConvention : IRegistrationConvention
    {
        public void Process(Type type, ValidationRegistry registry)
        {
            var properties = type.GetPublicProperties();
            properties.Each(p => p.GetValidationAttributes().Each(attribute => registry
                                                                                   .For(type)
                                                                                   .AddCall(new ValidationCall(attribute.RuleType, new Accessor(type, p)))));
        }
    }
}