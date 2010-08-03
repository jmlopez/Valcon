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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Accessor)) return false;
            return Equals((Accessor) obj);
        }

        public bool Equals(Accessor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.ModelType, ModelType) && Equals(other.Property, Property);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ModelType != null ? ModelType.GetHashCode() : 0)*397) ^ (Property != null ? Property.GetHashCode() : 0);
            }
        }
    }
}