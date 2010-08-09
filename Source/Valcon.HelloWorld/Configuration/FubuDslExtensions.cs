using System;
using System.Collections.Generic;
using FubuMVC.Core.Registration.Nodes;

namespace Valcon.HelloWorld.Configuration
{
    public static class FubuDslExtensions
    {
        public static bool IsCrudAction(this ActionCall call)
        {
            var crudVerbs = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase) { "CREATE", "REMOVE", "UPDATE" };
            var operation = call.HandlerType.Name.Replace("Endpoint", string.Empty);

            return crudVerbs.Contains(operation) && call.HasInput && call.OutputType().Name.StartsWith("Ajax");
        }
    }
}