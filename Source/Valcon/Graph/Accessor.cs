using System;
using System.Reflection;

namespace Valcon.Graph
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
    }
}