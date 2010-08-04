using System;

namespace Valcon.Registration.Graph
{
    public class RuleDef
    {
        public RuleDef()
        {
            Name = Guid.NewGuid().ToString();
        }

        public RuleDef(Type type)
            : this()
        {
            Type = type;
        }

        public string Name { get; set; }
        public Type Type { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}, Type: {1}", Name, Type);
        }
    }
}