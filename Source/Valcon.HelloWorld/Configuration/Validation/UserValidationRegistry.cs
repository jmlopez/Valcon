using Valcon.HelloWorld.Models.Users;
using Valcon.Registration.Dsl;

namespace Valcon.HelloWorld.Configuration.Validation
{
    public class UserValidationRegistry : ValidationRegistry
    {
        public UserValidationRegistry()
        {
            // example of how you can mix and match validation approaches 
            For<UserInputModel>()
                .Require(u => u.Password)
                .Require(u => u.ConfirmPassword)
                .Compare(u => u.Password, u => u.ConfirmPassword);
        }
    }
}