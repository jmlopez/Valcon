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
            //var propertyVal = GetRawValue(model);
            //if(propertyVal == null)
            //{
            //    return InvalidModelState();
            //}

            //DateTime val;
            //if(DateTime.TryParse(propertyVal.ToString(), out val))
            //{
            //    return null;
            //}

            //return new ValidationError(PropertyInfo, "Invalid date specified.");
            
            //TODO
            throw new NotImplementedException();
        }
    }
}