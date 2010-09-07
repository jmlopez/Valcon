using System;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Valcon.HelloWorld.Infrastructure
{
    public interface IJsonService
    {
        object Deserialize(Type type, string input);
    }

    public class JsonService : IJsonService
    {
        private readonly JavaScriptSerializer _serializer;
        private readonly MethodInfo _method;
        public JsonService()
        {
            _serializer = new JavaScriptSerializer();
            _method = _serializer
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .First(m => m.Name.EndsWith("Deserialize"));
        }
        public object Deserialize(Type type, string input)
        {
            return _method
                    .MakeGenericMethod(type)
                    .Invoke(_serializer, new[]{input});
        }
    }
}