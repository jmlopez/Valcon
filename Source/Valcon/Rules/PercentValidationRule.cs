using System;
using System.Linq.Expressions;

namespace Valcon.Rules
{
    public class PercentValidationRule<TModel, TField> : MoneyValidationRule<TModel, TField>
       where TModel : class
    {
        public PercentValidationRule(Expression<Func<TModel, TField>> property)
            : base(property)
        {
        }
    }
}