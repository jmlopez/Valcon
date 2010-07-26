using System;
using System.Linq.Expressions;

namespace Valcon.Rules
{
    public class DateValidationRule<TModel, TField> : BasicValidationRule<TModel, TField>
        where TModel : class
    {
        public DateValidationRule(Expression<Func<TModel, TField>> property)
            : base(property)
        {
        }

        public override ValidationError Validate(TModel model)
        {
            var propertyVal = GetRawValue(model);
            if(propertyVal == null)
            {
                return InvalidModelState();
            }

            DateTime val;
            if(DateTime.TryParse(propertyVal.ToString(), out val))
            {
                return null;
            }

            return new ValidationError(PropertyInfo, "Invalid date specified.");
        }
    }
}