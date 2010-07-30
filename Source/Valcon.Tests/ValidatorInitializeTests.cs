using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Valcon.Conventions;
using Valcon.Registration.Dsl;
using Valcon.Rules;

namespace Valcon.Tests
{
    [TestFixture]
    public class ValidatorInitializeTester
    {
        [Test]
        public void add_a_registry_by_generic_signature()
        {
            Validator.Initialize(x => x.AddRegistry<InitializeRegistry>());
            Validator
                .FindChain<ClassToValidate>()
                .Where(
                    r =>
                    r.GetType() ==
                    typeof (RequiredValidationRule<,>).MakeGenericType(typeof (ClassToValidate), typeof (string)))
                .ShouldHaveCount(2);

            var chain = Validator.FindChain<CreateUserInputModel>();
        }

        public class InitializeRegistry : ValidationRegistry
        {
            public InitializeRegistry()
            {
                For<ClassToValidate>()
                    .Require(c => c.SimpleRequiredField)
                    .Require(c => c.AnotherSimpleRequiredField);

                Scan(x =>
                         {
                             x.TheCallingAssembly();
                             x.IncludeNamespaceContainingType<ModelMarker>();
                             
                             x.UseValidationAttributes();
                             x.InheritValidationRules();

                             x.ByDefault
                                 .IfProperty(p => p.Name.Contains("Email"))
                                 .ConfigureRule(typeof (EmailValidationRule<,>));
                         });
            }
        }
    }

    public class ModelMarker
    {
        
    }

    public class CreateUserModel : CreateUserInputModel
    {
        public List<SecurityGroupModel> SecurityGroups { get; set; }
    }

    public class SecurityGroupModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }

    public class CreateUserInputModel
    {
        public int GroupId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}