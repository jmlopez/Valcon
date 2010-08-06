using System;
using System.Collections.Generic;

namespace Valcon.Registration.Graph
{
    public class ComparisonValidationCall : ValidationCall
    {
        private readonly Accessor _target;
        public ComparisonValidationCall(Type ruleType, Accessor accessor, Accessor target)
            : base(ruleType, accessor)
        {
            _target = target;
        }

        public Accessor Target
        {
            get { return _target; }
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
                                                          },
                                                    new ValueDependency
                                                          {
                                                              DependencyType = typeof(Accessor),
                                                              Value = Target
                                                          }
                                              }
            };
        }
    }
}