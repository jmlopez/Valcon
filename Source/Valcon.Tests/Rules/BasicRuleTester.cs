using NUnit.Framework;
using Valcon.Registration.Graph;
using Valcon.Rules;

namespace Valcon.Tests.Rules
{
    [TestFixture]
    public class BasicRuleTester
    {
        private FakeValidationRule _ruleToTest;
        private Accessor _accessor;
        [SetUp]
        public void BeforeEach()
        {
            _accessor = Accessor.For<ModelToTest>(m => m.MyProperty);
            _ruleToTest = new FakeValidationRule(_accessor);
        }

        [Test]
        public void access_is_set_properly()
        {
            _ruleToTest.Accessor.ShouldEqual(_accessor);
        }

        [Test]
        public void invalid_model_state_error_is_returned_if_the_model_is_not_the_proper_type()
        {
            _ruleToTest
                .Validate(new ClassToValidate())
                .ErrorMessage
                .ShouldBeTheSameAs("Invalid model state. Model cannot be null");
        }

        [Test]
        public void accessor_is_set_on_validation_error()
        {
            _ruleToTest
                .Error("My Error")
                .Accessor
                .ShouldEqual(_accessor);
        }

        [Test]
        public void validate_is_called_on_derived_type_when_model_is_not_null_and_of_the_right_type()
        {
            _ruleToTest
                .Validate(new ModelToTest())
                .ErrorMessage
                .ShouldBeTheSameAs("Test");
        }

        #region Nested Types
        private class FakeValidationRule : BasicValidationRule<ModelToTest>
        {
            public FakeValidationRule(Accessor accessor) : base(accessor)
            {
            }

            public override ValidationError Validate(ModelToTest model)
            {
                return Error("Test");
            }
        }

        private class ModelToTest
        {
            public string MyProperty { get; set; }
        }
        #endregion
    }
}