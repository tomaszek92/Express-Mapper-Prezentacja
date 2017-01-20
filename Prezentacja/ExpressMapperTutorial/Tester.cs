using System;
using System.Diagnostics;
using ExpressMapper.Extensions;
using ExpressMapperTutorial.Models;

namespace ExpressMapperTutorial
{
    public class Tester<TSource, TDest>
        where TSource : IRandomCreateable, IHandWrittenMapperable<TDest>, new()
        where TDest : new()
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private TSource[] _toMap;

        public void CompareMappers(long numerOfMappings)
        {
            Console.WriteLine($"Test for {numerOfMappings} mappings");

            _toMap = new TSource[numerOfMappings];
            for (int i = 0; i < numerOfMappings; i++)
            {
                _toMap[i] = new TSource();
                _toMap[i].FillWithRandomValues();
            }

            DoSingeTest(numerOfMappings, MapperType.ExpressMapper, x => x.Map<TSource, TDest>());
            DoSingeTest(numerOfMappings, MapperType.AutoMapper, x => AutoMapper.Mapper.Map<TDest>(x));
            DoSingeTest(numerOfMappings, MapperType.HandWritten, x => x.Map());
            Console.WriteLine();
        }

        private void DoSingeTest(long numerOfMappings, MapperType mapperType, Func<TSource, TDest> map)
        {
            _stopwatch.Restart();
            for (long i = 0; i < numerOfMappings; i++)
            {
                map(_toMap[i]);
            }
            _stopwatch.Stop();
            Console.WriteLine($"{GetMapperType(mapperType)}: {_stopwatch.Elapsed.TotalMilliseconds}");
        }

        private static string GetMapperType(MapperType mapperType)
        {
            switch (mapperType)
            {
                case MapperType.ExpressMapper:
                {
                    return "ExpressMapper";
                }
                case MapperType.AutoMapper:
                {
                    return "AutoMapper";
                }
                case MapperType.HandWritten:
                {
                    return "Hand written";
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(mapperType), mapperType, null);
                }
            }
        }
    }
}