using System;

namespace Valcon.Registration.Dsl
{
    public interface IValidationRegistry
    {
        IConfigureValidationForTypeExpression For(Type modelType);
        IConfigureValidationForTypeExpression<T> For<T>()
            where T : class;
    }
}