using System.Text.RegularExpressions;
using Valcon.Registration.Graph;

namespace Valcon.Rules
{
    public class EmailValidationRule<TModel> : BasicValidationRule<TModel>
       where TModel : class
    {
        private static readonly Regex EmailExp;
        static EmailValidationRule()
        {
            EmailExp = new Regex(@"^((?:(?:(?:[a-zA-Z0-9][\.\-\+_]?)*)[a-zA-Z0-9])+)\@((?:(?:(?:[a-zA-Z0-9][\.\-_]?){0,62})[a-zA-Z0-9])+)\.([a-zA-Z0-9]{2,6})$", RegexOptions.Compiled);
        }
        public EmailValidationRule(Accessor accessor)
            : base(accessor)
        {
        }

        public override ValidationError Validate(TModel model)
        {
            var propertyVal = GetPropertyValue(model);
            if(propertyVal == null || !EmailExp.IsMatch(propertyVal.ToString()))
            {
                return Error("Invalid email address specified: {0}.", propertyVal);
            }

            return null;
        }
    }
}