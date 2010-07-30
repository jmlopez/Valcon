using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Valcon.Rules
{
    public class CompareValidationRule<TModel, TField>
    {
        private readonly Expression<Func<TModel, TField>> _propertyOne;
        private readonly Expression<Func<TModel, TField>> _propertyTwo;
        protected readonly PropertyInfo _propertyInfo;

        public CompareValidationRule(Expression<Func<TModel, TField>> propertyOne, Expression<Func<TModel, TField>> propertyTwo)
        {
            _propertyOne = propertyOne;
            _propertyTwo = propertyTwo;
        }
    }
}