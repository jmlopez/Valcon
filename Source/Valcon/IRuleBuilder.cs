using System;
using System.Linq;
using System.Reflection;
using Valcon.Registration.Graph;

namespace Valcon
{
    public interface IRuleBuilder
    {
        IValidationRule Build(ValidationCall call);
    }

    public class RuleBuilder : IRuleBuilder
    {
        private readonly Func<Type, object> _serviceLocator;

        public RuleBuilder(Func<Type, object> serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public IValidationRule Build(ValidationCall call)
        {
            var def = call.ToRuleDef();
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

            var parameters = targetCtor.GetParameters();
            var args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; ++i)
            {
                var parameter = parameters[i];
                // TODO -- support dependencies via Func?
                if (parameter.ParameterType == typeof(Accessor))
                {
                    args[i] = call.Accessor;
                }
                else
                {
                    args[i] = _serviceLocator(parameter.ParameterType);
                }
            }

            return (IValidationRule) targetCtor.Invoke(args);
        }
    }
}