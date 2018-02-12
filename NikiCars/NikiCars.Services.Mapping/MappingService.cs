using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace NikiCars.Services.Mapping
{
    public class MappingService : IMappingService
    {
        static MappingService()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(Assembly.GetAssembly(typeof(IMappingService))));
            Mapper.AssertConfigurationIsValid();
        }

        public TOut Map<TOut>(object model)
        {
            return Mapper.Map<TOut>(model);
        }
    }
}
