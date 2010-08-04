using NUnit.Framework;

namespace Valcon.Tests
{
    [TestFixture]
    public class InitializationExpressionTester
    {
        private InitializationExpression _classUnderTest;
        [SetUp]
        public void BeforeEach()
        {
            _classUnderTest = new InitializationExpression();
        }
        [Test]
        public void service_locator_defaults_to_returning_null()
        {
            // making sure we don't get a null reference exception
            _classUnderTest.ServiceLocator(GetType()).ShouldBeNull();
        }
        [Test]
        public void sets_the_service_locator()
        {
            const string value = "Test";
            _classUnderTest.BuildDependenciesWith(t => value);
            _classUnderTest.ServiceLocator(GetType()).ShouldBeTheSameAs(value);
        }
        [Test]
        public void building_the_graph_calls_configure_graph_on_each_registry()
        {
            var visited = false;
            _classUnderTest.AddExpression(graph => visited = true);
            _classUnderTest.BuildGraph();
            visited.ShouldBeTrue();
        }
    }
}