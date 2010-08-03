using Valcon.Rules;

namespace Valcon.Attributes
{
    public class PercentAttribute : RuleAttribute 
    {
        public PercentAttribute() 
            : base(typeof(PercentValidationRule<>))
        {
        }
    }
}