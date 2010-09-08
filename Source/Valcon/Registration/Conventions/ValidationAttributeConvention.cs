using System.Collections.Generic;
using Valcon.Registration.Graph;

namespace Valcon.Registration.Conventions
{
    public class ValidationAttributeConvention : IConfigurationAction
    {
        public void Configure(ValidationGraph graph)
        {
            graph
                .Chains
                .Each(chain => chain
                                   .ModelType
                                   .EachProperty(p => p.GetValidationAttributes().Each(attribute => chain.AddCall(new ValidationCall(attribute.RuleType,new Accessor(chain.ModelType, p))))));
            
        }
    }
}