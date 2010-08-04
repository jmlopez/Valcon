using System.Linq;
using Valcon.HelloWorld.Domain;
using Valcon.HelloWorld.Infrastructure;
using Valcon.HelloWorld.Models.Users;
using Valcon.HelloWorld.Repositories;

namespace Valcon.HelloWorld.Endpoints.Users
{
    public class ListEndpoint
    {
        private readonly IUserRepository _userRepository;
        private readonly IMappingRegistry _mappingRegistry;

        public ListEndpoint(IUserRepository userRepository, IMappingRegistry mappingRegistry)
        {
            _userRepository = userRepository;
            _mappingRegistry = mappingRegistry;
        }

        public UserListModel Get()
        {
            var users = from u in _userRepository.FindAll()
                        select _mappingRegistry.Map<User, UserDetailsModel>(u);

            return new UserListModel
                       {
                           Users = users.ToList()
                       };
        }
    }
}