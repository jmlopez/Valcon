using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Valcon.Rules
{
    public class PhoneNumberValidationRule<TModel, TField> : BasicValidationRule<TModel, TField>
       where TModel : class
    {
        private static readonly Regex PhoneNumberExp;
        static PhoneNumberValidationRule()
        {
            PhoneNumberExp = new Regex("[0-9]{10}", RegexOptions.Compiled);
        }
        public PhoneNumberValidationRule(Expression<Func<TModel, TField>> property)
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

            if(PhoneNumberExp.IsMatch(propertyVal.ToString()))
            {
                return null;
            }

            return new ValidationError(PropertyInfo, "Invalid phone number specified.");
        }
    }
}