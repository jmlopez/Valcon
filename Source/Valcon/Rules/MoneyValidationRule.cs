using System;
using System.Linq.Expressions;

namespace Valcon.Rules
{
    public class MoneyValidationRule<TModel, TField> : BasicValidationRule<TModel, TField>
       where TModel : class
    {
        public MoneyValidationRule(Expression<Func<TModel, TField>> property)
            : base(property)
        {
        }

        public override ValidationError Validate(TModel model)
        {
            var propertyVal = GetRawValue(model);
            if (propertyVal == null || !propertyVal.GetType().IsAssignableFrom(typeof(double)))
            {
                return InvalidModelState();
            }

            var val = Double.Parse(propertyVal.ToString());
            if(val >= 0)
            {
                return null;
            }

            return new ValidationError(PropertyInfo, "Invalid money value specified.");
        }
    }
}