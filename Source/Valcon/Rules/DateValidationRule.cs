using System;
using Valcon.Registration.Graph;

namespace Valcon.Rules
{
    public class DateValidationRule<TModel> : BasicValidationRule<TModel>
        where TModel : class
    {
        public DateValidationRule(Accessor accessor)
            : base(accessor)
        {
        }

        public override ValidationError Validate(TModel model)
        {
            var propertyVal = GetPropertyValue(model);

            DateTime val;
            if (propertyVal == null || !DateTime.TryParse(propertyVal.ToString(), out val))
            {
                return Error("Invalid date specified: {0}.", propertyVal);
            }

            return null;
        }
    }
}