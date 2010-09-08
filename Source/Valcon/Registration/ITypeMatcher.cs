using System;
using FubuCore.Util;

namespace Valcon.Registration
{
    public interface ITypeMatcher
    {
        CompositeFilter<Type> TypeFilters { get; }
    }
}