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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ValidationCall)) return false;
            return Equals((ValidationCall) obj);
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

        public bool Equals(ValidationCall other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._ruleType, _ruleType) && Equals(other._accessor, _accessor);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_ruleType != null ? _ruleType.GetHashCode() : 0)*397) ^ (_accessor != null ? _accessor.GetHashCode() : 0);
            }
        }
    }
}