using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressMapperTutorial.Models.Test1;
using ExpressMapperTutorial.Models.Test2;

namespace ExpressMapperTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            ExpressMapper.Mapper.Register<PersonEf, PersonViewModel>();
            ExpressMapper.Mapper.Register<SourceC, DestC>();
            ExpressMapper.Mapper.Register<SourceB, DestB>();
            ExpressMapper.Mapper.Register<SourceA, DestA>();
            ExpressMapper.Mapper.Register<SourceTest, DestTest>();
            ExpressMapper.Mapper.Compile();

            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<PersonEf, PersonViewModel>());
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<SourceTest, DestTest>());
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<SourceA, DestA>());
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<SourceB, DestB>());
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<SourceC, DestC>());

            const int thousand = 1000;
            const int million = 1000000;

            var tester1 = new Tester<PersonEf, PersonViewModel>();
            tester1.CompareMappers(thousand);
            tester1.CompareMappers(million);

            var tester2 = new Tester<SourceTest, DestTest>();
            tester2.CompareMappers(1);

            Console.ReadLine();
        }
    }
}