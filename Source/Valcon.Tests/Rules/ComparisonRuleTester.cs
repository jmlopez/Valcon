using NUnit.Framework;

namespace Valcon.Tests.Rules
{
    public class ComparisonRuleTester
    {
        private UserSignupModel _classUnderTest;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            Validator.Initialize(x => x.For<UserSignupModel>().Compare(u => u.Password, u => u.ConfirmPassword));
        }

        [SetUp]
        public void BeforeEach()
        {
            _classUnderTest = new UserSignupModel();
        }

        [Test]
        public void returns_error_when_values_are_not_equal()
        {
            _classUnderTest.Password = "123";
            _classUnderTest.ConfirmPassword = "asdf";

            Validator
                .Validate(_classUnderTest)
                .IsValid
                .ShouldBeFalse();
        }

        [Test]
        public void returns_null_when_values_are_equal()
        {
            _classUnderTest.Password = "123";
            _classUnderTest.ConfirmPassword = "123";

            Validator
                .Validate(_classUnderTest)
                .IsValid
                .ShouldBeTrue();
        }

        #region Nested Type: UserSignupModel
        public class UserSignupModel
        {
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }
        #endregion
    }
}