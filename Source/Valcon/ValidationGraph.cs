using System;
using System.Collections.Generic;
using System.Linq;
using Valcon.Registration.Dsl;
using Valcon.Util;
using Valcon.Rules;

namespace Valcon
{
    public class ValidationGraph
    {
        private readonly Cache<Type, ValidationChain> _chains;
        private readonly List<ValidationRegistry> _registries;
        private readonly List<AssemblyScanner> _scanners;
        private readonly TypePool _types;
        public ValidationGraph()
        {
            _types = new TypePool();
            _scanners = new List<AssemblyScanner>();
            _registries = new List<ValidationRegistry>();
            _chains = new Cache<Type, ValidationChain>
                          {
                              OnMissing = ValidationChain.ForGeneric
                          };
        }

        public IEnumerable<ValidationChain> Chains { get { return _chains; } }

        public TypePool Types { get { return _types; } }

        public List<ValidationRegistry> Registries { get { return _registries; } }

        public void AddChain(Type type, ValidationChain chain)
        {
            if(_chains.Exists(c => c.ModelType == type))
            {
                // TODO -- throw?
                return;
            }

            _chains.Fill(type, new ValidationChain(type));
        }

        public void AddRule(Type type, IValidationRule rule)
        {
            FindChain(type).AddRule(rule);
        }

        public ValidationChain<T> FindChain<T>()
            where T : class
        {
            return (ValidationChain<T>) FindChain(typeof (T));
        }

        public ValidationChain FindChain(Type type)
        {
            return _chains[type];
        }

        public void AddScanner(AssemblyScanner scanner)
        {
            _scanners.Add(scanner);
        }

        public void ImportRegistry(Type type)
        {
            if (Registries.Any(x => x.GetType() == type))
            {
                return;
            }

            var registry = (ValidationRegistry) Activator.CreateInstance(type);
            registry.ConfigureGraph(this);
        }

        public void Seal()
        {
            _scanners.ForEach(scanner => scanner.ScanForAll(this));
        }
    }
}