namespace Valcon
{
    public interface IValidationProvider
    {
        ValidationSummary Validate(object model);
    }
}