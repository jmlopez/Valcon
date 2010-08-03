using System;
using System.Collections.Generic;
using Valcon.Registration;
using Valcon.Registration.Dsl;

namespace Valcon
{
    public class InitializationExpression : ValidationRegistry, IInitializationExpression
    {
        private readonly List<ValidationRegistry> _registries;
        private Func<Type, object> _serviceLocator;
        public InitializationExpression()
        {
            _registries = new List<ValidationRegistry> {this};
            _serviceLocator = t => null;
        }

        public Func<Type, object> ServiceLocator
        {
            get { return _serviceLocator; }
        }

        public void BuildDependenciesWith(Func<Type, object> serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public void AddRegistry<T>() 
            where T : ValidationRegistry, new()
        {
            AddRegistry(new T());
        }

        public void AddRegistry(ValidationRegistry registry)
        {
            _registries.Add(registry);
        }

        public ValidationGraph BuildGraph()
        {
            var graph = new ValidationGraph();
            _registries.ForEach(registry => registry.ConfigureGraph(graph));

            return graph;
        }
    }
}