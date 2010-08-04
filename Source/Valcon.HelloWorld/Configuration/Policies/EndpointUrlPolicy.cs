using System;
using System.Text;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;
using Valcon.HelloWorld.Endpoints;

namespace Valcon.HelloWorld.Configuration.Policies
{
    public class EndpointUrlPolicy : IUrlPolicy
    {
        private const string EndpointString = "Endpoint";
        
        public bool Matches(ActionCall call, IConfigurationObserver log)
        {
            return call.HandlerType.Name.EndsWith(EndpointString);
        }

        public IRouteDefinition Build(ActionCall call)
        {
            var routeDefinition = call.ToRouteDefinition();

            var strippedNamespace = call
                                        .HandlerType
                                        .Namespace
                                        .Replace(typeof(EndpointMarker).Namespace + ".", string.Empty);

            if (!strippedNamespace.Contains("."))
            {
                routeDefinition.Append(BreakUpCamelCaseWithHypen(strippedNamespace));
            }
            else
            {
                var patternParts = strippedNamespace.Split(new[] { "." }, StringSplitOptions.None);
                foreach (var patternPart in patternParts)
                {
                    routeDefinition.Append(BreakUpCamelCaseWithHypen(patternPart.Trim()));
                }
            }

            routeDefinition.Append(BreakUpCamelCaseWithHypen(call.HandlerType.Name.Replace(EndpointString, string.Empty)));
            return routeDefinition;
        }

        private static string BreakUpCamelCaseWithHypen(string input)
        {
            var routeBuilder = new StringBuilder();
            for (int i = 0; i < input.Length; ++i)
            {
                if (i != 0 && char.IsUpper(input[i]))
                {
                    routeBuilder.Append("-");
                }

                routeBuilder.Append(input[i]);
            }

            return routeBuilder
                        .ToString()
                        .ToLower();
        }
    }
}