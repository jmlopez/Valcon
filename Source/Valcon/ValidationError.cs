using Valcon.Registration.Graph;

namespace Valcon
{
    public class ValidationError
    {
        public ValidationError(Accessor accessor, string errorMessage)
        {
            Accessor = accessor;
            ErrorMessage = errorMessage;
        }

        public Accessor Accessor { get; private set; }
        public string ErrorMessage { get; private set; }
    }
}