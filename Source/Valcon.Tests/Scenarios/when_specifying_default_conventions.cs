using NUnit.Framework;
using Valcon.Registration.Dsl;
using Valcon.Rules;
using Valcon.Tests.Scenarios.Models;

namespace Valcon.Tests.Scenarios
{
    [TestFixture]
    public class when_specifying_default_conventions
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            Validator.Initialize(x => x.AddRegistry<ConventionTestRegistry>());
        }

        [Test]
        public void calls_only_get_applied_to_types_specified_in_scan()
        {
            Validator
                .FindChain<InapplicableModel>()
                .ShouldBeEmpty();
        }

        [Test]
        public void conventions_only_apply_to_their_respective_filters()
        {
            Validator
                .FindChain<UserDetailsModel>()
                .CallsFor(m => m.EmailAddress)
                .ShouldHaveCount(1)
                .ShouldContain(c => typeof(EmailValidationRule<>).IsAssignableFrom(c.RuleType.GetGenericTypeDefinition()));
        }

        #region Nested Types
        public class InapplicableModel
        {
            public string Email { get; set; }
        }

        public class ConventionTestRegistry : ValidationRegistry
        {
            public ConventionTestRegistry()
            {
                Scan(x =>
                         {
                             x.TheCallingAssembly();
                             x.IncludeNamespaceContainingType<ModelMarker>();
                             x.ExcludeType<ModelMarker>();

                             x.ByDefault
                                 .IfProperty(p => p.Name.Contains("Email"))
                                 .IsEmail();

                             x.ByDefault
                                 .IfProperty(p => p.Name.Contains("Phone"))
                                 .IsPhoneNumber();
                         });
            }
        }
        #endregion
    }
}