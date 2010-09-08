using NUnit.Framework;
using Valcon.Registration.Dsl;

namespace Valcon.Tests
{
    [TestFixture]
    public class when_validating_an_object
    {
        [TestFixtureSetUp]
        public void BeforeAll()
        {
            Validator.Initialize(new ValidationTestRegistry());
        }

        [Test]
        public void no_errors_are_returned_if_the_object_is_valid()
        {
            var validModel = new ClassToValidate
                            {
                                SimpleRequiredField = "Valid value"
                            };

            Validator
                .Validate(validModel)
                .IsValid
                .ShouldBeTrue();
        }

        [Test]
        public void errors_are_returned_when_the_object_is_invalid()
        {
            var invalidModel = new ClassToValidate();

            Validator
                .Validate(invalidModel)
                .IsValid
                .ShouldBeFalse();
        }

        public class ValidationTestRegistry : ValidationRegistry
        {
            public ValidationTestRegistry()
            {
                Rules
                    .For<ClassToValidate>()
                    .Require(c => c.SimpleRequiredField);
            }
        }
    }
}