using System;
using System.Collections.Generic;
using System.Reflection;
using Valcon.Registration.Graph;

namespace Valcon.Registration.Dsl
{
    public class ValidationOptions : IValidationOptions, IConfigurationAction
    {
        private readonly Func<PropertyInfo, bool> _predicate;
        private readonly IList<Type> _rulesToAdd;

        public ValidationOptions(Func<PropertyInfo, bool> predicate)
        {
            _predicate = predicate;
            _rulesToAdd = new List<Type>();
        }

        public IValidationOptions AddRule(Type ruleType)
        {
            _rulesToAdd.Add(ruleType);
            return this;
        }

        public void Configure(ValidationGraph graph)
        {
            graph
                .Chains
                .Each(chain => chain
                                   .ModelType
                                   .PropertiesWhere(_predicate)
                                   .Each(p => _rulesToAdd.Each(
                                       ruleType =>
                                       chain.AddCall(new ValidationCall(ruleType,
                                                                        new Accessor(chain.ModelType, p))))));
        }
    }
}