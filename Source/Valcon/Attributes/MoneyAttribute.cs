using Valcon.Rules;

namespace Valcon.Attributes
{
    public class MoneyAttribute : RuleAttribute
    {
        public MoneyAttribute()
            : base(typeof(MoneyValidationRule<>))
        {
        }
    }
}