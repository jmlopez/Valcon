using Valcon.Rules;

namespace Valcon.Attributes
{
    public class RequiredAttribute : RuleAttribute
    {
        public RequiredAttribute() 
            : base(typeof(RequiredValidationRule<>))
        {
        }
    }
}