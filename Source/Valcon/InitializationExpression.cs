using System.Collections.Generic;
using Valcon.Registration;
using Valcon.Registration.Dsl;

namespace Valcon
{
    public class InitializationExpression : ValidationRegistry, IInitializationExpression
    {
        private readonly List<ValidationRegistry> _registries;

        public InitializationExpression()
        {
            _registries = new List<ValidationRegistry> {this};
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