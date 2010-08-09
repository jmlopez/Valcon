using Valcon.HelloWorld.Models.Users;

namespace Valcon.HelloWorld.Endpoints.Users
{
    public class AddEndpoint
    {
        public UserInputModel Get()
        {
            return new UserInputModel();
        }
    }
}