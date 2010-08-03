using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Valcon.Registration.Graph
{
    public class Accessor
    {
        public Accessor(Type modelType, PropertyInfo property)
        {
            ModelType = modelType;
            Property = property;
        }

        public Type ModelType { get; private set; }
        public PropertyInfo Property { get; private set; }

        public static Accessor For<TModel>(Expression<Func<TModel, object>> expression)
        {
            return expression.ToAccessor();
        }
    }
}