using System.Collections.Generic;
using System.Linq;
using FubuCore.Reflection;
using FubuMVC.UI.Configuration;
using HtmlTags;
using Valcon.Registration;
using Valcon.Rules;

namespace Valcon.HelloWorld.Configuration
{
    public class HelloWorldHtmlConventions : DefaultHtmlConventions
    {
        private readonly ValidationGraph _graph;
        public HelloWorldHtmlConventions(ValidationGraph graph)
        {
            _graph = graph;

            Editors
                .If(a => a.Accessor.FieldName.Contains("Password"))
                .Modify(t => t.Attr("type", "password"));

            Editors
                .Always
                .Modify(ModifyWithValidationGraph);
        }

        private void ModifyWithValidationGraph(ElementRequest request, HtmlTag tag)
        {
            // check for partials
            var modelType = (request.Accessor is SingleProperty) ? request.Model.GetType() : request.Accessor.OwnerType;

            var calls = _graph
                            .FindChain(modelType)
                            .CallsFor(request.Accessor.FieldName);

            calls
                .Where(call => !typeof(ComparisonValidationRule<>).IsAssignableFrom(call.RuleType))
                .Each(call => tag.Modify(t => t.AddClass(call.ToRuleDef().Name.ToLower())));

            tag.Attr("id", request.ElementId);
        }
    }
}