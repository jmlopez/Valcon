using System;

namespace Valcon.Conventions
{
    public class LambdaGraphModifier : IGraphModifier
    {
        private readonly Action<ValidationGraph> _action;

        public LambdaGraphModifier(Action<ValidationGraph> action)
        {
            _action = action;
        }

        public void Modify(ValidationGraph graph)
        {
            _action(graph);
        }
    }
}