using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using Valcon.HelloWorld.Configuration.Actions;

namespace Valcon.HelloWorld.Configuration.Sources
{
    public class FindActionsSource : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(TypePool types)
        {
            return types
                .TypesMatching(t => t.HasAttribute<PartialModelAttribute>())
                .Select(m =>
                            {
                                var actionType = typeof(PartialAction<>).MakeGenericType(m);
                                return new ActionCall(actionType, actionType.GetExecuteMethod());
                            });
        }
    }

    public static class TypeExtensions
    {
        public static bool HasAttribute<TAttribute>(this Type t)
            where TAttribute : Attribute
        {
            object[] atts = t.GetCustomAttributes(typeof(TAttribute), true);
            return atts.Length > 0;
        }

        public static bool IsPolyphonyAction(this Type t)
        {
            return t.Namespace.StartsWith(typeof (ActionMarker).Namespace) && t.Name.EndsWith("Action") && t.GetExecuteMethod() != null;
        }

        public static MethodInfo GetExecuteMethod(this Type t)
        {
            return t.GetMethod("Execute", BindingFlags.Instance | BindingFlags.Public);
        }
    }
}