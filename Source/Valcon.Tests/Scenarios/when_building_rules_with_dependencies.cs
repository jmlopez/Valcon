using System;
using NUnit.Framework;
using Rhino.Mocks;
using Valcon.Registration.Graph;
using Valcon.Util;

namespace Valcon.Tests.Scenarios
{
    [TestFixture]
    public class when_building_rules_with_dependencies
    {
        #region Setup
        private RuleBuilder _classUnderTest;
        private Cache<Type, object> _serviceLocatorCache;
        private MockRepository _mockRepository;
        [TestFixtureSetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository();
            _serviceLocatorCache = new Cache<Type, object>
                                       {
                                           OnMissing = t => _mockRepository.StrictMock(t, null)
                                       };
            _classUnderTest = new RuleBuilder(t => _serviceLocatorCache[t]);
            _mockRepository.Record();
        }

        private TService MockFor<TService>()
        {
            return (TService) _serviceLocatorCache[typeof (TService)];
        }

        private void VerifyCallsFor<TService>()
        {
            MockFor<TService>()
                .VerifyAllExpectations();
        }

        private RuleBuilder ClassUnderTest
        {
            get
            {
                _mockRepository.ReplayAll();
                return _classUnderTest;
            }
        }
        #endregion

        [Test]
        public void dependencies_are_resolved_via_func()
        {
            var user = new User {Username = "test"};
            
            MockFor<IUserRepository>()
                .Expect(r => r.UsernameIsUnique(user.Username))
                .Return(false);

            ClassUnderTest
                .Build(new ValidationCall(typeof(UsernameIsUniqueValidationRule), Accessor.For<User>(u => u.Username)))
                .Validate(user)
                .ShouldNotBeNull();

            VerifyCallsFor<IUserRepository>();
        }


        #region Nested Type: IUserRepository
        public interface IUserRepository
        {
            bool UsernameIsUnique(string username);
        }
        #endregion

        #region Nested Type: UsernameIsUniqueValidationRule
        public class UsernameIsUniqueValidationRule : IValidationRule
        {
            private readonly IUserRepository _userRepository;
            private readonly Accessor _accessor;
            public UsernameIsUniqueValidationRule(IUserRepository userRepository)
            {
                _userRepository = userRepository;
                _accessor = Accessor.For<User>(u => u.Username);
            }

            public ValidationError Validate(object model)
            {
                var user = model as User;
                if(user == null || _userRepository.UsernameIsUnique(user.Username))
                {
                    return null;
                }

                return new ValidationError(_accessor, "Username is in use.");
            }
        }
        #endregion

        #region Nested Type: User
        public class User
        {
            public string Username { get; set; }
        }
        #endregion
    }
}