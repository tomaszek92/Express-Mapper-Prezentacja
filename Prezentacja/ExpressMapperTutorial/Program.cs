using System;
using System.Collections.Generic;
using ExpressMapperTutorial.Models.Test1;
using ExpressMapperTutorial.Models.Test2;
using ExpressMapperTutorial.Models.Test3;

namespace ExpressMapperTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize ExpressMapper
            // test 1
            ExpressMapper.Mapper.Register<PersonEf, PersonViewModel>();
            // test 2
            ExpressMapper.Mapper.Register<SourceC, DestC>();
            ExpressMapper.Mapper.Register<SourceB, DestB>();
            ExpressMapper.Mapper.Register<SourceA, DestA>();
            ExpressMapper.Mapper.Register<SourceTest, DestTest>();
            // test 3
            ExpressMapper.Mapper.RegisterCustom<SourceCarCategory, DestCarCategory>(src =>
            {
                switch (src)
                {
                    case SourceCarCategory.A:
                        return DestCarCategory.Mini;
                    case SourceCarCategory.B:
                        return DestCarCategory.Miejskie;
                    case SourceCarCategory.C:
                        return DestCarCategory.Kompaktowe;
                    case SourceCarCategory.D:
                        return DestCarCategory.Średnie;
                    case SourceCarCategory.E:
                        return DestCarCategory.Wyższe;
                    case SourceCarCategory.F:
                        return DestCarCategory.Luksusowe;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(src), src, null);
                }
            });
            ExpressMapper.Mapper.Register<SourceCarItem, DestCarItem>();
            ExpressMapper.Mapper.Register<SourceCar, DestCar>()
                .Function(dest => dest.MaxSpeedInMetersPerSec, src => Math.Round(src.MaxSpeed*10/3.6))
                .Member(dest => dest.Cat, src => src.Category)
                .Member(dest => dest.Mark, src => src.Brand);

            ExpressMapper.Mapper.Compile();

            // initialize AutoMapper
            AutoMapper.MapperConfiguration autoMapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                // test 1
                cfg.CreateMap<PersonEf, PersonViewModel>();
                // test 2
                cfg.CreateMap<SourceTest, DestTest>();
                cfg.CreateMap<SourceA, DestA>();
                cfg.CreateMap<SourceB, DestB>();
                cfg.CreateMap<SourceC, DestC>();
                // test 3
                cfg.CreateMap<SourceCarCategory, DestCarCategory>().ConstructUsing(src =>
                {
                    switch (src)
                    {
                        case SourceCarCategory.A:
                            return DestCarCategory.Mini;
                        case SourceCarCategory.B:
                            return DestCarCategory.Miejskie;
                        case SourceCarCategory.C:
                            return DestCarCategory.Kompaktowe;
                        case SourceCarCategory.D:
                            return DestCarCategory.Średnie;
                        case SourceCarCategory.E:
                            return DestCarCategory.Wyższe;
                        case SourceCarCategory.F:
                            return DestCarCategory.Luksusowe;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(src), src, null);
                    }
                });
                cfg.CreateMap<SourceCarItem, DestCarItem>();
                cfg.CreateMap<SourceCar, DestCar>()
                    .ForMember(
                        dest => dest.MaxSpeedInMetersPerSec,
                        options => options.MapFrom(src => Math.Round(src.MaxSpeed*10/3.6)))
                    .ForMember(dest => dest.Cat, options => options.MapFrom(src => src.Category))
                    .ForMember(dest => dest.Mark, options => options.MapFrom(src => src.Brand));
            });
            AutoMapper.IMapper autoMaper = autoMapperConfig.CreateMapper();

            const int hundred = 100;
            const int million = 1000000;

            var tester1 = new Tester<PersonEf, PersonViewModel>(autoMaper);
            tester1.CompareMappers(hundred);
            tester1.CompareMappers(million);
            Console.WriteLine("---------------------------------");

            var tester2 = new Tester<SourceTest, DestTest>(autoMaper);
            tester2.CompareMappers(hundred);
            Console.WriteLine("---------------------------------");

            var testet3 = new Tester<SourceCar, DestCar>(autoMaper);
            IDictionary<MapperType, List<DestCar>> mapResults = testet3.CompareMappers(hundred);
            testet3.CompareMappers(million);

            DestCar expressMapperCar = mapResults[MapperType.ExpressMapper][10];
            DestCar autoMapperCar = mapResults[MapperType.AutoMapper][10];

            if (!Equals(expressMapperCar, autoMapperCar))
            {
                throw new Exception();
            }
            Console.ReadLine();
        }
    }
}