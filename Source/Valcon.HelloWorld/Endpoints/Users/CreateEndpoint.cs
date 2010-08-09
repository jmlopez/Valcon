using Valcon.HelloWorld.Domain;
using Valcon.HelloWorld.Infrastructure;
using Valcon.HelloWorld.Models;
using Valcon.HelloWorld.Models.Users;

namespace Valcon.HelloWorld.Endpoints.Users
{
    public class CreateEndpoint
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEndpoint(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AjaxResponse Post(UserInputModel inputModel)
        {
            var newUser = new User
                              {
                                  FirstName = inputModel.FirstName,
                                  LastName = inputModel.LastName,
                                  EmailAddress = inputModel.EmailAddress,
                                  Password = inputModel.Password
                              };
            _unitOfWork.Insert(newUser);
            _unitOfWork.Commit();

            return new AjaxResponse
                       {
                           Success = true,
                           Message = "User created successfully"
                       };
        }
    }
}