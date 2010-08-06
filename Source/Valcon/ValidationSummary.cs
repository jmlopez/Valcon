using System.Collections.Generic;
using System.Linq;

namespace Valcon
{
    public class ValidationSummary
    {
        private readonly IEnumerable<ValidationError> _errors;

        public ValidationSummary(IEnumerable<ValidationError> errors)
        {
            _errors = errors;
        }

        public bool IsValid { get { return !_errors.Any(); } }

        public int ErrorCount { get { return _errors.Count(); } }

        public IEnumerable<ValidationError> Errors { get { return _errors; } }
    }
}