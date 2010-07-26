using System.Diagnostics;
using NUnit.Framework;
using Valcon.Registration.Dsl;

namespace Valcon.Tests.Dsl
{
    [TestFixture]
    public class RegistryTester
    {
        [Test]
        public void Blah()
        {
            Validator.Initialize(x => x.AddRegistry<ContactInfoRegistry>());
        }
    }

    public class ContactInfoRegistry : ValidationRegistry
    {
        public ContactInfoRegistry()
        {
            For<CustomerContactInfoDetailsModel>()
                .Require(c => c.FirstName)
                .Require(c => c.LastName);

            For<VendorContactInfoDetailsModel>()
                .Require(c => c.FirstName);
        }
    }

    public class ContactInfoDetailsModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CustomerContactInfoDetailsModel : ContactInfoDetailsModel
    {
    }

    public class VendorContactInfoDetailsModel : ContactInfoDetailsModel
    {
    }
}