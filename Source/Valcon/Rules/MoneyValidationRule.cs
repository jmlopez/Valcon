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
            //var propertyVal = GetRawValue(model);
            //if (propertyVal == null || !propertyVal.GetType().IsAssignableFrom(typeof(double)))
            //{
            //    return InvalidModelState();
            //}

            //var val = Double.Parse(propertyVal.ToString());
            //if(val >= 0)
            //{
            //    return null;
            //}

            //return new ValidationError(PropertyInfo, "Invalid money value specified.");

            //TODO
            throw new NotImplementedException();
        }
    }
}