using Valcon.Attributes;

namespace Valcon.HelloWorld.Models.Users
{
    public class UserDetailsModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}