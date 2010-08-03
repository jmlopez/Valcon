using System;

namespace Valcon.Registration.Graph
{
    public interface IDependency
    {
        Type DependencyType { get; }
        void AcceptVisitor(IDependencyVisitor visitor);
    }
}