using FubuCore.Reflection;
using FubuMVC.UI.Configuration;
using HtmlTags;

namespace Valcon.FubuMVC
{
    // TODO -- This is simply a place holder and serves only as an example. 
    public class ValidatorTagModifier
    {
        public static void Modify(ElementRequest request, HtmlTag tag)
        {
            var modelType = (request.Accessor is SingleProperty) ? request.Model.GetType() : request.Accessor.OwnerType;
            var rules = Validator
                            .FindChain(modelType)
                            .RulesFor(request.Accessor.FieldName);

            foreach (var rule in rules)
            {
                var cssClass = GetJQueryValidateCssClass(rule);
                tag.AddClass(cssClass.ToLower());
            }
        }

        private static string GetJQueryValidateCssClass(IValidationRule rule)
        {
            return rule.GetType().Name.Replace("ValidationRule`2", string.Empty);
        }
    }
}