using Valcon.Registration.Graph;

namespace Valcon.Registration.Dsl
{
    public interface IConfigureValidationForTypeExpression
    {
        IConfigureValidationForTypeExpression AddCall(ValidationCall call);
    }

    public interface IConfigureValidationForTypeExpression<T>
        where T : class
    {
        IConfigureValidationForTypeExpression<T> AddCall(ValidationCall call);
    }
}