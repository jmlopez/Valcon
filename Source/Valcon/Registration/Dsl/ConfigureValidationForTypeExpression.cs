using System;
using System.Collections.Generic;
using Valcon.Registration.Graph;

namespace Valcon.Registration.Dsl
{
    public class ConfigureValidationForTypeExpression : IConfigureValidationForTypeExpression
    {
        private readonly List<Action<ValidationChain>> _alterations;
        public ConfigureValidationForTypeExpression(Type modelType, ValidationRegistry registry)
        {
            _alterations = new List<Action<ValidationChain>>();

            registry.AddExpression(graph =>
            {
                var chain = graph.FindChain(modelType);
                _alterations.ForEach(action => action(chain));
            });
        }

        public IConfigureValidationForTypeExpression AddCall(ValidationCall call)
        {
            _alterations.Add(chain => chain.AddCall(call));
            return this;
        }
    }

    public class ConfigureValidationForTypeExpression<T> : IConfigureValidationForTypeExpression<T> 
        where T : class
    {
        private readonly List<Action<ValidationChain<T>>> _alterations;
        public ConfigureValidationForTypeExpression(ValidationRegistry registry)
        {
            _alterations = new List<Action<ValidationChain<T>>>();

            registry.AddExpression(graph =>
                                       {
                                           var chain = graph.FindChain<T>();
                                           _alterations.ForEach(action => action(chain));
                                       });
        }

        public IConfigureValidationForTypeExpression<T> AddCall(ValidationCall call)
        {
            _alterations.Add(chain => chain.AddCall(call));
            return this;
        }
    }
}