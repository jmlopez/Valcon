using System;
using System.Collections.Generic;
using FubuCore.Util;

namespace Valcon.Registration
{
    public class ValidationGraph
    {
        private readonly Cache<Type, ValidationChain> _chains;
        public ValidationGraph()
        {
            _chains = new Cache<Type, ValidationChain>
                          {
                              OnMissing = ValidationChain.GenericForModel
                          };
        }

        public IEnumerable<ValidationChain> Chains { get { return _chains; } }

        public void AddChain(ValidationChain chain)
        {
            _chains.Fill(chain.ModelType, chain);
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

        public Func<Type, object> ServiceLocator { get; set; }
    }
}