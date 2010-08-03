using Valcon.Registration.Graph;

namespace Valcon.Rules
{
    public abstract class BasicValidationRule<TModel> : IValidationRule
        where TModel : class
    {
        private readonly Accessor _accessor;

        protected BasicValidationRule(Accessor accessor)
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

        public ValidationError Validate(object model)
        {
            var modelToValidate = model as TModel;
            if(modelToValidate == null)
            {
                return InvalidModelState();
            }

            return Validate(modelToValidate);
        }

        public abstract ValidationError Validate(TModel model);

        public string PropertyName
        {
            get { return Accessor.Property.Name; }
        }
    }
}