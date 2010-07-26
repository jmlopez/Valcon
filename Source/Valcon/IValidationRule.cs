namespace Valcon
{
    public interface IValidationRule
    {
        ValidationError Validate(object model);
        string PropertyName { get; }
    }
}