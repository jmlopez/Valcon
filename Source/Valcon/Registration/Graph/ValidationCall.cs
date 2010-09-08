using System;
using System.Collections.Generic;
using Valcon.Rules;

namespace Valcon.Registration.Graph
{
    public class ValidationCall : ValidationNode
    {
        private readonly Accessor _accessor;
        public ValidationCall(Type ruleType, Accessor accessor)
            : base(ruleType)
        {
            if(!typeof(IValidationRule).IsAssignableFrom(ruleType))
            {
                throw new ArgumentException("Invalid rule type specified", "ruleType");
            }

            _accessor = accessor;
        }

        public Accessor Accessor
        {
            get { return _accessor; }
        }

        public override RuleDef ToRuleDef()
        {
            return new RuleDef
                       {
                           Name = DetermineRuleName(),
                           Type = DetermineRuleType(),
                           Dependencies = new List<ValueDependency>
                                              {
                                                  new ValueDependency
                                                      {
                                                          DependencyType = typeof(Accessor),
                                                          Value = Accessor
                                                      }
                                              }
                       };
        }

        protected virtual string DetermineRuleName()
        {
            var ruleName = DetermineRuleType().Name;
            if(!ruleName.Contains("ValidationRule"))
            {
                return ruleName;
            }

            return ruleName.Substring(0, ruleName.IndexOf("ValidationRule"));
        }

        protected virtual Type DetermineRuleType()
        {
            if (RuleType.IsGenericTypeDefinition)
            {
                return RuleType.MakeGenericType(Accessor.ModelType);
            }

            return RuleType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ValidationCall)) return false;
            return Equals((ValidationCall) obj);
        }

        public bool Equals(ValidationCall other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.RuleType, RuleType) && Equals(other._accessor, _accessor);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((RuleType != null ? RuleType.GetHashCode() : 0) * 397) ^ (_accessor != null ? _accessor.GetHashCode() : 0);
            }
        }
    }
}