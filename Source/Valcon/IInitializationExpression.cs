using System;
using Valcon.Registration.Dsl;

namespace Valcon
{
    public interface IInitializationExpression : IValidationRegistry
    {
        // TODO -- revisit this...I don't like it but it's a quick stop-gap for our ProAce use
        void BuildDependenciesWith(Func<Type, object> serviceLocator);

        void AddRegistry<T>() where T : ValidationRegistry, new();
        void AddRegistry(ValidationRegistry registry);
    }
}