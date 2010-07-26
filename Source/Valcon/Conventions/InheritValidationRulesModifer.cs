using System;
using System.Linq;

namespace Valcon.Conventions
{
    public class InheritValidationRulesModifer : IGraphModifier
    {
        public void Modify(ValidationGraph graph)
        {
            foreach (var chain in graph.Chains.ToList())
            {
                CopyRulesForChain(graph, chain);
            }
        }

        public void CopyRulesForChain(ValidationGraph graph, ValidationChain chain)
        {
            var modelType = chain.ModelType.BaseType;
            while(modelType != null && modelType != typeof(Object))
            {
                var properties = modelType.GetPublicProperties();
                foreach (var property in properties)
                {
                    if (!chain.RulesFor(property.Name).Any())
                    {
                        var parentChain = graph.FindChain(modelType).RulesFor(property.Name);
                        foreach (var rule in parentChain)
                        {
                            chain.AddRule(rule);
                        }
                    }
                }

                modelType = modelType.BaseType;
            }
        }
    }
}