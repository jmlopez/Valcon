using Valcon.Registration.Dsl;

namespace Valcon
{
    public interface IInitializationExpression : IValidationRegistry
    {
        void AddRegistry<T>() where T : ValidationRegistry, new();
        void AddRegistry(ValidationRegistry registry);
    }
}