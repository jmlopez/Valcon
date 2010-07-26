using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Valcon.Rules
{
    public class EmailValidationRule<TModel, TField> : BasicValidationRule<TModel, TField>
       where TModel : class
    {
        private static readonly Regex EmailExp;
        static EmailValidationRule()
        {
            EmailExp = new Regex(@"^((?:(?:(?:[a-zA-Z0-9][\.\-\+_]?)*)[a-zA-Z0-9])+)\@((?:(?:(?:[a-zA-Z0-9][\.\-_]?){0,62})[a-zA-Z0-9])+)\.([a-zA-Z0-9]{2,6})$", RegexOptions.Compiled);
        }
        public EmailValidationRule(Expression<Func<TModel, TField>> property)
            : base(property)
        {
        }

        public override ValidationError Validate(TModel model)
        {
            var propertyVal = GetRawValue(model);
            if (propertyVal == null)
            {
                return InvalidModelState();
            }

            if (EmailExp.IsMatch(propertyVal.ToString()))
            {
                return null;
            }

            return new ValidationError(PropertyInfo, "Invalid email address specified.");
        }
    }
}