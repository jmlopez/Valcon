using System;
using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using Valcon.HelloWorld.Models;

namespace Valcon.HelloWorld.Configuration.Behaviors
{
    public class CrudErrorWrapperBehavior : BasicBehavior
    {
        private readonly IFubuRequest _fubuRequest;
        private readonly IPartialFactory _partialFactory;
        public CrudErrorWrapperBehavior(IFubuRequest fubuRequest, IPartialFactory partialFactory) 
            : base(PartialBehavior.Executes)
        {
            _partialFactory = partialFactory;
            _fubuRequest = fubuRequest;
        }

        protected override DoNext performInvoke()
        {
            if(InsideBehavior != null)
            {
                try
                {
                    InsideBehavior.InvokePartial();
                }
                catch (Exception exc)
                {
                    var response = new AjaxResponse
                                        {
                                            Success = false,
                                            Errors = new List<string> { exc.Message }
                                        };
                    _fubuRequest.Set(response);

                    _partialFactory
                        .BuildPartial(response.GetType())
                        .Invoke();
                }
                    
            }

            return DoNext.Stop;
        }
    }
}