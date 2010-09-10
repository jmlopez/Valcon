using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Valcon.Tests.Rules
{
    [TestFixture]
    public class RequiredRuleTester
    {
        #region Setup
        private RequiredFieldModel _classUnderTest;
        
        [TestFixtureSetUp]
        public void BeforeAll()
        {
            Validator.Initialize(new RequiredRegistry());
        }

        [SetUp]
        public void BeforeEach()
        {
            _classUnderTest = new RequiredFieldModel();
        }
        #endregion

        [Test]
        public void string_is_not_valid_if_not_set()
        {
            var errors = Validate();
            errors.Where(e => e.Accessor.Property.Name == "SimpleString").ShouldHaveCount(1);
        }

        [Test]
        public void string_is_not_valid_if_empty()
        {
            _classUnderTest.SimpleString = string.Empty;
            var errors = Validate();
            errors.Where(e => e.Accessor.Property.Name == "SimpleString").ShouldHaveCount(1);
        }

        [Test]
        public void string_is_valid_if_not_empty()
        {
            _classUnderTest.SimpleString = "Test";
            var errors = Validate();
            errors.Where(e => e.Accessor.Property.Name == "SimpleString").ShouldHaveCount(0);
        }

        [Test]
        public void reference_type_is_not_valid_if_null()
        {
            var errors = Validate();
            errors.Where(e => e.Accessor.Property.Name == "Child").ShouldHaveCount(1);
        }

        [Test]
        public void reference_type_is_valid_if_not_null()
        {
            _classUnderTest.Child = new RequiredFieldModel();
            var errors = Validate();
            errors.Where(e => e.Accessor.Property.Name == "Child").ShouldHaveCount(0);
        }

        private IEnumerable<ValidationError> Validate()
        {
            return Validator.Validate(_classUnderTest).Errors;
        }

        #region Nested Types
        public class RequiredFieldModel
        {
            public string SimpleString { get; set; }
            public RequiredFieldModel Child { get; set; }
        }

        private class RequiredRegistry : ValidationRegistry
        {
            public RequiredRegistry()
            {
                Applies
                    .ToThisAssembly();

                Models
                    .IncludedTypesInNamespaceContaining<RequiredRuleTester>()
                    .IncludeTypes(t => t.Name.EndsWith("Model"));

                Rules
                    .IfProperty(p => !p.PropertyType.IsPrimitive, validate => validate.AsRequired());
            }
        }
        #endregion
    }

    
}