using System;
using Valcon.Registration.Graph;

namespace Valcon.Rules
{
    public abstract class BaseComparisonValidationRule<TModel> : BaseValidationRule<TModel>
        where TModel : class
    {
        private readonly Accessor _target;
        public BaseComparisonValidationRule(Accessor source, Accessor target) 
            : base(source)
        {
            _target = target;
        }

        public Accessor Target { get { return _target; } }

        public override ValidationError Validate(TModel model)
        {
            var sourceValue = GetAccessorValue(model);
            var targetValue = GetAccessorValue(Target, model);

            if(sourceValue == null || targetValue == null)
            {
                return null;
            }

            return Compare(sourceValue, targetValue);
        }

        public abstract ValidationError Compare(object sourceValue, object targetValue);
    }
}