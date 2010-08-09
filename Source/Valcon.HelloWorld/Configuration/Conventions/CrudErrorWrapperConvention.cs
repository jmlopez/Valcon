using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Registration;
using Valcon.HelloWorld.Configuration.Behaviors;

namespace Valcon.HelloWorld.Configuration.Conventions
{
    public class CrudErrorWrapperConvention : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph
                .Actions()
                .Where(action => action.IsCrudAction())
                .Each(action => action.AddBefore(new CrudErrorWrapperBehaviorNode()));
        }
    }
}