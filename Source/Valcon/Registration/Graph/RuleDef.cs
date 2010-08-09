using System;
using System.Collections.Generic;

namespace Valcon.Registration.Graph
{
    public class RuleDef
    {
        public RuleDef()
        {
            Name = Guid.NewGuid().ToString();
            Dependencies = new List<ValueDependency>();
        }

        public RuleDef(Type type)
            : this()
        {
            Type = type;
        }

        public string Name { get; set; }
        public Type Type { get; set; }
        public IList<ValueDependency> Dependencies { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}, Type: {1}", Name, Type);
        }
    }
}