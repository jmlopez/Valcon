using Valcon.Attributes;

namespace Valcon.Tests
{
    public class ClassToValidate
    {
        public int SimpleRequiredId { get; set; }
        public string SimpleRequiredField { get; set; }
        public string AnotherSimpleRequiredField { get; set; }
        [Required]
        public string RequiredFromAttribute { get; set; }
    }
}