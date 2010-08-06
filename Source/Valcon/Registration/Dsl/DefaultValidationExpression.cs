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
        private readonly List<Action<Accessor, ValidationRegistry>> _alterations;
        public DefaultValidationExpression(Func<PropertyInfo, bool> matches)
        {
            _matches = matches;
            _ruleTypes = new List<Type>();
            _alterations = new List<Action<Accessor, ValidationRegistry>>();
        }

        public IEnumerable<Action<Accessor, ValidationRegistry>> Alterations
        {
            get { return _alterations; }
        }

        public Func<PropertyInfo, bool> Matches
        {
            get { return _matches; }
        }

        public IDefaultValidationExpression AddRule(Type ruleType)
        {
            if(_ruleTypes.Contains(ruleType))
            {
                return this;
            }
            
            _ruleTypes.Add(ruleType);
            _alterations.Add((a, r) =>
                                {
                                    var modelExpression = r.For(a.ModelType);
                                    _ruleTypes.ForEach(rule => modelExpression.AddCall(new ValidationCall(rule, a)));
                                });
            return this;
        }
    }
}