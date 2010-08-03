using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Valcon.Registration.Graph;

namespace Valcon.Registration
{
    public class ValidationChain : IEnumerable<ValidationCall>
    {
        private readonly IList<ValidationCall> _calls;
        private readonly Type _type;
        public ValidationChain(Type type)
        {
            _calls = new List<ValidationCall>();
            _type = type;
        }

        public void AddCall(ValidationCall call)
        {
            _calls.Add(call);
        }

        public Type ModelType
        {
            get { return _type; }
        }

        public IEnumerator<ValidationCall> GetEnumerator()
        {
            return _calls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<ValidationCall> CallsFor(string propertyName)
        {
            return this.Where(call => call.Accessor.Property.Name == propertyName);
        }

        public static ValidationChain GenericForModel(Type modelType)
        {
            var chainType = typeof (ValidationChain<>).MakeGenericType(modelType);
            return (ValidationChain) Activator.CreateInstance(chainType);
        }
    }

    public class ValidationChain<T> : ValidationChain
    {
        public ValidationChain()
            : base(typeof(T))
        {
        }

        public IEnumerable<ValidationCall> CallsFor(Expression<Func<T, object>> propertyExpression)
        {
            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Invalid expression - not a member access.", "propertyExpression");
            }

            // TODO -- Blow up if not a property? Or should we allow for fields too?
            var property = memberExpression.Member as PropertyInfo;
            if(property == null)
            {
                yield break;
            }

            var calls = CallsFor(property.Name);
            foreach (var call in calls)
            {
                yield return call;
            }
        }
    }
}