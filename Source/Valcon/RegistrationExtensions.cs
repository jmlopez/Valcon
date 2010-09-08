using System;
using System.Collections.Generic;
using Valcon.Registration;
using Valcon.Registration.Conventions;

namespace Valcon
{
    public static class RegistrationExtensions
    {
        public static void Configure(this IEnumerable<IConfigurationAction> actions, ValidationGraph graph)
        {
            actions.Each(x => x.Configure(graph));
        }

        public static void AddAction(this IList<IConfigurationAction> actions, Action<ValidationGraph> action)
        {
            actions.Add(new LambdaConfigurationAction(action));
        }

        public static void UseValidationAttributes(this ValidationRegistry registry)
        {
            registry.ApplyConvention<ValidationAttributeConvention>();
        }

        public static void InheritValidationRules(this ValidationRegistry registry)
        {
            registry.ApplyConvention<InheritValidationRulesConvention>();
        }
    }
}