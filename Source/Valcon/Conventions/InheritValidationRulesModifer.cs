using System;
using System.Linq;
using Valcon.Registration;

namespace Valcon.Conventions
{
    public class InheritValidationRulesModifer : IGraphModifier
    {
        public void Modify(ValidationGraph graph)
        {
            foreach (var chain in graph.Chains.ToList())
            {
                CopyCallsForChain(graph, chain);
            }
        }

        public static void CopyCallsForChain(ValidationGraph graph, ValidationChain chain)
        {
            var modelType = chain.ModelType.BaseType;
            while(modelType != null && modelType != typeof(Object))
            {
                var properties = modelType.GetPublicProperties();
                foreach (var property in properties)
                {
                    if (!chain.CallsFor(property.Name).Any())
                    {
                        var parentChain = graph.FindChain(modelType).CallsFor(property.Name);
                        foreach (var call in parentChain)
                        {
                            chain.AddCall(call);
                        }
                    }
                }

                modelType = modelType.BaseType;
            }
        }
    }
}