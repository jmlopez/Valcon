using System;
using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.UI;
using Valcon.HelloWorld.Configuration.Policies;
using Valcon.HelloWorld.Endpoints;

namespace Valcon.HelloWorld.Configuration
{
    public class HelloWorldFubuRegistry : FubuRegistry
    {
        public HelloWorldFubuRegistry()
        {
            IncludeDiagnostics(true);

            Applies
                .ToThisAssembly();

            var httpVerbs = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase) { "GET", "POST", "PUT", "HEAD" };

            Actions
                .IncludeTypes(t => t.Namespace.StartsWith(typeof (EndpointMarker).Namespace) && t.Name.EndsWith("Endpoint"))
                .IncludeMethods(action => httpVerbs.Contains(action.Method.Name));

            httpVerbs
                .Each(verb => Routes.ConstrainToHttpMethod(action => action.Method.Name.Equals(verb, StringComparison.InvariantCultureIgnoreCase), verb));

            Views
                .TryToAttach(findViews => findViews.by_ViewModel());

            Routes
                .UrlPolicy<EndpointUrlPolicy>();

            Output
                .ToJson
                .WhenCallMatches(c => c.OutputType().Name.StartsWith("Ajax"));

            this.UseDefaultHtmlConventions();
        }
    }
}