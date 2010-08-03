using System;

namespace Valcon.Attributes
{
    public abstract class RuleAttribute : Attribute
    {
        public RuleAttribute(Type ruleType)
        {
            if(!ruleType.IsValidationRule())
            {
               throw new ArgumentException("Invalid rule type specified.", "ruleType"); 
            }

            RuleType = ruleType;
        }

        public Type RuleType { get; private set; }
    }
}