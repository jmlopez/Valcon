using System;

namespace Valcon.Registration.Dsl
{
    public interface IValidationOptions
    {
        IValidationOptions AddRule(Type ruleType);
    }
}