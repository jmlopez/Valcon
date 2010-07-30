using System;
using System.Reflection;
using Valcon.Registration.Dsl;
using Valcon.Rules;

namespace Valcon.Conventions
{
    public class ValidationAttributeConvention : IRegistrationConvention
    {
        public void Process(Type type, ValidationRegistry registry)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            properties.Each(p =>
                                {
                                    var validationAttributes = p.GetValidationAttributes();
                                    foreach (var validationAttribute in validationAttributes)
                                    {
                                        var name = validationAttribute.GetType().Name.Replace("Attribute", string.Empty);
                                        var rule = Rule.For(type, p, name);
                                        registry.For(type).AddRule(rule);
                                    }
                                });
        }
    }
}