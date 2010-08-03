using System;
using System.Linq.Expressions;
using System.Reflection;
using Valcon.Rules;

namespace Valcon.Registration.Graph
{
    public static class GraphExtensions
    {
        public static bool IsRequired(this ValidationCall call)
        {
            return CallIsOfRuleType(typeof(RequiredValidationRule<>), call);
        }

        public static bool IsEmail(this ValidationCall call)
        {
            return CallIsOfRuleType(typeof(EmailValidationRule<>), call);
        }

        public static bool IsPhoneNumber(this ValidationCall call)
        {
            return CallIsOfRuleType(typeof(PhoneNumberValidationRule<>), call);
        }

        public static bool IsMoney(this ValidationCall call)
        {
            return CallIsOfRuleType(typeof(MoneyValidationRule<>), call);
        }

        public static bool IsPercent(this ValidationCall call)
        {
            return CallIsOfRuleType(typeof(PercentValidationRule<>), call);
        }

        private static bool CallIsOfRuleType(Type type, ValidationCall call)
        {
            return type.IsAssignableFrom(call.RuleType.GetGenericTypeDefinition());
        }

        public static Accessor ToAccessor<TModel, TField>(this Expression<Func<TModel, TField>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Invalid expression - not a member access.", "expression");
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("Invalid expression - not a property accessor.", "expression");
            }

            return new Accessor(typeof(TModel), propertyInfo);
        }
    }
}