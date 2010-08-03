using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Valcon.Registration.Graph;
using Valcon.Rules;

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

    public interface IDefaultValidationExpression
    {
        IDefaultValidationExpression ConfigureRule(Type ruleType);
    }

    public class DefaultValidationExpression : IDefaultValidationExpression
    {
        private readonly Func<PropertyInfo, bool> _matches;
        private readonly List<Type> _ruleTypes;
        public DefaultValidationExpression(ConfigureDefaultValidationExpression expression, Func<PropertyInfo, bool> matches)
        {
            _matches = matches;
            _ruleTypes = new List<Type>();
            var ruleBuilder = new RuleBuilder();

            expression.AddAlteration((a, r) =>
                                          {
                                              var modelExpression = r.For(a.ModelType);
                                              _ruleTypes.ForEach(ruleType => modelExpression.AddRule(ruleBuilder.Build(a, ruleType)));
                                          });
        }

        public Func<PropertyInfo, bool> Matches
        {
            get { return _matches; }
        }

        public IDefaultValidationExpression ConfigureRule(Type ruleType)
        {
            if(!_ruleTypes.Contains(ruleType))
            {
                _ruleTypes.Add(ruleType);
            }

            return this;
        }
    }

    public class RuleBuilder
    {
        public IValidationRule Build(Accessor accessor, Type ruleType)
        {
            var concreteRuleType = ruleType.MakeGenericType(accessor.ModelType, accessor.Property.PropertyType);
            var constructors = concreteRuleType.GetConstructors(BindingFlags.Instance | BindingFlags.CreateInstance | BindingFlags.Public);
            if (constructors.Length == 0)
            {
                return null;
            }

            return (IValidationRule)constructors[0].Invoke(new object[] { LambdaBuilder.ToLambda(accessor.ModelType, accessor.Property) });
        }
    }

    public static class DefautlValidationDslExtensions
    {
        public static IDefaultValidationExpression MakeRequired(this IDefaultValidationExpression expression)
        {
            return expression.ConfigureRule(typeof(RequiredValidationRule<>));
        }

        public static IDefaultValidationExpression ValidateAsEmail(this IDefaultValidationExpression expression)
        {
            return expression.ConfigureRule(typeof (EmailValidationRule<>));
        }

        public static IDefaultValidationExpression ValidateAsPhoneNumber(this IDefaultValidationExpression expression)
        {
            return expression.ConfigureRule(typeof(PhoneNumberValidationRule<>));
        }
    }
}