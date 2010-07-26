using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Valcon.Rules
{
    public class Rule
    {
        public static IValidationRule For(Type modelType, PropertyInfo property, string name)
        {
            var rulesNamespace = typeof(RuleMarker).Namespace;
            var ruleTypes = typeof(RuleMarker)
                .Assembly
                .GetTypes()
                .Where(t => t.Namespace.StartsWith(rulesNamespace) && typeof(IValidationRule).IsAssignableFrom(t));

            var targetRule = ruleTypes.SingleOrDefault(t =>
                                                           {
                                                               var typeName = t.Name.ToLower();
                                                               if (t.IsGenericType)
                                                               {
                                                                   typeName = typeName.Replace("`2", string.Empty);
                                                               }

                                                               return typeName.Replace("validationrule", string.Empty) == name.ToLower();
                                                           });

            if (targetRule == null)
            {
                return null;
            }

            // TODO -- how do we pass in the expression?
            targetRule = targetRule.MakeGenericType(modelType, property.PropertyType);
            var constructors =
                targetRule.GetConstructors(BindingFlags.Instance | BindingFlags.CreateInstance | BindingFlags.Public);
            if (constructors.Length == 0)
            {
                return null;
            }

            return (IValidationRule)constructors[0].Invoke(new object[] { LambdaBuilder.ToLambda(modelType, property) });
        }
    }

    public class LambdaBuilder
    {
        public static LambdaExpression ToLambda(Type concreteType, PropertyInfo property)
        {
            var param = Expression.Parameter(concreteType, "x");
            var propertyExpression = Expression.Lambda(Expression.Property(param, property), param);
            return propertyExpression;
        }
    }
}