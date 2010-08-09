using Valcon.Registration.Graph;

namespace Valcon.Rules
{
    public class ComparisonValidationRule<TModel> : BaseComparisonValidationRule<TModel>
        where TModel : class
    {
        public ComparisonValidationRule(Accessor source, Accessor target) 
            : base(source, target)
        {
        }

        public override ValidationError Compare(object sourceValue, object targetValue)
        {
            if(sourceValue.Equals(targetValue))
            {
                return null;
            }

            return Error("Values must match. Expected \"{0}\" but found \"{1}\"", sourceValue, targetValue);
        }
    }
}