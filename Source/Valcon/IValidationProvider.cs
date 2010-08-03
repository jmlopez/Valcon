using System.Collections.Generic;
using Valcon.Registration;

namespace Valcon
{
    public interface IValidationProvider
    {
        IEnumerable<ValidationError> Validate(object model);
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

        public IEnumerable<ValidationError> Validate(object model)
        {
            if(model == null)
            {
                yield break;
            }

            var chain = _graph.FindChain(model.GetType());
            foreach (var call in chain)
            {
                var rule = _ruleBuilder.Build(call);
                var error = rule.Validate(model);
                if(error != null)
                {
                    yield return error;
                }
            }
        }
    }

    
}