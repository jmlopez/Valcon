using System;

namespace Valcon.Registration.Graph
{
    public class ConfiguredDependency : IDependency
    {
        public RuleDef Definition { get; set; }
        public Type DependencyType { get; set; }


        public void AcceptVisitor(IDependencyVisitor visitor)
        {
            visitor.Configured(this);
        }

        public override string ToString()
        {
            return string.Format("DependencyType: {0}, Definition: {1}", DependencyType, Definition);
        }
    }
}