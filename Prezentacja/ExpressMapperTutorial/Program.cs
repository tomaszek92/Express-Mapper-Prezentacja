using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressMapperTutorial.Models.Test1;

namespace ExpressMapperTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            ExpressMapper.Mapper.Register<PersonEf, PersonViewModel>();
            ExpressMapper.Mapper.Compile();
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<PersonEf, PersonViewModel>());

            const int thousand = 1000;
            const int million = 1000000;

            var firstTest = new Tester<PersonEf, PersonViewModel>();
            firstTest.CompareMappers(thousand);
            firstTest.CompareMappers(million);

            Console.ReadLine();
        }
    }
}