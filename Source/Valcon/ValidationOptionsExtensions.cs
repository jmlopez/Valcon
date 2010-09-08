using Valcon.Registration.Dsl;
using Valcon.Rules;

namespace Valcon
{
    public static class ValidationOptionsExtensions
    {
        public static IValidationOptions AsRequired(this IValidationOptions options)
        {
            return options.AddRule(typeof(RequiredValidationRule<>));
        }

        public static IValidationOptions AsEmail(this IValidationOptions options)
        {
            return options.AddRule(typeof(EmailValidationRule<>));
        }

        public static IValidationOptions AsPhoneNumber(this IValidationOptions options)
        {
            return options.AddRule(typeof(PhoneNumberValidationRule<>));
        }
    }
}