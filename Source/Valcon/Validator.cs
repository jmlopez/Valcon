using System;

namespace Valcon
{
    public static class Validator
    {
        private static ValidationGraph _graph;

        public static ValidationGraph ValidationGraph { get { return _graph; } }

        public static void Initialize(Action<IInitializationExpression> action)
        {
            lock (typeof(Validator))
            {
                var expression = new InitializationExpression();
                action(expression);

                _graph = expression.BuildGraph();
                _graph.Seal();
            }
        }

        public static ValidationChain<T> FindChain<T>()
           where T : class
        {
            return ValidationGraph.FindChain<T>();
        }

        public static ValidationChain FindChain(Type modelType)
        {
            return ValidationGraph.FindChain(modelType);
        }
    }
}