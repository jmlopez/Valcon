using Valcon.Registration.Graph;
using Valcon.Rules;

namespace Valcon
{
    public interface IRuleCompiler
    {
        IValidationRule Compile(RuleDef def);
    }
}