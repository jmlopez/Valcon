using System;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;

namespace Valcon.HelloWorld.Configuration.Behaviors
{
    public class CrudValidationBehaviorNode: BehaviorNode
    {
        private readonly Type _inputType;

        public CrudValidationBehaviorNode(Type inputType)
        {
            _inputType = inputType;
        }

        protected override ObjectDef buildObjectDef()
        {
            return new ObjectDef
                       {
                           Type = typeof(ValidateInputModelBehavior<>).MakeGenericType(_inputType)
                       };
        }

        public override BehaviorCategory Category
        {
            get { return BehaviorCategory.Wrapper; }
        }
    }
}