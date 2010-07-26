using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Valcon.Conventions;
using Valcon.Util;

namespace Valcon.Registration.Dsl
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
            return assemblies.SelectMany(x => _types[x].Where(filter.Matches));
        }
    }

    public class AssemblyScanner : IAssemblyScanner
    {
        private readonly List<Assembly> _assemblies = new List<Assembly>();
        private readonly List<IRegistrationConvention> _conventions = new List<IRegistrationConvention>();
        private readonly CompositeFilter<Type> _filter = new CompositeFilter<Type>();
        private readonly List<IGraphModifier> _graphModifiers = new List<IGraphModifier>();
        private readonly ConfigureDefaultValidationExpression _defaultValidationExpression = new ConfigureDefaultValidationExpression();

        public int Count { get { return _assemblies.Count; } }

        #region Designating Assemblies
        public void Assembly(Assembly assembly)
        {
            if (!_assemblies.Contains(assembly))
            {
                _assemblies.Add(assembly);
            }
        }

        public void Assembly(string assemblyName)
        {
            Assembly(AppDomain.CurrentDomain.Load(assemblyName));
        }

        public void AssembliesFromApplicationBaseDirectory()
        {
            AssembliesFromApplicationBaseDirectory(a => true);
        }

        public void AssembliesFromApplicationBaseDirectory(Predicate<Assembly> assemblyFilter)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            AssembliesFromPath(baseDirectory, assemblyFilter);
            string binPath = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
            if (Directory.Exists(binPath))
            {
                AssembliesFromPath(binPath, assemblyFilter);
            }
        }

        public void AssembliesFromPath(string path)
        {
            AssembliesFromPath(path, a => true);
        }

        public void AssembliesFromPath(string path, Predicate<Assembly> assemblyFilter)
        {
            IEnumerable<string> assemblyPaths = Directory.GetFiles(path)
                .Where(file =>
                       Path.GetExtension(file).Equals(
                           ".exe",
                           StringComparison.OrdinalIgnoreCase)
                       ||
                       Path.GetExtension(file).Equals(
                           ".dll",
                           StringComparison.OrdinalIgnoreCase));

            foreach (string assemblyPath in assemblyPaths)
            {
                Assembly assembly = null;
                try
                {
                    assembly = System.Reflection.Assembly.LoadFrom(assemblyPath);
                }
                catch
                {
                }
                if (assembly != null && assemblyFilter(assembly)) Assembly(assembly);
            }
        }

        public void TheCallingAssembly()
        {
            Assembly callingAssembly = findTheCallingAssembly();

            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
        }

        private static Assembly findTheCallingAssembly()
        {
            var trace = new StackTrace(false);

            Assembly thisAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Assembly callingAssembly = null;
            for (int i = 0; i < trace.FrameCount; i++)
            {
                StackFrame frame = trace.GetFrame(i);
                Assembly assembly = frame.GetMethod().DeclaringType.Assembly;
                if (assembly != thisAssembly)
                {
                    callingAssembly = assembly;
                    break;
                }
            }
            return callingAssembly;
        }

        public void AssemblyContainingType<T>()
        {
            _assemblies.Add(typeof(T).Assembly);
        }

        public void AssemblyContainingType(Type type)
        {
            _assemblies.Add(type.Assembly);
        }

        #endregion

        #region Filtering Types
        public void Exclude(Func<Type, bool> exclude)
        {
            _filter.Excludes += exclude;
        }

        public void ExcludeNamespace(string nameSpace)
        {
            Exclude(type => type.IsInNamespace(nameSpace));
        }

        public void ExcludeNamespaceContainingType<T>()
        {
            ExcludeNamespace(typeof(T).Namespace);
        }

        public void Include(Func<Type, bool> predicate)
        {
            _filter.Includes += predicate;
        }

        public void IncludeNamespace(string nameSpace)
        {
            Include(type => type.IsInNamespace(nameSpace));
        }

        public void IncludeNamespaceContainingType<T>()
        {
            IncludeNamespace(typeof(T).Namespace);
        }

        public void ExcludeType<T>()
        {
            Exclude(type => type == typeof(T));
        }
        #endregion

        #region Conventions
        public void Convention<T>() where T : IRegistrationConvention, new()
        {
            IRegistrationConvention previous = _conventions.FirstOrDefault(scanner => scanner is T);
            if (previous == null)
            {
                With(new T());
            }
        }

        public void With(IRegistrationConvention convention)
        {
            _conventions.Fill(convention);
        }

        public void ModifyGraphAfterScan(Action<ValidationGraph> action)
        {
            ModifyGraphAfterScan(new LambdaGraphModifier(action));
        }

        public void ModifyGraphAfterScan<T>() where T : IGraphModifier, new()
        {
           ModifyGraphAfterScan(new T()); 
        }

        public void ModifyGraphAfterScan(IGraphModifier modifer)
        {
            _graphModifiers.Add(modifer);
        }

        public ConfigureDefaultValidationExpression ByDefault
        {
            get { return _defaultValidationExpression; }
        }

        #endregion

        public void ScanForAll(ValidationGraph graph)
        {
            var registry = new ValidationRegistry();
            graph.Types.For(_assemblies, _filter).Each(type => _conventions.ForEach(c => c.Process(type, registry)));
            registry.ConfigureGraph(graph);

            _graphModifiers.ForEach(modifier => modifier.Modify(graph));
        }
    }
}