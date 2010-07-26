using System;
using System.Collections.Generic;

namespace Valcon.Registration.Dsl
{
    public class ValidationRegistry : IValidationRegistry
    {
        private readonly List<Action<ValidationGraph>> _actions;
        private readonly List<AssemblyScanner> _scanners;
        public ValidationRegistry()
        {
            _actions = new List<Action<ValidationGraph>>();
            _scanners = new List<AssemblyScanner>();
        }

        public void AddExpression(Action<ValidationGraph> alteration)
        {
            _actions.Add(alteration);
        }
        
        public IConfigureValidationForTypeExpression For(Type modelType)
        {
            return new ConfigureValidationForTypeExpression(modelType, this);
        }

        public IConfigureValidationForTypeExpression<T> For<T>()
            where T : class
        {
            return new ConfigureValidationForTypeExpression<T>(this);
        }

        public void ConfigureGraph(ValidationGraph graph)
        {
            _actions.ForEach(action => action(graph));
            graph.Registries.Add(this);
        }

        public void Scan(Action<IAssemblyScanner> action)
        {
            var scanner = new AssemblyScanner();
            action(scanner);

            _actions.Add(graph => graph.AddScanner(scanner));
        }

        public void Configure(Action<ValidationGraph> configure)
        {
            _actions.Add(configure);
        }

        public static bool IsPublicRegistry(Type type)
        {
            if (type.Assembly == typeof(ValidationRegistry).Assembly)
            {
                return false;
            }

            if (!typeof(ValidationRegistry).IsAssignableFrom(type))
            {
                return false;
            }

            if (type.IsInterface || type.IsAbstract || type.IsGenericType)
            {
                return false;
            }

            return (type.GetConstructor(new Type[0]) != null);
        }
    }
}