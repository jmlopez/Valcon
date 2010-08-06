using System;

namespace Valcon.Registration.Graph
{
    public abstract class ValidationNode
    {
        private readonly Type _ruleType;

        protected ValidationNode(Type ruleType)
        {
            _ruleType = ruleType;
        }

        public Type RuleType
        {
            get { return _ruleType; }
        }

        public virtual RuleDef ToRuleDef()
        {
            return new RuleDef
                       {
                           Type = RuleType
                       };
        }
    }
}