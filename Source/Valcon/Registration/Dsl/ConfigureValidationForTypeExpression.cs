using System;
using System.Collections.Generic;

namespace Valcon.Registration.Dsl
{
    public class ConfigureValidationForTypeExpression : IConfigureValidationForTypeExpression
    {
        private readonly List<Action<ValidationChain>> _alterations;
        private readonly Type _modelType;
        public ConfigureValidationForTypeExpression(Type modelType, ValidationRegistry registry)
        {
            _modelType = modelType;
            _alterations = new List<Action<ValidationChain>>();

            registry.AddExpression(graph =>
            {
                var chain = graph.FindChain(modelType);
                _alterations.ForEach(action => action(chain));
            });
        }

        public IConfigureValidationForTypeExpression AddRule(IValidationRule rule)
        {
            _alterations.Add(chain => chain.AddRule(rule));
            return this;
        }
    }

    public class ConfigureValidationForTypeExpression<T> : IConfigureValidationForTypeExpression<T> 
        where T : class
    {
        private readonly List<Action<ValidationChain<T>>> _alterations;
        private readonly Type _modelType;
        public ConfigureValidationForTypeExpression(ValidationRegistry registry)
        {
            _modelType = typeof (T);
            _alterations = new List<Action<ValidationChain<T>>>();

            registry.AddExpression(graph =>
                                       {
                                           var chain = graph.FindChain<T>();
                                           _alterations.ForEach(action => action(chain));
                                       });
        }

        public IConfigureValidationForTypeExpression<T> AddRule(IValidationRule rule)
        {
            _alterations.Add(chain => chain.AddRule(rule));
            return this;
        }
    }
}