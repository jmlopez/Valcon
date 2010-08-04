using System;

namespace Valcon.HelloWorld.Infrastructure
{
	public interface IMappingRegistry
	{
		TDestination Map<TSource, TDestination>(TSource source) where TDestination : class;
		object Map(Type sourceType, Type destinationType, object source);
	}
}
