using Valcon.Rules;

namespace Valcon.Attributes
{
    public class DateAttribute : RuleAttribute
    {
        public DateAttribute()
            : base(typeof(DateValidationRule<>))
        {
        }
    }
}