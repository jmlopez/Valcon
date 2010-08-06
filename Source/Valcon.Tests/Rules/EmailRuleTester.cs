using System.Linq;
using NUnit.Framework;

namespace Valcon.Tests.Rules
{
    [TestFixture]
    public class EmailRuleTester
    {
        #region Setup
        private EmailFieldModel _classUnderTest;
        
        [TestFixtureSetUp]
        public void BeforeAll()
        {
            Validator.Initialize(x => x.For<EmailFieldModel>().Email(m => m.Email));
        }

        [SetUp]
        public void BeforeEach()
        {
            _classUnderTest = new EmailFieldModel();
        }
        #endregion

        [Test]
        public void invalid_email()
        {
            _classUnderTest.Email = "!@#$!@#$";
            var errors = Validate().Errors;
            errors.Where(e => e.Accessor.Property.Name == "Email").ShouldHaveCount(1);
        }


        [Test]
        public void valid_email()
        {
            _classUnderTest.Email = "user@domain.com";
            var errors = Validate().Errors;
            errors.Where(e => e.Accessor.Property.Name == "Email").ShouldHaveCount(0);
        }

        private ValidationSummary Validate()
        {
            return Validator.Validate(_classUnderTest);
        }

        #region Nested Types
        public class EmailFieldModel
        {
            public string Email { get; set; }
        }
        #endregion
    }

    
}