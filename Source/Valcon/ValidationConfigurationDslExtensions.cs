using System;
using System.Linq.Expressions;
using Valcon.Registration.Dsl;
using Valcon.Registration.Graph;
using Valcon.Rules;

namespace Valcon
{
    public static class ValidationConfigurationDslExtensions
    {
        public static IConfigureValidationForTypeExpression<TModel> Require<TModel, TField>(this IConfigureValidationForTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddCall(new ValidationCall(typeof (RequiredValidationRule<>), property.ToAccessor()));
        }

        public static IConfigureValidationForTypeExpression<TModel> Date<TModel, TField>(this IConfigureValidationForTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddCall(new ValidationCall(typeof(DateValidationRule<>), property.ToAccessor()));
        }

        public static IConfigureValidationForTypeExpression<TModel> Email<TModel, TField>(this IConfigureValidationForTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddCall(new ValidationCall(typeof(EmailValidationRule<>), property.ToAccessor()));
        }

        public static IConfigureValidationForTypeExpression<TModel> Money<TModel, TField>(this IConfigureValidationForTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddCall(new ValidationCall(typeof(MoneyValidationRule<>), property.ToAccessor()));
        }

        public static IConfigureValidationForTypeExpression<TModel> Percent<TModel, TField>(this IConfigureValidationForTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddCall(new ValidationCall(typeof(PercentValidationRule<>), property.ToAccessor()));
        }

        public static IConfigureValidationForTypeExpression<TModel> PhoneNumber<TModel, TField>(this IConfigureValidationForTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddCall(new ValidationCall(typeof(PhoneNumberValidationRule<>), property.ToAccessor()));
        }
    }
}