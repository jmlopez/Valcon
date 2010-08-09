using Valcon.Registration.Graph;

namespace Valcon.Rules
{
    public abstract class BaseValidationRule<TModel> : IValidationRule
        where TModel : class
    {
        private readonly Accessor _accessor;

        protected BaseValidationRule(Accessor accessor)
        {
            _accessor = accessor;
        }

        protected ValidationError InvalidModelState()
        {
            return new ValidationError(Accessor, "Invalid model state. Model cannot be null");
        }

        public Accessor Accessor { get { return _accessor; } }

        public ValidationError Error(string message)
        {
            return new ValidationError(Accessor, message);
        }

        public ValidationError Error(string format, params object[] args)
        {
            return new ValidationError(Accessor, string.Format(format, args));
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

        public object GetAccessorValue(object model)
        {
            return GetAccessorValue(Accessor, model);
        }

        public object GetAccessorValue(Accessor accessor, object model)
        {
            return accessor.Property.GetValue(model, null);
        }

        public abstract ValidationError Validate(TModel model);

        public string PropertyName
        {
            get { return Accessor.Property.Name; }
        }
    }
}