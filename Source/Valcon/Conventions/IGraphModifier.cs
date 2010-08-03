using Valcon.Registration;

namespace Valcon.Conventions
{
    public interface IGraphModifier
    {
        void Modify(ValidationGraph graph);
    }
}