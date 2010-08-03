using Valcon.Rules;

namespace Valcon.Attributes
{
    public class PhoneNumberAttribute : RuleAttribute
    {
        public PhoneNumberAttribute()
            : base(typeof(PhoneNumberValidationRule<>))
        {
        }
    }
}