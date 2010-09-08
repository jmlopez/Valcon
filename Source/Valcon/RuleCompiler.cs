using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Valcon.Registration;
using Valcon.Registration.Graph;
using Valcon.Rules;

namespace Valcon
{
    public class RuleCompiler : IRuleCompiler
    {
        private readonly ValidationGraph _graph;

        public RuleCompiler(ValidationGraph graph)
        {
            _graph = graph;
        }

        public IValidationRule Compile(RuleDef def)
        {
            var ruleType = def.Type;

            // At minimum, rules should require an Accessor in their constructor
            var targetCtor = ruleType
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .OrderByDescending(c => c.GetParameters().Length)
                .FirstOrDefault();

            if(targetCtor == null)
            {
                throw new InvalidOperationException(string.Format("Could not find valid constructor for rule: {0}", ruleType));
            }

            var dependencies = new List<ValueDependency>(def.Dependencies);
            var parameters = targetCtor.GetParameters();
            var args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; ++i)
            {
                var parameter = parameters[i];
                var dependency = dependencies.FirstOrDefault(d => d.DependencyType == parameter.ParameterType);
                if(dependency != null)
                {
                    args[i] = dependency.Value;
                    dependencies.Remove(dependency);
                }
                else
                {
                    args[i] = _graph.ServiceLocator(parameter.ParameterType);
                }
            }

            return (IValidationRule) targetCtor.Invoke(args);
        }
    }
}