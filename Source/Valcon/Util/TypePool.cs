using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Valcon.Util
{
    public class TypePool
    {
        private readonly Cache<Assembly, Type[]> _types = new Cache<Assembly, Type[]>();

        public TypePool()
        {
            _types.OnMissing = assembly =>
                                   {
                                       try
                                       {
                                           return assembly.GetExportedTypes();
                                       }
                                       catch (Exception)
                                       {
                                           return new Type[0];
                                       }
                                   };
        }

        public IEnumerable<Type> For(IEnumerable<Assembly> assemblies, CompositeFilter<Type> filter)
        {
            return For(assemblies).Where(filter.Matches);
        }

        public IEnumerable<Type> For(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(x => _types[x]);
        }
    }
}