using System;
using System.Collections.Generic;
using FubuCore.Binding;
using FubuMVC.Core;
using FubuMVC.Core.Runtime;
using FubuMVC.UI;
using Valcon.HelloWorld.Configuration.Conventions;
using Valcon.HelloWorld.Configuration.Policies;
using Valcon.HelloWorld.Configuration.Sources;
using Valcon.HelloWorld.Endpoints;
using Valcon.HelloWorld.Infrastructure;
using Valcon.HelloWorld.Models;

namespace Valcon.HelloWorld.Configuration
{
    public class HelloWorldFubuRegistry : FubuRegistry
    {
        public HelloWorldFubuRegistry()
        {
            IncludeDiagnostics(true);

            Applies
                .ToThisAssembly();

            var httpVerbs = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase) { "GET", "POST" };

            Actions
                .IncludeTypes(t => t.Namespace.StartsWith(typeof (EndpointMarker).Namespace) && t.Name.EndsWith("Endpoint"))
                .IncludeMethods(action => httpVerbs.Contains(action.Method.Name))
                .FindWith<FindActionsSource>();

            httpVerbs
                .Each(verb => Routes.ConstrainToHttpMethod(action => action.Method.Name.Equals(verb, StringComparison.InvariantCultureIgnoreCase), verb));

            Views
                .TryToAttach(findViews => findViews.by_ViewModel());

            ApplyConvention<CrudErrorWrapperConvention>();
            ApplyConvention<CrudValidationConvention>();

            Routes
                .UrlPolicy<EndpointUrlPolicy>();

            Models
                .BindModelsWith<ComplexJsonBinder>();

            Services(r => r.SetServiceIfNone<IJsonService>(new JsonService()));

            Output
                .ToJson
                .WhenCallMatches(c => c.OutputType().Name.StartsWith("Ajax"));

            this.HtmlConvention(new HelloWorldHtmlConventions(Validator.ValidationGraph));
        }
    }

    public class ComplexJsonBinder : IModelBinder
    {
        public bool Matches(Type type)
        {
            return type.Name.Contains("InputModel");
        }

        public void Bind(Type type, object instance, IBindingContext context)
        {
            type.ToString();
        }

        public object Bind(Type type, IBindingContext context)
        {
            var jsonModel = context
                                .Service<IFubuRequest>()
                                .Get<JsonModel>();
            return context
                    .Service<IJsonService>()
                    .Deserialize(type, jsonModel.Body);
        }
    }
}