using System;
using System.Linq.Expressions;

namespace Valcon.Rules
{
    public class RequiredValidationRule<TModel, TField> : BasicValidationRule<TModel, TField>
        where TModel : class
    {
        public RequiredValidationRule(Expression<Func<TModel, TField>> property)
            : base(property)
        {
        }

        public override ValidationError Validate(TModel model)
        {
            var propertyValue = GetRawValue(model);
            if(propertyValue == null)
            {
                return InvalidModelState();
            }

            var type = typeof (TField);
            if(!type.IsValueType)
            {
                return null;
            }

            var defaultValue = Activator.CreateInstance(type);
            if (defaultValue.Equals(propertyValue))
            {
                return new ValidationError(PropertyInfo, string.Format("{0} is required", PropertyName));
            }

            return null;
        }
    }
}