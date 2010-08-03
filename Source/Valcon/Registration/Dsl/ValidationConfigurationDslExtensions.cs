using System;
using System.Linq.Expressions;
using Valcon.Registration.Graph;
using Valcon.Rules;

namespace Valcon.Registration.Dsl
{
    public static class ValidationConfigurationDslExtensions
    {
        public static IConfigureValidationForTypeExpression<TModel> Require<TModel, TField>(this IConfigureValidationForTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddRule(new RequiredValidationRule<TModel>(property.ToAccessor()));
        }

        public static IConfigureValidationForTypeExpression<TModel> Date<TModel, TField>(this IConfigureValidationForTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddRule(new DateValidationRule<TModel>(property.ToAccessor()));
        }

        public static IConfigureValidationForTypeExpression<TModel> Email<TModel, TField>(this IConfigureValidationForTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddRule(new EmailValidationRule<TModel>(property.ToAccessor()));
        }

        public static IConfigureValidationForTypeExpression<TModel> Money<TModel, TField>(this IConfigureValidationForTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddRule(new MoneyValidationRule<TModel>(property.ToAccessor()));
        }

        public static IConfigureValidationForTypeExpression<TModel> Percent<TModel, TField>(this IConfigureValidationForTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddRule(new PercentValidationRule<TModel>(property.ToAccessor()));
        }

        public static IConfigureValidationForTypeExpression<TModel> PhoneNumber<TModel, TField>(this IConfigureValidationForTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddRule(new PhoneNumberValidationRule<TModel>(property.ToAccessor()));
        }
    }
}