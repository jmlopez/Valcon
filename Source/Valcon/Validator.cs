using System;
using Valcon.Registration;

namespace Valcon
{
    public static class Validator
    {
        private static ValidationGraph _graph;
        private static IValidationProvider _validationProvider;

        public static ValidationGraph ValidationGraph { get { return _graph; } }
        public static IValidationProvider ValidationProvider { get { return _validationProvider; } }

        public static void Initialize(Action<ValidationRegistry> configure)
        {
            Initialize(new ValidationRegistry(configure));
        }

        public static void Initialize(ValidationRegistry registry)
        {
            lock(typeof(Validator))
            {
                _graph = registry.BuildGraph();
                _validationProvider = new ValidationProvider(_graph, new RuleCompiler(_graph));
            }
        }

        public static ValidationSummary Validate(object model)
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