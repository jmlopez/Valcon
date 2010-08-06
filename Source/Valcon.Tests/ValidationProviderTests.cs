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
            Validator.Initialize(x => x.AddRegistry<ValidationTestRegistry>());
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
                .ShouldBeEmpty();
        }

        [Test]
        public void errors_are_returned_when_the_object_is_invalid()
        {
            var invalidModel = new ClassToValidate();

            Validator
                .Validate(invalidModel)
                .ShouldNotBeEmpty();
        }

        public class ValidationTestRegistry : ValidationRegistry
        {
            public ValidationTestRegistry()
            {
                For<ClassToValidate>()
                    .Require(c => c.SimpleRequiredField);
            }
        }
    }
}