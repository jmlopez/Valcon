using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using Valcon.HelloWorld.Models;

namespace Valcon.HelloWorld.Configuration.Behaviors
{
    public class ValidateInputModelBehavior<TViewModel> : BasicBehavior
        where TViewModel : class
    {
        private readonly IFubuRequest _fubuRequest;
        private readonly IPartialFactory _partialFactory;
        private readonly IValidationProvider _validationProvider;
        public ValidateInputModelBehavior(IValidationProvider validationProvider, IFubuRequest fubuRequest, IPartialFactory partialFactory) 
            : base(PartialBehavior.Executes)
        {
            _validationProvider = validationProvider;
            _partialFactory = partialFactory;
            _fubuRequest = fubuRequest;
        }

        protected override DoNext performInvoke()
        {
            if(InsideBehavior != null)
            {
                var inputModel = _fubuRequest.Get<TViewModel>();
                var summary = _validationProvider.Validate(inputModel);
                if(!summary.IsValid)
                {
                    var response = new AjaxResponse
                                       {
                                           Success = false,
                                           Errors = summary.Errors.Select(e => e.ErrorMessage).ToList()
                                       };
                    _fubuRequest.Set(response);

                    _partialFactory
                        .BuildPartial(response.GetType())
                        .Invoke();
                }
                else
                {
                    InsideBehavior.Invoke();
                }
            }

            return DoNext.Stop;
        }
    }
}