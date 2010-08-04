using System.Collections.Generic;

namespace Valcon.HelloWorld.Models.Users
{
    public class UserListModel
    {
        public UserListModel()
        {
            Users = new List<UserDetailsModel>();
        }

        public List<UserDetailsModel> Users { get; set; }
    }
}