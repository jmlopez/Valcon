using System;
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

        public ValidationProvider(ValidationGraph graph)
        {
            _graph = graph;
        }

        public IEnumerable<ValidationError> Validate(object model)
        {
            throw new NotImplementedException();
        }
    }
}