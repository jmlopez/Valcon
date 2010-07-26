using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Valcon.Rules;

namespace Valcon
{
    public class ValidationChain : IEnumerable<IValidationRule>
    {
        private readonly IList<IValidationRule> _rules;
        private readonly Type _type;
        public ValidationChain(Type type)
        {
            _rules = new List<IValidationRule>();
            _type = type;
        }

        public void AddRule(IValidationRule rule)
        {
            _rules.Add(rule);
        }

        public Type ModelType
        {
            get { return _type; }
        }

        public IEnumerator<IValidationRule> GetEnumerator()
        {
            return _rules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<IValidationRule> RulesFor(string propertyName)
        {
            return this.Where(r => r.PropertyName == propertyName);
        }

        public static ValidationChain ForGeneric(Type modelType)
        {
            var chainType = typeof (ValidationChain<>).MakeGenericType(modelType);
            return (ValidationChain) Activator.CreateInstance(chainType);
        }
    }

    public class ValidationChain<T> : ValidationChain
    {
        public ValidationChain()
            : base(typeof(T))
        {
        }

        public IEnumerable<IValidationRule> RulesFor(Expression<Func<T, object>> propertyExpression)
        {
            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Invalid expression - not a member access.", "propertyExpression");
            }

            // TODO -- Blow up if not a property? Or should we allow for fields too?
            var property = memberExpression.Member as PropertyInfo;
            if(property == null)
            {
                yield break;
            }

            var rules = this.Where(r => r.PropertyName == property.Name);
            foreach (var rule in rules)
            {
                yield return rule;
            }
        }
    }
}