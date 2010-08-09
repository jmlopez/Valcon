namespace Valcon.HelloWorld.Configuration.Actions
{
    public class PartialAction<T>
        where T : class
    {
        public T Execute(T input)
        {
            return input;
        }
    }
}