using System;
using Valcon.Registration.Graph;

namespace Valcon.Rules
{
    public class RequiredValidationRule<TModel> : BasicValidationRule<TModel>
        where TModel : class
    {
        public RequiredValidationRule(Accessor accessor) 
            : base(accessor)
        {
        }

        public override ValidationError Validate(TModel model)
        {
            var propertyValue = Accessor.Property.GetValue(model, null);
            if (propertyValue == null)
            {
                return InvalidModelState();
            }

            var type = Accessor.Property.PropertyType;
            if (!type.IsValueType)
            {
                return null;
            }

            // I don't like this but it's working for now
            var defaultValue = Activator.CreateInstance(type);
            if (defaultValue.Equals(propertyValue))
            {
                return new ValidationError(Accessor, string.Format("{0} is required", PropertyName));
            }

            return null;
        }
    }
}