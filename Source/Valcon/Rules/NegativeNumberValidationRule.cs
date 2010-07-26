using System;
using System.Linq.Expressions;

namespace Valcon.Rules
{
    public class NegativeNumberValidationRule<TModel, TField> : BasicValidationRule<TModel, TField>
       where TModel : class
    {
        public NegativeNumberValidationRule(Expression<Func<TModel, TField>> property)
            : base(property)
        {
        }

        public override ValidationError Validate(TModel model)
        {
            //TODO -- Figure out how this is being used
            return null;
        }
    }
}