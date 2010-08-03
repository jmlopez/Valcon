using System;

namespace Valcon.Registration.Dsl
{
    public interface IDefaultValidationExpression
    {
        IDefaultValidationExpression AddRule(Type ruleType);
    }
}