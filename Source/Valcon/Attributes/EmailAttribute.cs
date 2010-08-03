using Valcon.Rules;

namespace Valcon.Attributes
{
    public class EmailAttribute : RuleAttribute
    {
        public EmailAttribute()
            : base(typeof(EmailValidationRule<>))
        {
        }
    }
}