using System;

namespace Valcon.Graph
{
    public class ValidationCall
    {
        private readonly Type _ruleType;
        private readonly Accessor _accessor;
        public ValidationCall(Type ruleType, Accessor accessor)
        {
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
    }
}