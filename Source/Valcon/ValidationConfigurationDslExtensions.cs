using System;
using System.Linq.Expressions;
using Valcon.Registration.Dsl;
using Valcon.Registration.Graph;
using Valcon.Rules;

namespace Valcon
{
    public static class ValidationConfigurationDslExtensions
    {
        public static ValidationByTypeExpression<TModel> Require<TModel, TField>(this ValidationByTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddCall(new ValidationCall(typeof (RequiredValidationRule<>), property.ToAccessor()));
        }

        public static ValidationByTypeExpression<TModel> Date<TModel, TField>(this ValidationByTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddCall(new ValidationCall(typeof(DateValidationRule<>), property.ToAccessor()));
        }

        public static ValidationByTypeExpression<TModel> Email<TModel, TField>(this ValidationByTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddCall(new ValidationCall(typeof(EmailValidationRule<>), property.ToAccessor()));
        }

        public static ValidationByTypeExpression<TModel> Money<TModel, TField>(this ValidationByTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddCall(new ValidationCall(typeof(MoneyValidationRule<>), property.ToAccessor()));
        }

        public static ValidationByTypeExpression<TModel> Percent<TModel, TField>(this ValidationByTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddCall(new ValidationCall(typeof(PercentValidationRule<>), property.ToAccessor()));
        }

        public static ValidationByTypeExpression<TModel> PhoneNumber<TModel, TField>(this ValidationByTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> property) where TModel : class
        {
            return expression.AddCall(new ValidationCall(typeof(PhoneNumberValidationRule<>), property.ToAccessor()));
        }

        public static ValidationByTypeExpression<TModel> Compare<TModel, TField>(this ValidationByTypeExpression<TModel> expression,
            Expression<Func<TModel, TField>> source, Expression<Func<TModel, TField>> target) where TModel : class
        {
            return expression.AddCall(new ComparisonValidationCall(typeof(ComparisonValidationRule<>), source.ToAccessor(), target.ToAccessor()));
        }
    }
}