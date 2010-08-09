using System;
using Valcon.Registration.Graph;

namespace Valcon.Rules
{
    public class PercentValidationRule<TModel> : BaseValidationRule<TModel>
       where TModel : class
    {
        public PercentValidationRule(Accessor accessor) 
            : base(accessor)
        {
        }

        public override ValidationError Validate(TModel model)
        {
            var propertyVal = GetAccessorValue(model);

            Decimal val;
            if (propertyVal == null || !Decimal.TryParse(propertyVal.ToString(), out val))
            {
                return Error("Invalid precentage value specified: {0}.", propertyVal);
            }

            return null;
        }
    }
}