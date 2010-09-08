using Valcon.HelloWorld.Models.Users;

namespace Valcon.HelloWorld.Configuration.Validation
{
    public class UserValidationExtension : IValidationRegistryExtension
    {
        public void Configure(ValidationRegistry registry)
        {
            registry
                .Rules
                .For<UserInputModel>()
                .Require(u => u.Password)
                .Require(u => u.ConfirmPassword)
                .Compare(u => u.Password, u => u.ConfirmPassword);
        }
    }
}