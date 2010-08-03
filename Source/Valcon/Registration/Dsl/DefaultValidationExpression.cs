using System;
using System.Collections.Generic;
using System.Reflection;
using Valcon.Registration.Graph;

namespace Valcon.Registration.Dsl
{
    public class DefaultValidationExpression : IDefaultValidationExpression
    {
        private readonly Func<PropertyInfo, bool> _matches;
        private readonly List<Type> _ruleTypes;
        public DefaultValidationExpression(ConfigureDefaultValidationExpression expression, Func<PropertyInfo, bool> matches)
        {
            _matches = matches;
            _ruleTypes = new List<Type>();
  
            expression.AddAlteration((a, r) =>
                                         {
                                             var modelExpression = r.For(a.ModelType);
                                             _ruleTypes.ForEach(ruleType => modelExpression.AddCall(new ValidationCall(ruleType, a)));
                                         });
        }

        public Func<PropertyInfo, bool> Matches
        {
            get { return _matches; }
        }

        public IDefaultValidationExpression AddRule(Type ruleType)
        {
            if(!_ruleTypes.Contains(ruleType))
            {
                _ruleTypes.Add(ruleType);
            }

            return this;
        }
    }
}