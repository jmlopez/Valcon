using System;
using System.Collections.Generic;
using System.Reflection;

namespace Valcon.Registration.Dsl
{
    public class RulesExpression
    {
        private readonly IList<IConfigurationAction> _policies;

        public RulesExpression(IList<IConfigurationAction> policies)
        {
            _policies = policies;
        }

        public RulesExpression IfProperty(Func<PropertyInfo, bool> predicate, Action<IValidationOptions> configure)
        {
            var options = new ValidationOptions(predicate);
            configure(options);

            _policies.Add(options);

            return this;
        }

        public RulesExpression BuildDependenciesWith(Func<Type, object> serviceLocator)
        {
            _policies.AddAction(graph => graph.ServiceLocator = serviceLocator);
            return this;
        }

        public ValidationByTypeExpression<T> For<T>()
            where T : class
        {
            return new ValidationByTypeExpression<T>(_policies);
        }
    }
}