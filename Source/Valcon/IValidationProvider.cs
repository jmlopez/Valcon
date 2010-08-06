using System.Collections.Generic;
using Valcon.Registration;

namespace Valcon
{
    public interface IValidationProvider
    {
        ValidationSummary Validate(object model);
    }

    public class ValidationProvider : IValidationProvider
    {
        private readonly ValidationGraph _graph;
        private readonly IRuleBuilder _ruleBuilder;
        public ValidationProvider(ValidationGraph graph, IRuleBuilder ruleBuilder)
        {
            _graph = graph;
            _ruleBuilder = ruleBuilder;
        }

        public ValidationSummary Validate(object model)
        {
            var errors = new List<ValidationError>();
            if (model != null)
            {
                var chain = _graph.FindChain(model.GetType());
                foreach (var call in chain)
                {
                    var rule = _ruleBuilder.Build(call);
                    var error = rule.Validate(model);
                    if (error != null)
                    {
                        errors.Add(error);
                    }
                }
            }

            return new ValidationSummary(errors);
        }
    }

    
}