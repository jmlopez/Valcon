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
        public ConfigureDefaultValidationExpression()
        {
            _defaultExpressions = new List<DefaultValidationExpression>();
        }

        public IDefaultValidationExpression IfProperty(Func<PropertyInfo, bool> predicate)
        {
            var expression = new DefaultValidationExpression(predicate);
            _defaultExpressions.Add(expression);
            return expression;
        }

        public void ConfigureRegistry(Type type, PropertyInfo propertyInfo, ValidationRegistry registry)
        {
            _defaultExpressions
                .Where(e => e.Matches(propertyInfo))
                .Each(e => e.Alterations.Each(action => action(new Accessor(type, propertyInfo), registry)));
        }
    }
}