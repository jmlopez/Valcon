using System.Reflection;

namespace Valcon
{
    public class ValidationError
    {
        public ValidationError(PropertyInfo propertyName, string errorMessage)
        {
            Property = propertyName;
            ErrorMessage = errorMessage;
        }

        public PropertyInfo Property { get; private set; }
        public string ErrorMessage { get; private set; }
    }
}