using System.Collections.Generic;
using Valcon.Registration;

namespace Valcon
{
    public class ValidationProvider : IValidationProvider
    {
        private readonly ValidationGraph _graph;
        private readonly IRuleCompiler _ruleCompiler;
        public ValidationProvider(ValidationGraph graph, IRuleCompiler ruleCompiler)
        {
            _graph = graph;
            _ruleCompiler = ruleCompiler;
        }

        public ValidationSummary Validate(object model)
        {
            var errors = new List<ValidationError>();
            if (model != null)
            {
                var chain = _graph.FindChain(model.GetType());
                foreach (var call in chain)
                {
                    var rule = _ruleCompiler.Compile(call.ToRuleDef());
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