using System;

namespace Valcon.Registration.Graph
{
    public class ValueDependency
    {
        public object Value { get; set; }

        public Type DependencyType { get; set; }

        public override string ToString()
        {
            return string.Format("DependencyType: {1}, Value: {0}", Value, DependencyType);
        }
    }
}