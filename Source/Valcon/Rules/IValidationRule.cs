namespace Valcon.Rules
{
    public interface IValidationRule
    {
        ValidationError Validate(object model);
    }
}