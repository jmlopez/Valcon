using System;

namespace Valcon.Registration
{
    public class LambdaConfigurationAction : IConfigurationAction
    {
        private readonly Action<ValidationGraph> _action;

        public LambdaConfigurationAction(Action<ValidationGraph> action)
        {
            _action = action;
        }

        public void Configure(ValidationGraph graph)
        {
            _action(graph);
        }
    }
}