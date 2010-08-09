using Valcon.Registration.Dsl;
using Valcon.Rules;

namespace Valcon
{
    public static class DefaultValidationDslExtensions
    {
        public static IDefaultValidationExpression IsRequired(this IDefaultValidationExpression expression)
        {
            return expression.AddRule(typeof(RequiredValidationRule<>));
        }

        public static IDefaultValidationExpression IsEmail(this IDefaultValidationExpression expression)
        {
            return expression.AddRule(typeof (EmailValidationRule<>));
        }

        public static IDefaultValidationExpression IsPhoneNumber(this IDefaultValidationExpression expression)
        {
            return expression.AddRule(typeof(PhoneNumberValidationRule<>));
        }
    }
}