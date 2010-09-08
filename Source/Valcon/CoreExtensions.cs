using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Valcon.Attributes;
using Valcon.Rules;

namespace Valcon
{
    internal static class CoreExtensions
    {
        public static object GetDefaultInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static bool IsValidationRule(this Type type)
        {
            return typeof (IValidationRule).IsAssignableFrom(type);
        }

        public static IEnumerable<RuleAttribute> GetValidationAttributes(this PropertyInfo property)
        {
            var attributeNamespace = typeof (AttributeMarker).Namespace;

            // TODO -- Allow for any "RuleAttribute" rather than just Valcon's predefined
            var attributeTypes =
                typeof (AttributeMarker).Assembly.GetTypes().Where(t => t.Namespace.StartsWith(attributeNamespace)
                                                                        && typeof (RuleAttribute).IsAssignableFrom(t) && !t.IsAbstract);
            foreach (var attributeType in attributeTypes)
            {
                var attributes = property.GetCustomAttributes(attributeType, true);
                if(attributes.Length != 0)
                {
                    yield return (RuleAttribute)attributes[0];
                }
            }
        }

        public static IEnumerable<PropertyInfo> GetPublicProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public static IEnumerable<PropertyInfo> PropertiesWhere(this Type type, Func<PropertyInfo, bool> predicate)
        {
            return type.GetPublicProperties().Where(predicate);
        }

        public static void EachProperty(this Type type, Action<PropertyInfo> action)
        {
            type
                .GetPublicProperties()
                .Each(action);
        }
    }
}