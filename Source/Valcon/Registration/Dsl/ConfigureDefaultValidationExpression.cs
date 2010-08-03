using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Valcon.Registration.Graph;

namespace Valcon.Registration.Dsl
{
    public class ConfigureDefaultValidationExpression
    {
        private readonly List<DefaultValidationExpression> _defaultExpressions;
        private readonly List<Action<Accessor, ValidationRegistry>> _alterations;
        public ConfigureDefaultValidationExpression()
        {
            _defaultExpressions = new List<DefaultValidationExpression>();
            _alterations = new List<Action<Accessor, ValidationRegistry>>();
        }

        public IDefaultValidationExpression IfProperty(Func<PropertyInfo, bool> predicate)
        {
            var expression = new DefaultValidationExpression(this, predicate);
            _defaultExpressions.Add(expression);
            return expression;
        }

        public void AddAlteration(Action<Accessor, ValidationRegistry> alteration)
        {
            _alterations.Add(alteration);
        }

        public void ConfigureRegistry(Type type, PropertyInfo propertyInfo, ValidationRegistry registry)
        {
            _defaultExpressions
                .Where(e => e.Matches(propertyInfo))
                .Each(e => _alterations.Each(action => action(new Accessor(type, propertyInfo), registry)));
        }
    }
}