using System;
using System.Reflection;
using Valcon.Conventions;

namespace Valcon.Registration.Dsl
{
    public interface IAssemblyScanner
    {
        #region Designating Assemblies
        void Assembly(Assembly assembly);
        void Assembly(string assemblyName);
        void TheCallingAssembly();
        void AssemblyContainingType<T>();
        void AssemblyContainingType(Type type);
        void AssembliesFromPath(string path);
        void AssembliesFromPath(string path, Predicate<Assembly> assemblyFilter);
        void AssembliesFromApplicationBaseDirectory();
        void AssembliesFromApplicationBaseDirectory(Predicate<Assembly> assemblyFilter);
        #endregion

        #region Filtering Types
        void Exclude(Func<Type, bool> exclude);
        void ExcludeNamespace(string nameSpace);
        void ExcludeNamespaceContainingType<T>();
        void Include(Func<Type, bool> predicate);
        void IncludeNamespace(string nameSpace);
        void IncludeNamespaceContainingType<T>();
        void ExcludeType<T>();
        #endregion

        #region Conventions Support
        void Convention<T>() where T : IRegistrationConvention, new();
        void With(IRegistrationConvention convention);
        void ModifyGraphAfterScan(Action<ValidationGraph> action);
        void ModifyGraphAfterScan<T>() where T : IGraphModifier, new();
        void ModifyGraphAfterScan(IGraphModifier modifer);
        ConfigureDefaultValidationExpression ByDefault { get; }
        #endregion
    }
}