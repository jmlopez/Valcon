namespace Valcon.HelloWorld.Models.Users
{
    public class UserInputModel : UserDetailsModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}