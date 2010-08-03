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
            var propertyValue = GetPropertyValue(model);
            if (propertyValue == null || (typeof(string).IsAssignableFrom(Accessor.Property.PropertyType) 
                && string.IsNullOrEmpty(propertyValue.ToString())))
            {
                return Error("Required field was null or empty: {0}", Accessor.Property.Name);
            }

            return null;
        }
    }
}