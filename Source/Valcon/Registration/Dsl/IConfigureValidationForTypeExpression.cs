using Valcon.Rules;

namespace Valcon.Registration.Dsl
{
    public interface IConfigureValidationForTypeExpression
    {
        IConfigureValidationForTypeExpression AddRule(IValidationRule rule);
    }

    public interface IConfigureValidationForTypeExpression<T>
        where T : class
    {
        IConfigureValidationForTypeExpression<T> AddRule(IValidationRule rule);
    }
}