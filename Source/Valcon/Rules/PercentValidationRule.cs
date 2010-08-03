using Valcon.Registration.Graph;

namespace Valcon.Rules
{
    public class PercentValidationRule<TModel> : MoneyValidationRule<TModel>
       where TModel : class
    {
        public PercentValidationRule(Accessor accessor) 
            : base(accessor)
        {
        }
    }
}