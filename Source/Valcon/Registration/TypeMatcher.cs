using System;
using System.Collections.Generic;
using FubuCore.Util;

namespace Valcon.Registration
{
    public class TypeMatcher : ITypeMatcher
    {
        private readonly CompositeFilter<Type> _typeFilters = new CompositeFilter<Type>();
        public CompositeFilter<Type> TypeFilters { get { return _typeFilters; } }

        public void EachType(TypePool types, Action<Type> action)
        {
            types
                .TypesMatching(TypeFilters.Matches)
                .Each(action);
        }
    }
}