using System;
using System.Linq;
using Nerdle.AutoConfig.Exceptions;

namespace Nerdle.AutoConfig.Mappers
{
    static class Mapper
    {
        static readonly IQueryableMapper[] Mappers =
        {
            new ValueMapper(), 
            new CollectionMapper() 
        };
            
        public static IMapper For(Type type)
        {
            var mappers = Mappers.Where(m => m.CanMap(type)).ToList();

            if (mappers.Count > 1)
                throw new AutoConfigMappingException(
                    string.Format("Multiple IMappers found to handle type '{0}'.", type));

            return mappers.Count == 1 ? (IMapper)mappers.Single() : new ComplexMapper();
        }
    }
}