using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Valcon.Rules
{
    public abstract class BasicValidationRule<TModel, TField> : IValidationRule
        where TModel : class
    {
        private readonly Expression<Func<TModel, TField>> _property;
        protected readonly PropertyInfo PropertyInfo;
        protected BasicValidationRule(Expression<Func<TModel, TField>> property)
        {
            _property = property;

            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Invalid expression - not a member access.", "property");
            }

            // TODO -- Blow up if not a property? Or should we allow for fields too?
            PropertyInfo = memberExpression.Member as PropertyInfo;
        }

        public ValidationError Validate(object model)
        {
            var modelToValidate = model as TModel;
            if(modelToValidate == null)
            {
                return InvalidModelState();
            }

            return Validate(modelToValidate);
        }

        protected ValidationError InvalidModelState()
        {
            return new ValidationError(PropertyInfo, "Invalid model state. Model cannot be null");
        }

        public abstract ValidationError Validate(TModel model);

        public Expression<Func<TModel, TField>> Property
        {
            get { return _property; }
        }

        public TField GetRawValue(TModel model)
        {
            return (TField)PropertyInfo.GetValue(model, null);
        }

        public string PropertyName
        {
            get { return PropertyInfo.Name; }
        }
    }
}