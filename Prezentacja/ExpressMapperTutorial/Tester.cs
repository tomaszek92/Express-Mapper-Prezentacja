using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using AutoMapper;
using ExpressMapper.Extensions;
using ExpressMapperTutorial.Models;

namespace ExpressMapperTutorial
{
    public class Tester<TSource, TDest>
        where TSource : IRandomCreateable, IHandWrittenMapperable<TDest>, new()
        where TDest : new()
    {
        private readonly IMapper _autoMaper;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private TSource[] _toMap;

        public Tester(IMapper autoMaper)
        {
            _autoMaper = autoMaper;
        }

        public IDictionary<MapperType, List<TDest>> CompareMappers(long numerOfMappings)
        {
            Console.WriteLine($"Test for {numerOfMappings} mappings");

            _toMap = new TSource[numerOfMappings];
            for (int i = 0; i < numerOfMappings; i++)
            {
                _toMap[i] = new TSource();
                _toMap[i].FillWithRandomValues();
            }

            IDictionary<MapperType, List<TDest>> results = new Dictionary<MapperType, List<TDest>>();
            results.Add(DoSingeTest(numerOfMappings, MapperType.ExpressMapper, x => x.Map<TSource, TDest>()));
            results.Add(DoSingeTest(numerOfMappings, MapperType.AutoMapper, x => _autoMaper.Map<TDest>(x)));
            results.Add(DoSingeTest(numerOfMappings, MapperType.HandWritten, x => x.Map()));

            Console.WriteLine();
            return results;
        }

        private KeyValuePair<MapperType, List<TDest>> DoSingeTest(long numerOfMappings, MapperType mapperType,
            Func<TSource, TDest> map)
        {
            List<TDest> result = new List<TDest>();

            _stopwatch.Restart();
            for (long i = 0; i < numerOfMappings; i++)
            {
                result.Add(map(_toMap[i]));
            }
            _stopwatch.Stop();
            Console.WriteLine($"{GetMapperType(mapperType)}: {_stopwatch.Elapsed.TotalMilliseconds}");

            return new KeyValuePair<MapperType, List<TDest>>(mapperType, result);
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