using System;
using System.Collections.Generic;
using FubuCore;
using Valcon.Registration;
using Valcon.Registration.Dsl;

namespace Valcon
{
    public class ValidationRegistry
    {
        private readonly TypePool _types = new TypePool();
        private readonly ModelMatcher _modelMatcher = new ModelMatcher();
        private readonly ExtensionTypeMatcher _extensionMatcher = new ExtensionTypeMatcher();
        private readonly List<IConfigurationAction> _conventions = new List<IConfigurationAction>();
        private readonly IList<IConfigurationAction> _policies = new List<IConfigurationAction>();

        public AppliesToExpression Applies { get { return new AppliesToExpression(_types); } }
        public TypeCandidateExpression Models { get { return new TypeCandidateExpression(_modelMatcher, _types); }}
        public RulesExpression Rules { get { return new RulesExpression(_policies); } }
        public ExtensionCandidateExpression Extensions { get { return new ExtensionCandidateExpression(_types, _extensionMatcher); } }

        public ValidationRegistry()
        {
            AddConvention(graph => _modelMatcher.BuildChains(_types, graph));
        }

        public ValidationRegistry(Action<ValidationRegistry> configure)
             : this()
        {
            configure(this);
        }

        public void AddConvention(Action<ValidationGraph> action)
        {
            _conventions.Add(new LambdaConfigurationAction(action));
        }

        public void ApplyConvention<TConvention>()
           where TConvention : IConfigurationAction, new()
        {
            ApplyConvention(new TConvention());
        }

        public void ApplyConvention<TConvention>(TConvention convention)
            where TConvention : IConfigurationAction
        {
            _conventions.Add(convention);
        }

        public ValidationGraph BuildGraph()
        {
            _extensionMatcher.EachType(_types, type => type
                                                           .GetDefaultInstance()
                                                           .As<IValidationRegistryExtension>()
                                                           .Configure(this));

            var graph = new ValidationGraph();

            _conventions.Configure(graph);
            _policies.Configure(graph);

            return graph;
        }
    }
}