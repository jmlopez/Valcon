namespace Valcon.HelloWorld.Models.Users
{
    public class UserInputModel : UserDetailsModel
    {
        // Registry specifies that these are required
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}