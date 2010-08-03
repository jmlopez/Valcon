using System;

namespace Valcon.Registration.Graph
{
    public class ValidationCall
    {
        private readonly Type _ruleType;
        private readonly Accessor _accessor;
        public ValidationCall(Type ruleType, Accessor accessor)
        {
            if(!typeof(IValidationRule).IsAssignableFrom(ruleType))
            {
                throw new ArgumentException("Invalid rule type specified", "ruleType");
            }

            _ruleType = ruleType;
            _accessor = accessor;
        }

        public Accessor Accessor
        {
            get { return _accessor; }
        }

        public Type RuleType
        {
            get { return _ruleType; }
        }

        public virtual RuleDef ToRuleDef()
        {
            return new RuleDef
                       {
                           Type = DetermineRuleType()
                       };
        }

        protected virtual Type DetermineRuleType()
        {
            if(RuleType.IsGenericTypeDefinition)
            {
                return RuleType.MakeGenericType(Accessor.ModelType);
            }

            return RuleType;
        }
    }
}