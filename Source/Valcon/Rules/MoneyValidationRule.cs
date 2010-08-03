using System;
using Valcon.Registration.Graph;

namespace Valcon.Rules
{
    public class MoneyValidationRule<TModel> : BasicValidationRule<TModel>
       where TModel : class
    {
        public MoneyValidationRule(Accessor accessor) 
            : base(accessor)
        {
        }

        public override ValidationError Validate(TModel model)
        {
            var propertyVal = GetPropertyValue(model);

            Decimal val;
            if (propertyVal == null || !Decimal.TryParse(propertyVal.ToString(), out val) || val < 0)
            {
                return Error("Invalid money value specified: {0}.", propertyVal);
            }

            return null;
        }
    }
}