using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;

namespace Valcon.HelloWorld.Configuration.Behaviors
{
    public class CrudErrorWrapperBehaviorNode: BehaviorNode
    {
        protected override ObjectDef buildObjectDef()
        {
            return new ObjectDef
                       {
                           Type = typeof(CrudErrorWrapperBehavior)
                       };
        }

        public override BehaviorCategory Category
        {
            get { return BehaviorCategory.Wrapper; }
        }
    }
}