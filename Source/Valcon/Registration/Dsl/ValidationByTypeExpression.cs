using System;
using System.Collections.Generic;
using Valcon.Registration.Graph;

namespace Valcon.Registration.Dsl
{
    public class ValidationByTypeExpression
    {
        private readonly Type _modelType;
        private readonly IList<IConfigurationAction> _policies;

        public ValidationByTypeExpression(Type modelType, IList<IConfigurationAction> policies)
        {
            _modelType = modelType;
            _policies = policies;
        }

        public ValidationByTypeExpression AddCall(ValidationCall call)
        {
            _policies.AddAction(graph => graph
                                             .FindChain(_modelType)
                                             .AddCall(call));
            return this;
        }
    }

    public class ValidationByTypeExpression<T> : ValidationByTypeExpression
        where T : class
    {
        public ValidationByTypeExpression(IList<IConfigurationAction> policies) 
            : base(typeof(T), policies)
        {
        }

        public new ValidationByTypeExpression<T> AddCall(ValidationCall call)
        {
            base.AddCall(call);
            return this;
        }
    }
}