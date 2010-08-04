using System.Collections.Generic;
using Valcon.HelloWorld.Domain;

namespace Valcon.HelloWorld.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> FindAll();
    }

    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> FindAll()
        {
            return new List<User>
                       {
                           new User { EmailAddress = "test1@valcon.com", FirstName = "Test", LastName = "User 1"},
                           new User { EmailAddress = "test2@valcon.com", FirstName = "Test", LastName = "User 2"},
                           new User { EmailAddress = "test3@valcon.com", FirstName = "Test", LastName = "User 3"},
                           new User { EmailAddress = "test4@valcon.com", FirstName = "Test", LastName = "User 4"},
                           new User { EmailAddress = "test5@valcon.com", FirstName = "Test", LastName = "User 5"}
                       };
        }
    }
}