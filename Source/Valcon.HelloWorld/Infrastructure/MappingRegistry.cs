using System;
using AutoMapper;

namespace Valcon.HelloWorld.Infrastructure
{
	public class MappingRegistry : IMappingRegistry
	{
        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : class
		{
			return Mapper.Map<TSource, TDestination>(source);
		}

	    public object Map(Type sourceType, Type destinationType, object source)
	    {
	        return Mapper.Map(source, sourceType, destinationType);
	    }
	}
}
