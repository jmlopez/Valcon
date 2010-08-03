using System;
using System.Text.RegularExpressions;
using Valcon.Registration.Graph;

namespace Valcon.Rules
{
    public class PhoneNumberValidationRule<TModel> : BasicValidationRule<TModel>
       where TModel : class
    {
        private static readonly Regex PhoneNumberExp;
        static PhoneNumberValidationRule()
        {
            PhoneNumberExp = new Regex("[0-9]{10}", RegexOptions.Compiled);
        }


        public PhoneNumberValidationRule(Accessor accessor) : base(accessor)
        {
        }

        public override ValidationError Validate(TModel model)
        {
            //var propertyVal = GetRawValue(model);
            //if (propertyVal == null)
            //{
            //    return InvalidModelState();
            //}

            //if(PhoneNumberExp.IsMatch(propertyVal.ToString()))
            //{
            //    return null;
            //}

            //return new ValidationError(PropertyInfo, "Invalid phone number specified.");

            //TODO
            throw new NotImplementedException();
        }
    }
}