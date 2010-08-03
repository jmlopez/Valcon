﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Valcon.Attributes;

namespace Valcon
{
    internal static class CoreExtensions
    {
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

        public static bool IsInNamespace(this Type type, string nameSpace)
        {
            return type.Namespace.StartsWith(nameSpace);
        }

        public static void Fill<T>(this IList<T> list, T value)
        {
            if (list.Contains(value)) return;
            list.Add(value);
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T target in enumerable)
            {
                action(target);
            }

            return enumerable;
        }

        /// <summary>
        /// Appends a sequence of items to an existing list
        /// </summary>
        /// <typeparam name="T">The type of the items in the list</typeparam>
        /// <param name="list">The list to modify</param>
        /// <param name="items">The sequence of items to add to the list</param>
        /// <returns></returns>
        public static IList<T> AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            items.Each(list.Add);
            return list;
        }
    }
}