using System;
using System.Collections.Generic;
using Valcon.Registration;

namespace Valcon
{
    public static class Validator
    {
        private static ValidationGraph _graph;
        private static IValidationProvider _validationProvider;

        public static ValidationGraph ValidationGraph { get { return _graph; } }
        public static IValidationProvider ValidationProvider { get { return _validationProvider; } }

        public static void Initialize(Action<IInitializationExpression> action)
        {
            lock (typeof(Validator))
            {
                var expression = new InitializationExpression();
                action(expression);

                _graph = expression.BuildGraph();
                _graph.Seal();

                _validationProvider = new ValidationProvider(_graph, new RuleBuilder(expression.ServiceLocator));
            }
        }

        public static IEnumerable<ValidationError> Validate(object model)
        {
            return ValidationProvider.Validate(model);
        }

        public static ValidationChain<T> FindChain<T>()
           where T : class
        {
            return ValidationGraph.FindChain<T>();
        }

        public static ValidationChain FindChain(Type modelType)
        {
            return ValidationGraph.FindChain(modelType);
        }
    }
}