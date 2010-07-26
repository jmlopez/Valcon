using System;
using Valcon.Registration.Dsl;

namespace Valcon.Conventions
{
    public interface IRegistrationConvention
    {
        void Process(Type type, ValidationRegistry registry);
    }
}