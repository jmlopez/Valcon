using System;
using System.Collections.Generic;
using FubuCore.Util;

namespace Valcon.Registration
{
    public class ModelMatcher : ITypeMatcher
    {
        private readonly CompositeFilter<Type> _typeFilters = new CompositeFilter<Type>();
        public CompositeFilter<Type> TypeFilters { get { return _typeFilters; } }

        public void BuildChains(TypePool types, ValidationGraph graph)
        {
            types
                .TypesMatching(TypeFilters.Matches)
                .Each(type => graph.AddChain(ValidationChain.GenericForModel(type)));
        }
    }
}