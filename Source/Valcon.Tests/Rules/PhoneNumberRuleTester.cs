using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Valcon.Tests.Rules
{
    [TestFixture]
    public class PhoneNumberRuleTester
    {
        #region Setup
        private PhoneNumberFieldModel _classUnderTest;
        
        [TestFixtureSetUp]
        public void BeforeAll()
        {
            Validator.Initialize(x => x.For<PhoneNumberFieldModel>().PhoneNumber(m => m.PhoneNumber));
        }

        [SetUp]
        public void BeforeEach()
        {
            _classUnderTest = new PhoneNumberFieldModel();
        }
        #endregion

        [Test]
        public void invalid_phone_number()
        {
            _classUnderTest.PhoneNumber = "123";
            var errors = Validate().Errors;
            errors.Where(e => e.Accessor.Property.Name == "PhoneNumber").ShouldHaveCount(1);
        }


        [Test]
        public void valid_phone_number()
        {
            _classUnderTest.PhoneNumber = "5123400340";
            var errors = Validate().Errors;
            errors.Where(e => e.Accessor.Property.Name == "PhoneNumber").ShouldHaveCount(0);
        }

        private ValidationSummary Validate()
        {
            return Validator.Validate(_classUnderTest);
        }

        #region Nested Types
        public class PhoneNumberFieldModel
        {
            public string PhoneNumber { get; set; }
        }
        #endregion
    }

    
}